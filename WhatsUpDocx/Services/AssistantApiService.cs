namespace WhatsUpDocx.Services
{
    using Microsoft.AspNetCore.Components.Forms;
    using System;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    // Example Request Models (Adapt as needed)
    public class CreateAssistantRequest
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public object[] Tools { get; set; }
        public string Model { get; set; }
    }

    public class CreateMessageRequest
    {
        public string Role { get; set; }    // e.g. "user"
        public string Content { get; set; }
    }

    public class CreateRunRequest
    {
        public string Instructions { get; set; }
    }
    public class AssistantsApiService
    {
        private readonly HttpClient _httpClient;

        // Optional: These could be stored in a database or passed in from your calling code.
        private string _assistantId;
        private string _threadId;

        public AssistantsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // The base address and headers could be set here,
            // or configured externally where the service is registered.
            // Example:
            // _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_OPENAI_API_KEY");
            // _httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
        }

        public async Task<ThreadResponse> CreateThreadAsync()
        {
            var response = await _httpClient.PostAsync("v1/threads", new StringContent("", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();


            // Read and deserialize the response
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<ThreadResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result; // or just the Thread ID
        }

        public async Task<MessageResponse> CreateMessageAsync(string threadId, string fileId)
        {
            // 1. Construct the request body:
            var body = new
            {
                role = "user",
                content = "Please, give me a summary of the content of the file",
                attachments = new[]
                {
                    new
                    {
                        file_id = fileId,
                        tools = new[]
                        {
                            new
                            {
                                type = "file_search"
                            }
                        }
                    }
                }
            };

            // 2. Serialize the body to JSON
            var jsonBody = JsonSerializer.Serialize(body);

            // 3. POST /v1/threads with the JSON
            using var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"v1/threads/{threadId}/messages", content);
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<MessageResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<ThreadResponse> GetThreadAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<ThreadResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<string> GetMessageAsync(string threadId, string messageId)
        {
            var url = $"v1/threads/{threadId}/messages/{messageId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<GetMessagesResponse> GetMessagesAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}/messages";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<GetMessagesResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<string> GetFileAsync(string fileId)
        {
            var url = $"v1/files/{fileId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> GetFilesAsync()
        {
            var url = $"v1/files";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<FileResponse> UploadFileAsync(IBrowserFile blazorFile, string purpose = "assistants")
        {
            // Validate
            if (blazorFile == null)
                throw new ArgumentNullException(nameof(blazorFile));

            using var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(purpose), "purpose");


            var stream = blazorFile.OpenReadStream(long.MaxValue);
            var fileContent = new StreamContent(stream);

            formData.Add(fileContent, "file", blazorFile.Name);

            // Post to OpenAI /v1/files
            var response = await _httpClient.PostAsync("v1/files", formData);
            response.EnsureSuccessStatusCode();

            // Deserialize the response into FileResponse
            var json = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<FileResponse>(json, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<RunResponse> CreateRunAsync(string threadId, string assistantId)
        {
            var body = new
            {
                assistant_id = assistantId,
                //instructions = request.Instructions
            };

            var url = $"v1/threads/{threadId}/runs";
            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<RunResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<RunResponse> GetRunAsync(string threadId, string runId)
        {
            var url = $"v1/threads/{threadId}/runs/{runId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<RunResponse>(jsonString, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

        public async Task<string> GetRunsAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}/runs";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public class FileResponse
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public long Bytes { get; set; }
            public long CreatedAt { get; set; }
            public string Filename { get; set; }
            public string Purpose { get; set; }
        }
        public class ThreadResponse
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public long CreatedAt { get; set; }
            public Dictionary<string, object> Metadata { get; set; }
            public Dictionary<string, object> ToolResources { get; set; }
        }
        public class MessageResponse
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public long CreatedAt { get; set; }
            public string AssistantId { get; set; }
            public string ThreadId { get; set; }
            public string RunId { get; set; }
            public string Role { get; set; }
            public List<ContentDetails> Content { get; set; }
            public List<object> Attachments { get; set; }
            public Dictionary<string, object> Metadata { get; set; }

            public class ContentDetails
            {
                public string Type { get; set; }
                public Text Text { get; set; }
            }

            public class Text
            {
                public string Value { get; set; }
                public List<object> Annotations { get; set; }
            }
        }

        public class GetMessagesResponse
        {
            public string Object { get; set; }
            public List<MessageResponse> Data { get; set; }
            public string FirstId { get; set; }
            public string LastId { get; set; }
            public bool HasMore { get; set; }
        }
        public class RunResponse
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public long CreatedAt { get; set; }
            public string AssistantId { get; set; }
            public string ThreadId { get; set; }
            public string Status { get; set; }
            public long? StartedAt { get; set; }
            public long? ExpiresAt { get; set; }
            public long? CancelledAt { get; set; }
            public long? FailedAt { get; set; }
            public long? CompletedAt { get; set; }
            public string LastError { get; set; }
            public string Model { get; set; }
            public string Instructions { get; set; }
            public object IncompleteDetails { get; set; }
            public List<Tool> Tools { get; set; }
            public Dictionary<string, object> Metadata { get; set; }
            public object Usage { get; set; }
            public double Temperature { get; set; }
            public double TopP { get; set; }
            public int MaxPromptTokens { get; set; }
            public int MaxCompletionTokens { get; set; }
            public TruncationStrategyDetails TruncationStrategy { get; set; }
            public string ResponseFormat { get; set; }
            public string ToolChoice { get; set; }
            public bool ParallelToolCalls { get; set; }

            public class Tool
            {
                public string Type { get; set; }
            }

            public class TruncationStrategyDetails
            {
                public string Type { get; set; }
                public object LastMessages { get; set; }
            }
        }


    }
}



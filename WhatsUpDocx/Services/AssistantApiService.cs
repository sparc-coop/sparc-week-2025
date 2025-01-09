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
        public async Task<string> CreateAssistantAsync(CreateAssistantRequest request)
        {
            var body = new
            {
                name = request.Name,
                instructions = request.Instructions,
                tools = request.Tools,
                model = request.Model
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("v1/assistants", jsonContent);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(jsonString);
            _assistantId = doc.RootElement.GetProperty("id").GetString(); // e.g. assistant_xyz123

            return jsonString; // or you could return the Assistant ID or a typed model
        }

        public async Task<string> CreateThreadAsync()
        {
            var response = await _httpClient.PostAsync("v1/threads", new StringContent("", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(jsonString);
            _threadId = doc.RootElement.GetProperty("id").GetString(); // e.g. thread_abc123

            return jsonString; // or just the Thread ID
        }

        public async Task<string> CreateThreadWithMessageAndFileAsync(string fileId)
        {
            // 1. Construct the request body:
            var body = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = "Please, analyze the content of the file"
                    }
                },
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
            var response = await _httpClient.PostAsync("v1/threads", content);
            response.EnsureSuccessStatusCode();

            // 4. Return raw JSON (or parse it)
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> CreateMessageAsync(string threadId, string fileId)
        {
            // 1. Construct the request body:
            var body = new
            {
                role = "user",
                content = "Please, analyze the content of the file",
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

            // 4. Return raw JSON (or parse it)
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> GetThreadAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
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

        public async Task<string> GetMessagesAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}/messages";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
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

        public async Task<string> UploadFileAsync(IBrowserFile blazorFile, string purpose = "assistants")
        {
            // Validate
            if (blazorFile == null)
                throw new ArgumentNullException(nameof(blazorFile));

            using var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(purpose), "purpose");


            var stream = blazorFile.OpenReadStream(long.MaxValue);
            var fileContent = new StreamContent(stream);

            formData.Add(fileContent, "file", blazorFile.Name);

            // 5. Post to OpenAI /v1/files
            var response = await _httpClient.PostAsync("v1/files", formData);
            response.EnsureSuccessStatusCode();

            // 6. Return raw JSON string (or deserialize to a C# model)
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<string> CreateMessageAsync(CreateMessageRequest request)
        {
            var body = new
            {
                role = request.Role,    // "user" | "system" | "assistant"
                content = request.Content
            };

            var url = $"v1/threads/{_threadId}/messages";
            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateRunAsync(string threadId, string assistantId)
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

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetRunAsync(string threadId, string runId)
        {
            var url = $"v1/threads/{threadId}/runs/{runId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetRunsAsync(string threadId)
        {
            var url = $"v1/threads/{threadId}/runs";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}



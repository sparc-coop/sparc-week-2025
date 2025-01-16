using Newtonsoft.Json.Linq;

namespace WhatsUpDocx.Document;

public class Document : BlossomEntity<string>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Analysis { get; set; }
    public bool IsDeleted { get; set; }


    public Document() : base(Guid.NewGuid().ToString())
    {
    }

    public void DefineDocument(string name, string analysis)
    {
        Name = name;
        Analysis = analysis;
    }
}

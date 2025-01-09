using Newtonsoft.Json.Linq;

namespace WhatsUpDocx.Documents;

public class Document : BlossomEntity<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string Analysis { get; set; }


    public Document() : base(Guid.NewGuid().ToString())
    {
    }

    public void DefineName(string name)
    {
        Name = name;
    }
}

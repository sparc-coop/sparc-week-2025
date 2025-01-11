namespace Sparc.Blossom.Girassol.Books;

public class Book : BlossomEntity<string>
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public Book(string title, string description) : base(Guid.NewGuid().ToString())
    {
        Title = title;
        Description = description;
    }
}

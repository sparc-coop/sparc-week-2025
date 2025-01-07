namespace Sparc.Blossom.Template.Books;

public class Books(BlossomAggregateOptions<Book> options) : BlossomAggregate<Book>(options)
{
    public BlossomQuery<Book> ByTitle(string title)
        => Query().Where(x => x.Title == title);

    public BlossomQuery<Book> Paginated(int page, int perPage)
        => Query().SkipTake(page*perPage, perPage);
}

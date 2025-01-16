namespace WhatsUpDocx.Document;

public class Documents(BlossomAggregateOptions<Document> options) : BlossomAggregate<Document>(options)
{
    public BlossomQuery<Document> All()
        => Query();

    public BlossomQuery<Document> AllWithoutDeleted()
        => Query().Where(x => !x.IsDeleted);
}
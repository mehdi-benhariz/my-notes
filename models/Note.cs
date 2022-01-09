public class Note
{
    public Note(string title, string? content)
    {
        Title = title;
        Content = content;
    }

    public int Id { get; set; }
    public string Title { get; set; } = "unknown";
    public string? Content { get; set; }
    public string? Secret { get; set; }


}

//create DTO class
public class NoteDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }

    public NoteDTO(Note note) => (Id, Title, Content) = (note.Id, note.Title, note.Content);

}


namespace Services;

public static class NoteService
{
    public static NoteDTO ToDTO(this Note note) => new NoteDTO(note);
}

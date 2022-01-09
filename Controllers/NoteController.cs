using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[Controller]")]
//create controller
public class NoteController : ControllerBase
{
    private readonly NoteDb _db;
    public NoteController(NoteDb db) => _db = db;
    //add url mapping

    [HttpGet("all")]
    public async Task<ActionResult<List<NoteDTO>>> getAllAsync()
    {
        var notes = await _db.Notes.Select(x => new NoteDTO(x)).ToListAsync();

        return Ok(notes);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDTO>> getById(int id)
    {
        var note = await _db.Notes.FindAsync(id);
        if (note is null) return NotFound("the note was not found");
        return Ok(new NoteDTO(note));
    }
    [HttpPost("add")]
    public async Task<ActionResult<NoteDTO>> addNote(Note note)
    {
        //check if note is valid
        //todo change it later
        // if (note is null) return BadRequest("note is null");
        // if (note.Title is null) return BadRequest("note title is null");
        // if (note.Title.Length < 3) return BadRequest("note title is too short");
        // if (note.Content is null) return BadRequest("note content is null");
        // if (note.Content.Length < 3) return BadRequest("note content is too short");
        // if (note.Secret is null) return BadRequest("note secret is null");
        // if (note.Secret.Length < 3) return BadRequest("note secret is too short");

        var newNote = new Note(note.Title, note.Content);
        _db.Notes.Add(newNote);

        var result = await _db.SaveChangesAsync();
        if (result == 0) return BadRequest("note was not added");
        return Ok(new NoteDTO(newNote));
    }
    [HttpPut("edit/{id}")]
    public async Task<ActionResult<NoteDTO>> editNote(int id, Note note)
    {
        var noteToEdit = await _db.Notes.FindAsync(id);
        if (noteToEdit is null) return NotFound("note was not found");
        noteToEdit.Title = note.Title;
        noteToEdit.Content = note.Content;
        noteToEdit.Secret = note.Secret;

        var result = await _db.SaveChangesAsync();
        if (result == 0) return BadRequest("note was not edited");
        return Ok(new NoteDTO(noteToEdit));
    }
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<NoteDTO>> deleteNote(int id)
    {
        var noteToDelete = await _db.Notes.FindAsync(id);
        if (noteToDelete is null) return NotFound("note was not found");
        _db.Notes.Remove(noteToDelete);

        var result = await _db.SaveChangesAsync();
        if (result == 0) return BadRequest("note was not deleted");
        return Ok(new NoteDTO(noteToDelete));
    }
    [HttpGet("search/{title}")]
    public async Task<ActionResult<List<NoteDTO>>> searchNote(string title)
    {
        var notes = await _db.Notes.Where(x => x.Title.Contains(title)).Select(x => new NoteDTO(x)).ToListAsync();
        if (notes.Count() == 0) return NotFound("note was not found");
        return Ok(notes);
    }

}

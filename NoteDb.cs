using Microsoft.EntityFrameworkCore;
using models;

public class NoteDb : DbContext
{
    public NoteDb(DbContextOptions<NoteDb> options)
        : base(options)
    { }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<User> Users { get; set; }

}
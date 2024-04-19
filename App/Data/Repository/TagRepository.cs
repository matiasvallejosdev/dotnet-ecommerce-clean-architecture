namespace App.Data;

using App.Contracts;
using Microsoft.EntityFrameworkCore;

public class TagRepository : ITagRepository
{
    private readonly EntityDataContext _context;

    public TagRepository(EntityDataContext context)
    {
        _context = context;
    }

    public async Task<Tag> CreateTag(Tag tag)
    {
        tag.CreatedAt = DateTime.Now;
        tag.UpdatedAt = DateTime.Now;
        tag.IsDown = false;
        tag.DownAt = null;

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> GetTagByName(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
    }
}
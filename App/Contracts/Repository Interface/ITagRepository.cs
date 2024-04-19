namespace App.Contracts;
using App.Data;

public interface ITagRepository
{
    Task<Tag> CreateTag(Tag tag);
    Task<Tag?> GetTagByName(string name);
}
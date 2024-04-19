namespace App.Contracts;

public class BaseDto
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDown { get; set; }
    public DateTime? DownAt { get; set; }
}
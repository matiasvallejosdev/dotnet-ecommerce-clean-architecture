namespace App.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Base
{
    public Base()
    {
        CreatedAt = DateTime.Now; // Default to current time
        UpdatedAt = DateTime.Now; // Default to current time
        IsDown = false; // Default to false
    }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsDown { get; set; } = false;
    public DateTime? DownAt { get; set; }
}

using System.Drawing;

namespace BCloud.Models.Interfaces;

public interface IShowingItem
{
    public string Type { get; set; }
    public static string? BackgroundColor { get; set; }

    public string Name { get; set; }
    public string IconPath { get; set; }
    
    public IShowingItem ItSelf => this;
}
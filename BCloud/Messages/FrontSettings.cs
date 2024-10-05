namespace BCloud.Messages;

public class FrontSettings
{
    public FrontSettings(string cloudAnimation, string color)
    {
        CloudAnimation = cloudAnimation;
        Color = color;
    }

    public string CloudAnimation { get; set; }
    public string Color { get; set; }
}
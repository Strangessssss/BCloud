using System.Collections.ObjectModel;
using BCloud.Models.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BCloud.Models;

public class UserProfile: ObservableObject, IShowingItem
{
    public UserProfile(string name)
    {
        Name = name;
        StatusImageId = Random.Shared.Next(1, 8);
    }

    public string Id { get; set; }
    public string Type { get; set; } = "User";
    public string Name { get; set; }
    public bool Authorized  { get; set; } =  false;
    public string IconPath { get; set; } = string.Empty;
    public int StatusImageId { get; set; }
    public UserProfile ItSelf => this;
}
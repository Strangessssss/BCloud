using System.Collections.ObjectModel;
using BCloud.Models.Interfaces;
using BCloud.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BCloud.Models;

public partial class UserProfile: ObservableObject, IShowingItem
{
    public UserProfile(string name)
    {
        Name = name;
    }

    public static string BackgroundColor { get; set; } = "#5D90A0";
    
    public string Type { get; set; } = "User";
    public string Name { get; set; }
    public bool Authorized  { get; set; } = false;
    public string IconPath { get; set; } = @"../Appearance/Images/dust_in_silhouette.png";
    public int StatusImageId { get; set; }
    public UserProfile ItSelf => this;

    [RelayCommand]
    public void Open()
    {
        if (Authorized)
        {
            if (Name == "Log in")
            {
                new LoginView().Show();
            }
        }
    } 
}
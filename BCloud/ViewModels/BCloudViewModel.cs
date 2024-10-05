using System.Collections.ObjectModel;
using BCloud.Models;
using BCloud.Models.Interfaces;
using BCloud.Services;

namespace BCloud.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class BCloudViewModel : ObservableObject
{
    
    public BCloudViewModel()
    {
        _explorerItems = new ObservableCollection<IShowingItem>();
        _explorerItems.Add(new UserProfile("Tima"));
        ((UserProfile)_explorerItems.Last()).Authorized = true;
        _explorerItems.Add(new UserProfile("Ali"));
        _explorerItems.Add(new UserProfile("Xanbaba"));
        _explorerItems.Add(new UserProfile("Alex"));
        _explorerItems.Add(new UserProfile("Ayxan"));
        _explorerItems.Add(new UserProfile("Samir"));
        _explorerItems.Add(new UserProfile("Rasul"));
        _explorerItems.Add(new UserProfile("Amin"));
        _explorerItems.Add(new UserProfile("Ayten"));
        _explorerItems.Add(new UserProfile("Ramil"));
    }
    
    [ObservableProperty] private ObservableCollection<IShowingItem>? _explorerItems;
    // [ObservableProperty] private ObservableCollection<User>? _explorerItems;
}
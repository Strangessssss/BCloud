using System.Collections.ObjectModel;
using BCloud.Messages;
using BCloud.Models;
using BCloud.Models.Interfaces;
using BCloud.Services;
using BCloud.Services.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BCloud.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class BCloudViewModel : ObservableObject
{
    
    private ClientService _clientService;
    
    public BCloudViewModel()
    {
        _clientService = new ("127.0.0.1", 8080, _user.Name);
        _ = _clientService.Connect();
        
        WeakReferenceMessenger.Default.Register<ReceivedFilesMessage>(this, OnReceivedFilesMessage);
        
        _explorerItems = new ObservableCollection<IShowingItem>();
        _explorerItems.Add(new UserProfile(_user.Name){Authorized = true});
        _explorerItems.Add(new UserProfile("Xanbaba"){Authorized = false});
        _explorerItems.Add(new UserProfile("Ali"){Authorized = false});
        _explorerItems.Add(new UserProfile("Samir"){Authorized = false});
        _explorerItems.Add(new UserProfile("Alex"){Authorized = false});
        _explorerItems.Add(new UserProfile("Ramil"){Authorized = false});
        _explorerItems.Add(new UserProfile("Magomed"){Authorized = false});
    }

    private void OnReceivedFilesMessage(object recipient, ReceivedFilesMessage message)
    {
        _clientService.Send(new Message(User.Name)
        {
            Destination = message.Destination,
            Content = message.Files
        });
    }

    [ObservableProperty] private ObservableCollection<IShowingItem>? _explorerItems;
    [ObservableProperty] private User _user = new(){Name = "Tima"};
}
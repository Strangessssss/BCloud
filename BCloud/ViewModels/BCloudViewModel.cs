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
        _clientService = new ClientService("127.0.0.1", 8080);
        _ = _clientService.Connect();
        
        WeakReferenceMessenger.Default.Register<ReceivedFilesMessage>(this, OnReceivedFilesMessage);
        WeakReferenceMessenger.Default.Register<SignUpMessage>(this, TryRegister);
        
        _explorerItems = [];

        if (CredentialsService.Name == null)
        {
            _explorerItems.Add(new UserProfile("Log in")
            {
                StatusImageId = 8,
                Authorized = true,
                IconPath = "../Appearance/Images/question.png"
            });
        }
        // _explorerItems.Add(new UserProfile(_user.Name){Authorized = true});
        // _explorerItems.Add(new UserProfile("Xanbaba"){Authorized = false});
        // _explorerItems.Add(new UserProfile("Ali"){Authorized = false});
        // _explorerItems.Add(new UserProfile("Samir"){Authorized = false});
        // _explorerItems.Add(new UserProfile("Alex"){Authorized = false});
        // _explorerItems.Add(new UserProfile("Ramil"){Authorized = false});
        // _explorerItems.Add(new UserProfile("Magomed"){Authorized = false});
    }

    private void TryRegister(object recipient, SignUpMessage message)
    {
        _clientService.Register(message);
    }

    private void OnReceivedFilesMessage(object recipient, ReceivedFilesMessage message)
    {
        if (User is null)
        {
            return;
        }
        
        _clientService.SendFiles(new FileMessage(User.Name)
        {
            Destination = message.Destination,
            FilePaths = message.Files
        });
    }

    [ObservableProperty] private ObservableCollection<IShowingItem>? _explorerItems = new ObservableCollection<IShowingItem>();
    [ObservableProperty] private User? _user;
}
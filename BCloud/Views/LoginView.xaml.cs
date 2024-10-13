using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BCloud.Appearance.Services;
using BCloud.Services.Messages;
using CommunityToolkit.Mvvm.Messaging;


namespace BCloud.Views;

public partial class LoginView : Window
{
    private const string SettingsFileName = "Appearance/AppearanceSettings.json";
    private const string CredentialsFileName = "Credentials.json";
    
    private string Color1 { get; set; } = "#c74066";
    private string Color2 { get; set; } = "#ff3366";
    private string Color3 { get; set; } = "#c74066";
    
    public LoginView()
    {
        InitializeComponent();
        UpdateControlColors();
        var lastName =  JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(CredentialsFileName))?["Username"];
        if (lastName != null)
            UsernameTextBox.Text = lastName;
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void Maximize(object sender, RoutedEventArgs e)
    {
        switch (WindowState)
        {
            case WindowState.Normal:
                WindowState = WindowState.Maximized;
                break;
            case WindowState.Maximized:
                WindowState = WindowState.Normal;
                break;
            case WindowState.Minimized:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Hide(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized; 
    }
    

    private void UpdateControlColors()
    {
        var colors = AppearanceManager.GetThemeInfo(SettingsFileName);
        if (colors != null)
            UpdateControlColors(colors);
    }

    private void UpdateControlColors(Dictionary<string, string> colors)
    {
        TitleBorder.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color1"]));
        BackgroundBorder.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color2"]));
        AppearanceSettingsTextBlock.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color3"]));
    }
    
    private Color ChangeBackgroundColor(string hexColor)
    {
        var color = (Color)ColorConverter.ConvertFromString(hexColor);
        return color;
    }

    private void SignUp_OnClick(object sender, RoutedEventArgs e)
    {
        if (PasswordTextBox.Text.Length is > 0 and <= 256 && UsernameTextBox.Text.Length is > 0 and <= 256)
        {
            if (Regex.IsMatch(UsernameTextBox.Text, @"[^a-zA-Z0-9]"))
            {
                WeakReferenceMessenger.Default.Send(new SignUpMessage()
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordTextBox.Text
                });
            }
        }
    }
}
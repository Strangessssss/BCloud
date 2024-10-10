using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using BCloud.Appearance.Services;
using BCloud.Messages;
using BCloud.Models;
using BCloud.Models.Interfaces;
using CommunityToolkit.Mvvm.Messaging;

namespace BCloud.Views;

public partial class BCloudUser
{
    
    private const string SettingsFileName = "Appearance/AppearanceSettings.json";
    private string? _themeImage = AppearanceManager.GetThemeImage(SettingsFileName);
    private string? UserBackground { get; set;}
    public BCloudUser()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
        UpdateControlColors();
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
        UserBackground = colors["Color2"];
        UserProfile.BackgroundColor = colors["Color3"];
    }
    
    private Color ChangeBackgroundColor(string hexColor)
    {
        var color = (Color)ColorConverter.ConvertFromString(hexColor);
        return color;
    }


    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void Hide(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized; 
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
    
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (!AppearanceManager.IsAnimationOn(SettingsFileName))
            return;

        var width = ScrollingImage1.ActualWidth;

        
        var animation = new DoubleAnimation
        {
            From = -width - 200,
            To = MaxWidth, 
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new TimeSpan(0, 0, Random.Shared.Next(15, 20)))
        };

        var animation1 = new DoubleAnimation
        {
            From = -width - 200,
            To = MaxWidth,
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new TimeSpan(0, 0, Random.Shared.Next(15, 20)))
        };

        var animation2 = new DoubleAnimation
        {
            From = -width - 200,
            To = MaxWidth,
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new TimeSpan(0, 0, Random.Shared.Next(15, 20)))
        };

        if (_themeImage != null)
        {
            var image = new BitmapImage(new Uri(_themeImage, UriKind.Relative));
            ScrollingImage1.Source = image;
            ScrollingImage2.Source = image;
            ScrollingImage3.Source = image;
        }

        ScrollingImage1.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);
        ScrollingImage2.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);
        ScrollingImage3.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);

        ScrollingImage1.BeginAnimation(Canvas.LeftProperty, animation);
        ScrollingImage2.BeginAnimation(Canvas.LeftProperty, animation1);
        ScrollingImage3.BeginAnimation(Canvas.LeftProperty, animation2);

    }

    private void UIElement_OnDragEnter(object sender, DragEventArgs e)
    {
        if (sender is Label { Content: Image image }) 
            image.Source = new BitmapImage(new Uri("../Appearance/Images/palms_up_together.png", UriKind.Relative));
    }

    private void UIElement_OnDragLeave(object sender, DragEventArgs e)
    {
        if (sender is Label { Content: Image image }) 
            image.Source = new BitmapImage(new Uri("../Appearance/Images/bust_in_silhouette.png", UriKind.Relative));
    }

    private void UIElement_OnDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            var user = ((sender as Label)!).DataContext as UserProfile;
            var message = new ReceivedFilesMessage { Files = files.ToList(), Destination = user!.Name};
            WeakReferenceMessenger.Default.Send(message);
        }
        UIElement_OnDragLeave(sender, e);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        new SettingsView().ShowDialog();
    }
}
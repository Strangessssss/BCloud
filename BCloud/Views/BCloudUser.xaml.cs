using System.Runtime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using BCloud.Models;
using CommunityToolkit.Mvvm.Messaging;

namespace BCloud.Views;

public partial class BCloudUser : Window
{
    public BCloudUser()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
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
        var width = ScrollingCloud.ActualWidth;
        
        var animation = new DoubleAnimation
        {
            From = MaxWidth,
            To = -width,
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new System.TimeSpan(0, 0, Random.Shared.Next(10, 30)))
        };
        
        var animation1 = new DoubleAnimation
        {
            From = MaxWidth,
            To = -width,
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new System.TimeSpan(0, 0, Random.Shared.Next(10, 30)))
        };
        
        var animation2 = new DoubleAnimation
        {
            From = MaxWidth,
            To = -width,
            RepeatBehavior = RepeatBehavior.Forever,
            Duration = new Duration(new System.TimeSpan(0, 0, Random.Shared.Next(10, 30)))
        };

        ScrollingCloud.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);
        ScrollingCloud1.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);
        ScrollingCloud2.Margin = new Thickness(0, Random.Shared.Next(0, Random.Shared.Next(250, 350)), 0, 0);
        
        ScrollingCloud.BeginAnimation(Canvas.LeftProperty, animation);
        ScrollingCloud1.BeginAnimation(Canvas.LeftProperty, animation1);
        ScrollingCloud2.BeginAnimation(Canvas.LeftProperty, animation2);
    }
    
    private T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T tChild)
            {
                return tChild;
            }
            var childOfChild = FindVisualChild<T>(child);
            return childOfChild;
        }
        return null;
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
            var files = (((string[])e.Data.GetData(DataFormats.FileDrop)));

            foreach (var file in files)
            {
                Console.WriteLine($"File dropped: {file}");
            }
        }
        UIElement_OnDragLeave(sender, e);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        new SettingsView().ShowDialog();
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;


namespace BCloud.Views;

public partial class SettingsView : Window
{
    public string Color1 { get; set; } = "#50808f";
    public string Color2 { get; set; } = "#50808f";
    public string Color3 { get; set; } = "#217894";
    public SettingsView()
    {
        InitializeComponent();
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

    private void MfcCmdUp_OnClick(object sender, RoutedEventArgs e)
    {
        MfcTxtNum.Text = (int.Parse(MfcTxtNum.Text) + 1).ToString();
    }

    private void MfcCmdDown_OnClick(object sender, RoutedEventArgs e)
    {
        MfcTxtNum.Text = (int.Parse(MfcTxtNum.Text) - 1).ToString();
    }

    private void MfsCmdUp_OnClick(object sender, RoutedEventArgs e)
    {
        MfsTxtNum.Text = (int.Parse(MfsTxtNum.Text) + 1).ToString();
    }

    private void MfsCmdDown_OnClick(object sender, RoutedEventArgs e)
    {
        MfsTxtNum.Text = (int.Parse(MfsTxtNum.Text) - 1).ToString();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog
        {
            Title = "Select a folder"
        };
        if (folderDialog.ShowDialog() == true)
        {
            var selectedPath = folderDialog.FolderName;

            RfdTextBox.Text = selectedPath;
        }
        
    }
}
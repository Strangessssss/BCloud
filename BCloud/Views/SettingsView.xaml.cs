using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BCloud.Appearance.Services;
using Microsoft.Win32;


namespace BCloud.Views;

public partial class SettingsView
{
    private const string SettingsFileName = "Appearance/AppearanceSettings.json";
    private const string ThemesDirectory = "Appearance/Themes";
    
    private string Color1 { get; set; } = "#c74066";
    private string Color2 { get; set; } = "#ff3366";
    private string Color3 { get; set; } = "#c74066";
    
    public SettingsView()
    {
        InitializeComponent();
        UpdateControlColors();
        
        var files = Directory.GetFiles(ThemesDirectory);
        foreach (var file in files)
        {
            ThemesComboBox.Items.Add(new ComboBoxItem(){Content = Path.GetFileNameWithoutExtension(file)});
        }
        for (var i = 0; i < ThemesComboBox.Items.Count; i++)
        {
            var theme = ThemesComboBox.Items[i];
            if ((string)((ComboBoxItem)theme!).Content != AppearanceManager.GetTheme(SettingsFileName)) continue;
            ThemesComboBox.SelectedIndex = i;
            break;
        }
        AnimationComboBox.SelectedIndex = AppearanceManager.IsAnimationOn(SettingsFileName) ? 0 : 1;
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
        FileSettingsTextBlock.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color3"]));
        SecurityTextBlock.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color3"]));
        ApplyButton.Background = new SolidColorBrush(ChangeBackgroundColor(colors["Color3"]));
    }
    
    private Color ChangeBackgroundColor(string hexColor)
    {
        var color = (Color)ColorConverter.ConvertFromString(hexColor);
        return color;
    }
    
    private void ThemeChanged(object sender, SelectionChangedEventArgs e)
    {
        var settings = AppearanceManager.GetSettings(SettingsFileName);
        var chosenTheme = (ComboBoxItem)e.AddedItems[0]!;
        if (settings != null)
                settings["Theme"] = chosenTheme.Content.ToString()!;
        var newJson = JsonSerializer.Serialize(settings);
        File.WriteAllText(SettingsFileName, newJson);
    }
    
    private void AnimationChoiceChanged(object sender, SelectionChangedEventArgs e)
    {
        var settings = AppearanceManager.GetSettings(SettingsFileName);
        var chosenTheme = (ComboBoxItem)e.AddedItems[0]!;
        if (settings != null)
            settings["Animation"] = chosenTheme.Content.ToString()!;
        var newJson = JsonSerializer.Serialize(settings);
        File.WriteAllText(SettingsFileName, newJson);
    }
}
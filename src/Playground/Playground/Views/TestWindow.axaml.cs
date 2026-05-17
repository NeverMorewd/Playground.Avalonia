using Avalonia.Controls;

namespace Playground.Views;

public partial class TestWindow : Window
{
    public TestWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void Close_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    { 
        Close();
    }
}
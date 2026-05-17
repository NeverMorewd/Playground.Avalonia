using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Playground.Views;

public partial class TestWindow : Window
{
    public TestWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
}
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Playground.Views;

public partial class ViewA : UserControl
{
    private bool _loaded = false;
    public ViewA()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (_loaded)
        {
            throw new InvalidOperationException("The view in NoReloadTabControl should be loaded once only!");
        }
        base.OnLoaded(e);
        _loaded = true;
    }
}
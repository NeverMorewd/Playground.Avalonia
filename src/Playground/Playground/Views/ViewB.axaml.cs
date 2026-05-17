using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace Playground.Views;

public partial class ViewB : UserControl
{
    private bool _loaded = false;
    public ViewB()
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

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
    }
}
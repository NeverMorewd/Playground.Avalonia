using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace Playground.Controls;

public class NoReloadTabControl : TabControl
{
    private const string PartItemsHolder = "PART_NoReloadItemsHolder";

    private Panel? _itemsHolder;

    //protected override Type StyleKeyOverride => typeof(TabControl);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _itemsHolder = e.NameScope.Find<Panel>(PartItemsHolder);

        if (Items is INotifyCollectionChanged ncc)
        {
            ncc.CollectionChanged += (s, ev) => UpdateSelectedVisibility();
        }

        UpdateSelectedVisibility();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SelectedItemProperty || change.Property == SelectedIndexProperty)
        {
            UpdateSelectedVisibility();
        }
    }
    private void UpdateSelectedVisibility()
    {
        if (_itemsHolder == null) return;

        var selectedItem = SelectedItem;

        foreach (var item in Items)
        {
            if (item != null)
            {
                EnsurePresenter(item);
            }
        }

        var currentItems = Items.Cast<object>().Where(i => i != null).ToList();
        var toRemove = _itemsHolder.Children
            .OfType<ContentPresenter>()
            .Where(p => p.Tag != null && !currentItems.Contains(p.Tag))
            .ToList();

        foreach (var p in toRemove)
        {
            _itemsHolder.Children.Remove(p);
            ((ISetLogicalParent)p).SetParent(null);
        }

        foreach (var child in _itemsHolder.Children.OfType<ContentPresenter>())
        {
            bool isSelected = child.Tag == selectedItem ||
                             (selectedItem != null && ResolveContent(child.Tag!) == selectedItem);

            child.IsVisible = isSelected;
        }
    }

    private ContentPresenter EnsurePresenter(object item)
    {
        var existing = FindPresenter(item);
        if (existing != null) return existing;

        var presenter = new ContentPresenter
        {
            Content = ResolveContent(item),
            ContentTemplate = ContentTemplate,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            IsVisible = false,
            Tag = item
        };
        ((ISetLogicalParent)presenter).SetParent(this);
        _itemsHolder?.Children.Add(presenter);

        return presenter;
    }

    private ContentPresenter? FindPresenter(object item)
    {
        return _itemsHolder?.Children
            .OfType<ContentPresenter>()
            .FirstOrDefault(cp => cp.Tag == item);
    }
    private static object? ResolveContent(object item) =>
        item is TabItem ti ? ti.Content : item;
}

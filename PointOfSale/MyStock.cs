using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PointOfSale;

//Enable notification entities
//Docs: https://learn.microsoft.com/ef/core/change-tracking/change-detection#notification-entities
//This is another area where CommunityToolkit.Mvvm source generators can greatly reduce boilerplate code.
public partial class MyStock : NotifyingEntity
{
    private int _id;
    private decimal _productId;
    private string? _productName;
    private int _qty;
    private decimal _price;
    private string? _description;

    public int Id
    {
        get => _id;
        set => SetWithNotify(value, out _id);
    }

    public decimal ProductId 
    { 
        get => _productId;
        set => SetWithNotify(value, out _productId);
    }

    public string? ProductName 
    { 
        get => _productName;
        set => SetWithNotify(value, out _productName);
    }

    public int Qty 
    { 
        get => _qty;
        set => SetWithNotify(value, out _qty);
    }

    public decimal Price 
    { 
        get => _price; 
        set => SetWithNotify(value, out _price);
    }

    public string? Description 
    {
        get => _description;
        set => SetWithNotify(value, out _description);
    }
}

public abstract class NotifyingEntity : INotifyPropertyChanging, INotifyPropertyChanged
{
    //This matches the docs but is a bit awkward of an API
    protected void SetWithNotify<T>(T value, out T field, [CallerMemberName] string propertyName = "")
    {
        NotifyChanging(propertyName);
        field = value;
        NotifyChanged(propertyName);
    }

    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void NotifyChanging(string propertyName)
        => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
}

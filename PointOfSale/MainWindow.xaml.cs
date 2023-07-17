using Microsoft.EntityFrameworkCore;
using PointOfSale.ViewModel;
using System;
using System.Windows;
using System.Windows.Data;

namespace PointOfSale;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MyStockContext DbContext { get; } = new();

    public MainWindow()
    {
        //This forces the creation and update of the database.
        //Assumes code first with migrations.
        //https://learn.microsoft.com/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
        DbContext.Database.Migrate();

        Loaded += MainWindow_Loaded;
        InitializeComponent();

        //This is just the quick and dirty approach
        //In many apps you might consider leveraging some form of dependency injection and MVVM.
        //you can see my preferred approach in setting up WPF apps here: https://github.com/Keboo/DotnetTemplates
        //This binds the data source directly to the DataGrid so that it can perform CRUD operations on it.
        var observableCollection = DbContext.MyStocks.Local.ToObservableCollection();

        //Allow for updating the collection from background threads
        //https://learn.microsoft.com/dotnet/api/system.windows.data.bindingoperations.enablecollectionsynchronization?view=windowsdesktop-7.0
        BindingOperations.EnableCollectionSynchronization(observableCollection, new object());

        //Connect the observable collection to the DataGrid
        dataGrid.ItemsSource = observableCollection;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //Load the data from the database
        await DbContext.MyStocks.LoadAsync();
    }

    private async void ConfirmPayment_Click(object sender, RoutedEventArgs args)
    {
        //Persis the changes to the database
        await DbContext.SaveChangesAsync();

        //Force the data grid to update to latest values
        //https://learn.microsoft.com/ef/core/get-started/wpf
        CollectionViewSource.GetDefaultView(dataGrid.ItemsSource).Refresh();
    }
}

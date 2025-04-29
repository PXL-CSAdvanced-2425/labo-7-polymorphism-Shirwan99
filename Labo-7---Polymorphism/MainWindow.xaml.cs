using Labo_7___Polymorphism.Data;
using Labo_7___Polymorphism.Entities;
using Microsoft.Win32;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Machine = Labo_7___Polymorphism.Entities.Machine;

namespace Labo_7___Polymorphism;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Store<Machine> _store = new Store<Machine>();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        // Open file dialog to select a file
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CVS bestand|*.cvs";
        if (ofd.ShowDialog() == true)
        {
            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                sr.ReadLine(); // Skip the header line
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] values = line.Split(',');

                    Machine machine = null;
                    switch (values[0])
                    {
                        case "L":
                            machine = new LaserCutter(
                                values[1],
                                double.Parse(values[2]),
                                double.Parse(values[3]),
                                double.Parse(values[4]),
                                double.Parse(values[4]));
                            break;
                        case "R":
                            machine = new Router(
                                values[1],
                                double.Parse(values[2]),
                                double.Parse(values[3]),
                                double.Parse(values[4]));
                            break;
                        case "G":
                            machine = new General(
                                values[1]);
                            break;
                        default:
                            break;
                    }
                    if (machine != null)
                    {
                        _store.AddItem(machine);
                    }
                }
            }
            UpdateListBox();

            clearButton.IsEnabled = true;
            sortButton.IsEnabled = true;
            filterButton.IsEnabled = true;
        }
    }
    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        _store.RemoveItem(SelctedMachine);
        UpdateListBox();
    }
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        _store.ClearAllItems();
        UpdateListBox();
    }

    private void UseButton_Click(object sender, RoutedEventArgs e)
    {
        if(SelctedMachine is not null)
        {
            if(int.TryParse(inputTextBox.Text, out int minutes)&& minutes > 0)
            {
                SelctedMachine.Use(minutes);
                UpdateListBox();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of minutes.");
            }
        }
    }
    private void SortButton_Click(object sender, RoutedEventArgs e)
    {
        _store.SortItems((x, y) => string.Compare(x.Name, y.Name));
        UpdateListBox();
    }

    private void FilterButton_Click(object sender, RoutedEventArgs e)
    {
        string filter = inputTextBox.Text;
        if (string.IsNullOrEmpty(filter))
        {
            itemsListBox.Items.Clear();
            foreach (var item in _store.FilterItems(m => m.Name.Contains(filter)))
            {
                itemsListBox.Items.Add(item);
            }
        }
    }

    private void itemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SelctedMachine != null)
        {
            removeButton.IsEnabled = true;
            useButton.IsEnabled = !SelctedMachine.OutOfUse;
        }
        else
        {
            removeButton.IsEnabled = false;
            useButton.IsEnabled = false;
        }
    }
    public Machine SelctedMachine => itemsListBox.SelectedItem as Machine;
    private void UpdateListBox()
    {
        itemsListBox.Items.Clear();

        foreach (Machine machine in _store.GetAllItems())
        {
            itemsListBox.Items.Add(machine);
        }
        removeButton.IsEnabled = false;
        useButton.IsEnabled = false;
    }
}
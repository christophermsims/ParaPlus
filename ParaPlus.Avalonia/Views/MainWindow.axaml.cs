using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ParaPlus.Avalonia.Views;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void OpenInventorAwards_Click(object? sender, RoutedEventArgs e)
	{
		var inventorAwardsWindow = new InventorAwardsWindow();
		inventorAwardsWindow.Show(this);
	}

	private void OpenQuarterlyOnePagers_Click(object? sender, RoutedEventArgs e)
	{
		var quarterlyOnePagersWindow = new QuarterlyOnePagersWindow();
		quarterlyOnePagersWindow.Show(this);
	}
}
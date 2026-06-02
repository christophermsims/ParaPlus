using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ParaPlus.Avalonia.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ParaPlus.Avalonia.Views
{
    public partial class InventorAwardsWindow : Window
    {
        public InventorAwardsWindow()
        {
            InitializeComponent();
            DataContext = new InventorAwardsViewModel();
        }

        private InventorAwardsViewModel? ViewModel => DataContext as InventorAwardsViewModel;

        private async Task<string?> PickFile()
        {
            var topLevel = GetTopLevel(this);
            if (topLevel == null) return null;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("CSV Files") { Patterns = new[] { "*.csv" } }
                }
            });

            return files.Count >= 1 ? files[0].TryGetLocalPath() : null;
        }

        private async Task<string?> PickFolder()
        {
            var topLevel = GetTopLevel(this);
            if (topLevel == null) return null;

            var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select Folder",
                AllowMultiple = false
            });

            return folders.Count >= 1 ? folders[0].TryGetLocalPath() : null;
        }

        private async void SelectQuarterlyIssuedAwardsFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile();
                if (path is not null) ViewModel.QuarterlyIssuedAwardsFile = path;
            }
        }

        private async void SelectMasterFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile();
                if (path is not null) ViewModel.MasterFile = path;
            }
        }

        private async void SelectInventorAddressFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile();
                if (path is not null) ViewModel.InventorAddressFile = path;
            }
        }

        private async void SelectOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFolder();
                if (path is not null) ViewModel.OutputFolder = path;
            }
        }

        private async void SelectChineseInventorFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile();
                if (path is not null) ViewModel.ChineseInventorFile = path;
            }
        }

        private async void SelectChineseOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFolder();
                if (path is not null) ViewModel.ChineseOutputFolder = path;
            }
        }
    }
}
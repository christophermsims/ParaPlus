using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ParaPlus.Avalonia.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParaPlus.Avalonia.Views
{
    public partial class QuarterlyOnePagersWindow : Window
    {
        public QuarterlyOnePagersWindow()
        {
            InitializeComponent();
            DataContext = new QuarterlyOnePagersViewModel();
        }

        private QuarterlyOnePagersViewModel? ViewModel => DataContext as QuarterlyOnePagersViewModel;

        private async Task<string?> PickFile(IReadOnlyList<FilePickerFileType> fileTypeFilter)
        {
            var topLevel = GetTopLevel(this);
            if (topLevel == null) return null;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select File",
                AllowMultiple = false,
                FileTypeFilter = fileTypeFilter
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

        private static readonly FilePickerFileType CsvFileType = new("CSV Files") { Patterns = new[] { "*.csv" } };

        private async void SelectQuarterlyPatentFilings_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile(new[] { CsvFileType });
                if (path is not null) ViewModel.QuarterlyPatentFilings = path;
            }
        }

        private async void SelectQuarterlyPatentsIssued_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile(new[] { CsvFileType });
                if (path is not null) ViewModel.QuarterlyPatentsIssued = path;
            }
        }

        private async void SelectQuarterlyOnePagersFile_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFile(new[] { CsvFileType });
                if (path is not null) ViewModel.QuarterlyOnePagersFile = path;
            }
        }

        private async void SelectOutputDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel is not null)
            {
                var path = await PickFolder();
                if (path is not null) ViewModel.OutputDirectory = path;
            }
        }
    }
}
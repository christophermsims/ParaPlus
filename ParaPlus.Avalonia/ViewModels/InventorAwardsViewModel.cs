using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ParaPlus.Business.FileProcessing;
using ParaPlus.Business.Helper;
using ParaPlus.Business.Jobs;
using ParaPlus.Business.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParaPlus.Avalonia.ViewModels
{
    public partial class InventorAwardsViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessAwardsCommand))]
        private string quarterlyIssuedAwardsFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessAwardsCommand))]
        private string masterFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessAwardsCommand))]
        private string inventorAddressFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessAwardsCommand))]
        private string outputFolder = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessChineseAwardsCommand))]
        private string chineseInventorFile = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessChineseAwardsCommand))]
        private string chineseOutputFolder = string.Empty;

        [ObservableProperty]
        private string logs = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ProcessAwardsCommand))]
        [NotifyCanExecuteChangedFor(nameof(ProcessChineseAwardsCommand))]
        private bool isProcessing;

        public bool CanProcessAwards =>
            !string.IsNullOrWhiteSpace(QuarterlyIssuedAwardsFile) &&
            !string.IsNullOrWhiteSpace(MasterFile) &&
            !string.IsNullOrWhiteSpace(InventorAddressFile) &&
            !string.IsNullOrWhiteSpace(OutputFolder) &&
            !IsProcessing;

        public bool CanProcessChineseAwards =>
            !string.IsNullOrWhiteSpace(ChineseInventorFile) &&
            !string.IsNullOrWhiteSpace(ChineseOutputFolder) &&
            !IsProcessing;

        public InventorAwardsViewModel()
        {
            // In a real app, you might inject these via DI
        }

        private void Log(string message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                Logs += $"{message}{Environment.NewLine}";
            });
        }

        [RelayCommand(CanExecute = nameof(CanProcessAwards))]
        private async Task ProcessAwardsAsync()
        {
            IsProcessing = true;
            Logs = string.Empty;
            Log("Starting inventor awards processing...");

            try
            {
                await Task.Run(() =>
                {
                    // These verifiers and processors would ideally be injected.
                    IFileVerifier quarterlyIssuedAwardsFileVerifier = new QuarterlyIssuedAwardsFileVerifier();
                    IFileProcessor<QuarterlyInventor> quarterlyIssuedAwardsFileProcessor = new QuarterlyIssuedAwardsFileProcessor(quarterlyIssuedAwardsFileVerifier);

                    IFileVerifier masterFileVerifier = new MasterIssuedAwardsFileVerifier();
                    IFileProcessor<MasterInventor> masterFileProcessor = new MasterIssuedAwardsFileProcessor(masterFileVerifier);

                    IFileVerifier inventorAddressFileVerifier = new InventorAddressFileVerifier();
                    IFileProcessor<InventorAddress> inventorAddressFileProcessor = new InventorAddressFileProcessor(inventorAddressFileVerifier);

                    IssuedInventorAwardsJob issuedAwardsJob = new(
                        quarterlyIssuedAwardsFileProcessor,
                        masterFileProcessor,
                        inventorAddressFileProcessor,
                        Log
                    );

                    issuedAwardsJob.QuarterlyFilePath = QuarterlyIssuedAwardsFile;
                    issuedAwardsJob.MasterFilePath = MasterFile;
                    issuedAwardsJob.InventorAddressFilePath = InventorAddressFile;
                    issuedAwardsJob.OutputFolder = OutputFolder;

                    issuedAwardsJob.ExecuteJob();
                });
                Log("Processing complete.");
            }
            catch (Exception ex)
            {
                Log($"An error occurred: {ex.Message}");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanProcessChineseAwards))]
        private async Task ProcessChineseAwardsAsync()
        {
            IsProcessing = true;
            Logs = string.Empty;
            Log("Starting Chinese inventor awards processing...");

            try
            {
                await Task.Run(() =>
                {
                    IFileVerifier chineseInventorFileVerifier = new ChineseInventorFileVerifier();
                    IFileProcessor<ChineseInventor> chineseIventorFileProcessor = new ChineseInventorFileProcessor(chineseInventorFileVerifier);

                    IssuedChineseInventorAwardsJob issuedAwardsJob = new(
                        chineseIventorFileProcessor,
                        Log
                    );

                    issuedAwardsJob.ChineseInventorFile = ChineseInventorFile;
                    issuedAwardsJob.OutputFolder = ChineseOutputFolder;
                    issuedAwardsJob.ExecuteJob();
                });
                Log("Processing complete.");
            }
            catch (Exception ex)
            {
                Log($"An error occurred: {ex.Message}");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        [RelayCommand]
        private void OpenLink(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Log($"Could not open link: {ex.Message}");
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using TMSApp.Services;
using Xamarin.Essentials;
using System.IO;

namespace TMSApp.ViewModels.Admin
{
    public class ReportSummaryViewModel : BaseViewModel
    {
        private readonly AdminService _adminService;
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        public ICommand DownloadUserSummaryCommand { get; }
        public ICommand DownloadTaskReportCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ReportSummaryViewModel()
        {
            _adminService = new AdminService();

            DownloadUserSummaryCommand = new Command(async () => await DownloadUserSummaryAsync());
            DownloadTaskReportCommand = new Command(async () => await DownloadTaskReportAsync());
            OpenFileCommand = new Command<string>(async (filePath) => await OpenFileAsync(filePath));

        }
        private async Task OpenFileAsync(string filePath)
        {
            try
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to open file: {ex.Message}", "OK");
            }
        }
        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        private string _userSummaryFilePath;
        public string UserSummaryFilePath
        {
            get => _userSummaryFilePath;
            set => SetProperty(ref _userSummaryFilePath, value);
        }

        private string _taskReportFilePath;
        public string TaskReportFilePath
        {
            get => _taskReportFilePath;
            set => SetProperty(ref _taskReportFilePath, value);
        }

        private async Task DownloadUserSummaryAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var fileData = await _adminService.GetUserSummaryFileAsync();
                if (fileData != null)
                {
                    var filePath = SaveFileLocally("UserSummary.csv", fileData);
                    await Application.Current.MainPage.DisplayAlert("Success", $"User Summary downloaded to {filePath}", "OK");
                    await OpenFileAsync(filePath);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to generate User Summary.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DownloadTaskReportAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var fileData = await _adminService.GetTaskReportFileAsync();
                if (fileData != null)
                {
                    var filePath = SaveFileLocally("TaskReport.csv", fileData);
                    await Application.Current.MainPage.DisplayAlert("Success", $"Task Report downloaded to {filePath}", "OK");
                    await OpenFileAsync(filePath);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to generate Task Report.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private string SaveFileLocally(string fileName, byte[] fileData)
        {
            string filePath;

            if (Device.RuntimePlatform == Device.Android)
            {
                filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            }
            else
            {
                throw new PlatformNotSupportedException("File saving is not supported on this platform.");
            }

            File.WriteAllBytes(filePath, fileData);
            return filePath;
        }
    }
}

using System;
using Spikes.ViewModel;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Spikes
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private MainViewModel MainViewModel
        {
            get
            {
                var mainViewModel = DataContext as MainViewModel;
                return mainViewModel;
            }
        }

        private async void OnTestClick(object sender, RoutedEventArgs e)
        {
            //var sampleFile = await Windows.Storage.PathIO.ReadBufferAsync(@"D:\Skydrive\Comics\Ultimatum\Ultimatum\15 - Ultimate Fantastic Four 059.cbr");

            var picker = new FolderPicker { ViewMode = PickerViewMode.List, SuggestedStartLocation = PickerLocationId.ComputerFolder };
            picker.FileTypeFilter.Add(".cbr");
            picker.FileTypeFilter.Add(".cbz");

            var folder = await picker.PickSingleFolderAsync();

            //if (folder == null) return;

            //var folders = folder.GetFoldersAsync();

            //foreach (var currentFolder in folders)
            //{

            //}

            //Windows.Storage.FileIO.ReadTextAsync(StoreageFile)


        }

        private async void OnFileSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            MainViewModel.FileSelectedCommand.Execute(null);
        }

        private void OnFolderSelected(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel.FolderSelectedCommand.Execute(null);
        }

        private async void OnLoadImage(IRandomAccessStream imageStream)
        {
            var image = new WriteableBitmap(1, 1);

            image.SetSource(imageStream);

            ComicImage.Source = image;

            ComicImage.Width = 400;
            ComicImage.Height = 300;

            imageStream.Dispose();
        }

        private void OnMainViewLoaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.LoadImageFile += OnLoadImage;
        }

        /// <summary>
        ///     Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e"> Event data that describes how this page was reached. The Parameter property is typically used to configure the page. </param>
        protected override void OnNavigatedTo(NavigationEventArgs e) {}
    }
}
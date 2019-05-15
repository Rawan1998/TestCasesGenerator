using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestCasesGenerator.Console;

namespace TestCasesGenerator.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private string _filePath;

        public MainWindow()
        {
            this._filePath = null;
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void FileUploadDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                this._filePath = filePath;
                (this.DataContext as MainWindowViewModel).GenerateTests(filePath);
            }
        }

        private void FileUploadClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                this._filePath = filePath;
                (this.DataContext as MainWindowViewModel).GenerateTests(filePath);
            }
        }

        private async void CustomTest(object sender, RoutedEventArgs e)
        {
            if (this._filePath == null)
            {
                return;
            }

            var runner = CustomTestRunner.Instance;
            await Task.Run(() => runner.RunCustomTest(this._filePath));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using MyPhotoAI.Classes;
using Newtonsoft.Json;

namespace MyPhotoAI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = $"Image files (*.png; *.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
      openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
      
      if (openFileDialog.ShowDialog()==true)
      {
        string fileName = openFileDialog.FileName;
        this.selectedImage.Source = new BitmapImage(new Uri(fileName));

        this.GetMoviesAsync();
      }
    }

    private async void GetMoviesAsync()
    {
      string api_key = "55ad3422681300f02641616898bdbee0";
      string url = $"https://api.themoviedb.org/3/search/movie?api_key={api_key}&language=en-US&query=Kissing%20booth&page=1&include_adult=false";

      using (HttpClient client = new HttpClient())
      {
        var response = await client.GetAsync(url);

        var responseString = await response.Content.ReadAsStringAsync();

        Macika movieDetails = JsonConvert.DeserializeObject<Macika>(responseString);
        this.mylistview.ItemsSource = movieDetails.results;
      }

    }
  }
}

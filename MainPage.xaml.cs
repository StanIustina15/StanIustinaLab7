namespace StanIustinaLab7
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void OnDiscoverMoreClicked(object sender, EventArgs e)
        {
            // Afișează un mesaj de tip alertă
            DisplayAlert("Descoperă mai multe", "To be continued...", "OK");
        }
    }
}
       



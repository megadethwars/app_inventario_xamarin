using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresarProducto : ContentPage
    {
        public string barcodetext;
        public String texto;
        public string foto = string.Empty;
        public IngresarProducto()
        {
            InitializeComponent();
            butt_foto.Clicked += Button_Clicked;
           
            //Content = butt_foto;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            search.Text = barcodetext;
            if(!(foto==string.Empty))
            {
                imagen.Source = foto;
            }
        }
        

        private void SearchBar(object sender, EventArgs e)
        {

        }

        private void Scan(object sender, EventArgs e)
        {
            //Declarada en Inventario Principal
            Navigation.PushAsync(new Escanear4(this));
        }

        private void ScanFotos(object sender, EventArgs e)
        {

        }

        private void ReingresaP(object sender, EventArgs e)
        {

        }
        async void Button_Clicked(object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ": (No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
               new Plugin.Media.Abstractions.StoreCameraMediaOptions
               {
                   Directory = "Sample",

                   Name = "test.jpg"
               });
            if (file == null)
                return;
            await DisplayAlert("File Location", file.Path, "OK");
            imagen.Source = file.Path;
            foto = file.Path;
        }
    }
}
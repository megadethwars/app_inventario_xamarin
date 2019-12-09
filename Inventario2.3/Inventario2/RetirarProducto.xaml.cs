using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RetirarProducto : ContentPage
    {
        public List<Plugin.Media.Abstractions.MediaFile> f1 = new List<Plugin.Media.Abstractions.MediaFile>();
        Plugin.Media.Abstractions.MediaFile f;
        string p;
        List<InventDB> users1;
        public List<Movimientos> mv = new List<Movimientos>();
        
        public string text;
        public RetirarProducto()
        {
            InitializeComponent();
            ScanFotosOut.Clicked += Button_Clicked;
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            search.Text = text;
            p = Guid.NewGuid().ToString("D");
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

             f = await CrossMedia.Current.TakePhotoAsync(
               new Plugin.Media.Abstractions.StoreCameraMediaOptions
               {
                   Directory = "Sample",

                   Name = "prueba.jpg"
               });
            if (f == null)
                return;
            await DisplayAlert("", "Foto Exitosa", "OK");
            imagen.Source = f.Path;
            
        }

        private async void SearchBar(object sender, EventArgs e)
        {
            var isNumeric = long.TryParse(search.Text, out long n);
            


            if (!isNumeric)
            {
                //SQLiteConnection conn = new SQLiteConnection(App.DtabaseLocation);
                //conn.CreateTable<InventDB>();
                //var users1 = conn.Query<InventDB>("select * from InventDB where Nombre= ?", search.Text);
                //conn.Close();
                 users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.Nombre == search.Text).ToListAsync();
                if (users1.Count == 1)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    nombreTxt.Text = users1[0].Nombre;
                    modelotxt.Text = users1[0].marca;
                    cantidadtxt.Text = users1[0].cantidad;
                }
                else
                {
                    DisplayAlert("Buscando", "Producto no encontrado", "Aceptar");
                    
                }
            }
            else
            {

                users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.Codigo == search.Text).ToListAsync();
                if (users1.Count != 0)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    nombreTxt.Text = users1[0].Nombre;
                    modelotxt.Text = users1[0].marca;
                    cantidadtxt.Text = users1[0].cantidad;

                }
                else
                {
                    DisplayAlert("Buscando", " no encontrado", "OK");
                    
                }
            }
        }

        private void Scan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear2(this));
            //Declarada en inventario Principal
        }
       



        private void RetiraP(object sender, EventArgs e)
        {
            var isNumeric = int.TryParse(cantidadtxt.Text, out int n);
            var Numeric = int.TryParse(cantEntry.Text, out int x);
            if (x>n)
            {
                DisplayAlert("Error", "Cantidad Excedida", "Aceptar");
            }
            else
            {
                Movimientos mv1 = new Movimientos
                {
                    ID = p,
                    observ = editorTxt.Text,
                    producto = users1[0].Nombre,
                    marca = users1[0].marca,
                    modelo = users1[0].modelo,
                    IdProducto = users1[0].ID,
                    cantidad = x.ToString(),
                    foto = p+".jpg",
                    movimiento = "Retirar",
                    lugar = lugarEntry.Text,
                    fecha = DateTime.Now.ToString("dd/MM/yyyy")
                };
                mv.Add(mv1);
                f1.Add(f);
                f = null;
                Navigation.PushAsync(new Carrito(this));
            }

        }

     
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carrito(this));
        }
    }
}
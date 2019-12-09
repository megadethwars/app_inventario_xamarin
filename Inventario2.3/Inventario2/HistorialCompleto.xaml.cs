using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialCompleto : ContentPage
    {
        public string scantext;
        public HistorialCompleto()
        {
            InitializeComponent();
            desde.Date = DateTime.Now;
            desde.MinimumDate = new DateTime(2000, 1, 1);
            desde.MaximumDate = desde.Date;
            desde.DateSelected += desde_DateSelected;

            hasta.Date = DateTime.Now;
            hasta.MinimumDate = new DateTime(2000, 1, 1);
            hasta.MaximumDate = hasta.Date;
            hasta.DateSelected += hasta_DateSelected;
            
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchbar.Text = scantext;
        }

        private void desde_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void hasta_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private async void IncludeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (includeSwitch.IsToggled)
            {
                TimeSpan span = hasta.Date - desde.Date;
                DateTime s = hasta.Date;
                
                List<Movimientos> fechas = new List<Movimientos>();
                for (int x = 0; x <= span.TotalDays; x++)
                {
                    var yesterday = s.AddDays(-x);
                    var fecha = await App.MobileService.GetTable<Movimientos>().Where(u => u.fecha == yesterday.ToString("dd/MM/yyyy")).ToListAsync();
                    for(int y =0;y<fecha.Count();y++)
                    {
                        fechas.Add(fecha[y]);
                    }
                    postListView.ItemsSource = fechas;
                }
            }
        }

        private async void Search_SearchButtonPressed(object sender, EventArgs e)
        {
            var isNumeric = long.TryParse(searchbar.Text, out long n);



            if (!isNumeric)
            {
                //SQLiteConnection conn = new SQLiteConnection(App.DtabaseLocation);
                //conn.CreateTable<InventDB>();
                //var users1 = conn.Query<InventDB>("select * from InventDB where Nombre= ?", search.Text);
                //conn.Close();
                var users1 = await App.MobileService.GetTable<Movimientos>().Where(u => u.producto == searchbar.Text).ToListAsync();
                if (users1.Count != 0)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    DisplayAlert("Buscando", "Producto no encontrado", "Aceptar");
                    postListView.ItemsSource = users1;
                }
            }
            else
            {

                var users1 = await App.MobileService.GetTable<Movimientos>().Where(u => u.IdProducto == searchbar.Text).ToListAsync();
                if (users1.Count != 0)
                {
                    //DisplayAlert("Buscando", "encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
                else
                {
                    DisplayAlert("Buscando", " no encontrado", "OK");
                    postListView.ItemsSource = users1;
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear5(this));
        }

        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Movimientos;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesHistorial(selectedPost));
        }
    }
}
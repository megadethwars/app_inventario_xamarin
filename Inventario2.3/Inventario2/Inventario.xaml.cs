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
    public partial class Inventario : ContentPage
    {
        //string codigo;
        public List<InventDB> users;
        public string stringcode;
        public Inventario()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            search.Text = stringcode;
          
          
            var usuarios = await App.MobileService.GetTable<InventDB>().ToListAsync();
            
           
            postListView.ItemsSource = usuarios;
        }

        
        private void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as InventDB;
            if (selectedPost != null)
                Navigation.PushAsync(new DetallesProducto(selectedPost));
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
                var users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.Nombre == search.Text).ToListAsync();
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

                var users1 = await App.MobileService.GetTable<InventDB>().Where(u => u.Codigo == search.Text).ToListAsync();
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

        private void Scan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Escanear(this));
        }

        private async void MenuOp(object sender, EventArgs e)
        { //Despegar menu de  3 opciones Ingresar, Retirar, Detalles
            string res = await DisplayActionSheet("Opciones", "Cancelar", null, "Agregar Nuevo Producto", "Reingresar Producto", "Retirar Producto");
            switch (res)
            {
                case "Agregar Nuevo Producto":
                    //Abrir vista/pagina Detalles del Producto
                    Navigation.PushAsync(new NuevoProducto());
                    break;
                case "Reingresar Producto":
                    //Abrir vista/pagina Ingresar Producto
                    Navigation.PushAsync(new IngresarProducto(  ));
                    break;
                case "Retirar Producto":
                    //Abrir vista/pagina Retirar Producto
                    Navigation.PushAsync(new RetirarProducto());
                    break;

                
            }
        }
    }
}
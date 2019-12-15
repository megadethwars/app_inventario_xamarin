using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class DetallesProducto : ContentPage
    {
        InventDB n;
        public DetallesProducto(InventDB db)
        {
            InitializeComponent();
            this.n = db;
            nameProd.Text = n.Nombre;
            idCodigo.Text = n.Codigo;
            idcantidad.Text = n.cantidad;
            idlugar.Text = n.lugar;
            idobserv.Text = n.observaciones;
            idProd.Text = n.ID.ToString();
            idmarca.Text = n.marca;
            idFecha.Text = n.Fecha;
            idmodelo.Text = n.modelo;
            imagen.Source = "https://fotosavs.blob.core.windows.net/fotosinventario/"+n.foto;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("¡Estas a punto de eliminar un Producto!, ¿Deseas continuar?", "Cancelar", null, "Eliminar Producto");
            switch (res)
            {
                case "Eliminar Producto":
                    //Eliminar empleado
                    try
                    {
                        await App.MobileService.GetTable<InventDB>().DeleteAsync(n);


                        await DisplayAlert("Hecho", "Producto borrado exitosamente", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {


                       await  DisplayAlert("Error", "Error al borrar el producto", "Aceptar");

                    }
                        break;
                    
            }
        }
    }
}
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
    public partial class DetallesHistorial : ContentPage
    {
        public DetallesHistorial(Movimientos mv)
        {
            InitializeComponent();
            nameUser.Text = mv.usuario;
            nameProd.Text = mv.producto;
            idProd.Text = mv.IdProducto;
            idmarca.Text = mv.marca;
            idmodelo.Text = mv.modelo;
            idcantidad.Text = mv.cantidad;
            idobserv.Text = mv.observ;
            idlugar.Text = mv.lugar;
            idMove.Text = mv.movimiento;
            idFecha.Text = mv.fecha;
            imagen.Source = "https://fotosavs.blob.core.windows.net/fotosinventario/"+mv.foto;

        }
    }
}
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
    public partial class DetallesCarrito : ContentPage
    {
        public RetirarProducto ca;
        public DetallesCarrito(Movimientos m,RetirarProducto r)
        {
            InitializeComponent();
            nameProd.Text = m.producto;
            marcatxt.Text = m.marca;
            modeltxt.Text = m.modelo;
            cantidadtxt.Text = m.cantidad;
            observtxt.Text = m.observ;
            ca = r;
        }

        
    }
}
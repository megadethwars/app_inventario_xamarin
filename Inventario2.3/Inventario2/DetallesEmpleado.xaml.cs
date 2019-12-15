using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventario2.Model;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetallesEmpleado : ContentPage
    {
        Usuario usuario;
        public DetallesEmpleado(Usuario user)
        {
            InitializeComponent();
            this.usuario = user;
            nameEmp.Text = usuario.nombre + " " + usuario.apellido_paterno+" "+usuario.apellido_materno;
            idEmpAsig.Text = usuario.ID.ToString();
            fechaCont.Text = usuario.fechaContratacion;
            tipoUs.Text = usuario.tipoUsuario;
            telText.Text = usuario.telefono;
            correotext.Text = usuario.correo;
        }

        private async void EliminaEmp(object sender, EventArgs e)
        {
            string res = await DisplayActionSheet("¡Estas a punto de eliminar un Empleado!, ¿Deseas continuar?", "Cancelar", null, "Eliminar Empleado");
            switch (res)
            {
                case "Eliminar Empleado":
                    //Eliminar empleado
                    try
                    {
                        await App.MobileService.GetTable<Usuario>().DeleteAsync(usuario);


                        await DisplayAlert("Hecho", "Producto borrado exitosamente", "Aceptar");
                        await Navigation.PopAsync();
                    }
                    catch (MobileServiceInvalidOperationException ms)
                    {


                        await DisplayAlert("Error", "Error al borrar el producto", "Aceptar");

                    }
                    break;

            }
        }

    }
}
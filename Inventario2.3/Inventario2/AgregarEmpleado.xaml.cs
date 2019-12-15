using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Inventario2.Model;

using Microsoft.WindowsAzure.MobileServices;

namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarEmpleado : ContentPage
    {
        public string identi = Guid.NewGuid().ToString();
        public string tipousuario;
        public AgregarEmpleado()
        {
            InitializeComponent();
            
        }
        private void GenerateID(object sender, EventArgs e)
        {//Generar ID usando Data Binding y asignarlo a la variable idEmp

            //var idEmpleado = 04236; 
            //idEmp.Text= idEmpleado.ToString();
        }

        private async void AgregaEmp(object sender, EventArgs e)
        {
            if (contra2.Text == contraEntry.Text)
            {

                Usuario user = new Usuario
                {
                    ID = identi,
                    nombre = nombrEntry.Text,
                    contrasena = contraEntry.Text,
                    apellido_paterno = apepEntry.Text,
                    apellido_materno = apemEntry.Text,
                    tipoUsuario = tipousuario,
                    telefono = telEntry.Text,
                    correo = correoEntry.Text,
                    fechaContratacion = DateTime.Now.ToString("dd/MM/yyyy")
                };
                try
                {
                    await App.MobileService.GetTable<Usuario>().InsertAsync(user);
                    //UploadFile(f.GetStream());
                    await DisplayAlert("Agregado", "Usuario agregado correctamente", "Aceptar");
                    await Navigation.PopAsync();


                }
                catch (MobileServiceInvalidOperationException ms)
                {
                    var response = await ms.Response.Content.ReadAsStringAsync();
                    await DisplayAlert("error", response, "Aceptar");
                }
            }
            else
                DisplayAlert("Error","Contraseña no coincide","Aceptar");
        }

        private void PickerUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipousuario = pickerUser.SelectedItem as string;
        }
    }
}
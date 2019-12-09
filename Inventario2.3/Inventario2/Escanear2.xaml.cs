using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
namespace Inventario2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Escanear2 : ZXingScannerPage
    {
        RetirarProducto s;
        public Escanear2(RetirarProducto r)
        {
            InitializeComponent();
            s = r;
        }

        public void ScanPage(ZXing.Result result)
        {
            Boolean x;
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await DisplayAlert("Scanned result", result.Text, "OK");
                s.text = result.Text;
                await Navigation.PopAsync();
                //await DisplayAlert("","","oooo");
            });
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsScanning = false;
        }
    }
}
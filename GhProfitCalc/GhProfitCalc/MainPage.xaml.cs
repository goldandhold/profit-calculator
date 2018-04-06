using GhProfitCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GhProfitCalc
{
	public partial class MainPage : ContentPage
	{
        public MainPageViewModel ViewModel { get; set; }
		public MainPage()
		{
			InitializeComponent();

            // Instantiates our viewmodel
            ViewModel = new MainPageViewModel();

            // Sets the Binding Context to our page which gives
            // the view ability to bind to our public properties such
            // as our ViewModel
            this.BindingContext = this;
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maoui.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavigationSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationFirstPage : ContentPage
	{
		public NavigationFirstPage ()
		{
			this.InitializeComponent();          
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            (this.Parent as NavigationPage).PushAsync(new NavigationSecondPage());
        }
    }
}

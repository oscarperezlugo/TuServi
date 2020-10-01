﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediosdePago : ContentPage
    {
        string totalcompra;

        public MediosdePago()
        {
            InitializeComponent();

            totalcompra = SecureStorage.GetAsync("totalcompra").ToString();
        }
    }
}
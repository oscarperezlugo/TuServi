using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TuServi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroEmpresa : CarouselPage
    {
        public RegistroEmpresa()
        {
            InitializeComponent();
        }
        public void Siguiente_Clicked(object sender, EventArgs e)
        {
            CurrentPage = paso2;

        }
    }
}
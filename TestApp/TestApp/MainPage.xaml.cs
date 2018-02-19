using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    public partial class MainPage : TabbedPage
    {
        private static MainPage instance;
        public MainPage()
        {
            InitializeComponent();
            
        }
        public static MainPage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainPage();
                }

                return instance;
            }
        }
    }
}

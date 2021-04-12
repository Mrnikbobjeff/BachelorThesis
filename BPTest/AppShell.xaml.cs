using System;
using BPTest.Views;
using Xamarin.Forms;

namespace BPTest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}

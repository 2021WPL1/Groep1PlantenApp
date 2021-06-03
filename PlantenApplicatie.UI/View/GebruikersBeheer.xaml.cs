using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlantenApplicatie.UI.View
{
    /// <summary>
    /// Interaction logic for GebruikersBeheer.xaml
    /// </summary>
    public partial class GebruikersBeheer : Window
    {
        private GebruikersBeheerViewModel viewModel;
        public GebruikersBeheer(Gebruiker gebruiker)
        {
            viewModel = new GebruikersBeheerViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            //  viewModel.ShowAllUser();
            viewModel.LoadLoggedInUser(gebruiker);
            viewModel.EnableRolButtons();
            InitializeComponent();
            

        }
        // hemen &maarten 
        //in de plaats van refresh
        protected override void OnActivated(EventArgs eventArgs)
        {
            base.OnActivated(eventArgs);
            viewModel.ShowAllUser();
        }

    }
}

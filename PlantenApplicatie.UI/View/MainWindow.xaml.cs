using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.ViewModel;

namespace PlantenApplicatie.UI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Nieuwe context aanmaken
        private static Planten2021Context context = new Planten2021Context();
        private MainViewModel viewModel;

        public MainWindow(Gebruiker gebruiker)
        {
            InitializeComponent();
            //Senne,Maarten,Hermes
            //mainviewmodel
            viewModel = new MainViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.LoadLoggedInUser(gebruiker);
            viewModel.EnableRolButtons();
            viewModel.InitializeTfgsv();
            viewModel.LoadAll();
        }

      
    }
}
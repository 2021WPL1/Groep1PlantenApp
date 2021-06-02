using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.ViewModel;


namespace PlantenApplicatie.UI.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private EditViewModel viewModel;

        public EditWindow(Plant plant)
        {//voor edit -> plant meegeven
            //Senne & Hermes
            InitializeComponent();

            viewModel = new EditViewModel(PlantenDataService.Instance(),EditScherm);

            DataContext = viewModel;
            viewModel.InitializeAll();
            viewModel.FillDataFromPlant(plant);
        }
    }
}

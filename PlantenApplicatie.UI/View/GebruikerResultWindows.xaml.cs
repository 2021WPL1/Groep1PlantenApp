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
    /// Interaction logic for GebruikerResultWindows.xaml
    /// </summary>
    public partial class GebruikerResultWindows : Window
    {
        private GebruikerResultModel viewModel;
        public GebruikerResultWindows(Plant plant)
        {
            InitializeComponent();
            viewModel = new GebruikerResultModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.fillLabels(plant);
            viewModel.LoadLists();
        }
    }
}

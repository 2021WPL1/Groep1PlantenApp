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
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.ViewModel;

namespace PlantenApplicatie.UI.View
{
    //Maarten & Stephanie
    public partial class ResultatenWindow : Window
    {
        private ResultaatViewModel viewModel;
        public ResultatenWindow(Plant plant)
        {
            InitializeComponent();
            viewModel = new ResultaatViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.fillLabels(plant);
        }
    }
}

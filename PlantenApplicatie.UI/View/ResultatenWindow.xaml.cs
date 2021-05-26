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
    //Jelle & Hemen
    public partial class ResultatenWindow : Window
    {
        private ResultatenViewModel viewModel;
        public ResultatenWindow(Plant plant)
        {
            InitializeComponent();
            viewModel = new ResultatenViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.fillLabels(plant);
            viewModel.LoadList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

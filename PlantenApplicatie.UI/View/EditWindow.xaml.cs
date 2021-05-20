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
using PlantenApplicatie.UI.ViewModel;

namespace PlantenApplicatie.UI.View
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private static Planten2021Context context = new Planten2021Context();
        private EditViewModel viewModel;

        public EditWindow()
        {
            InitializeComponent();

            viewModel = new EditViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.InitializeAll();
        }
    }
}

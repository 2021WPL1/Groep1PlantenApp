using PlantenApplicatie.Data;
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
    /// Interaction logic for WachtwoordVergeten.xaml
    /// </summary>
    public partial class WachtwoordVergetenWindow : Window
    {
        private WachtwoordViewModel viewModel;
        public WachtwoordVergetenWindow()
        {
            InitializeComponent();
            viewModel = new WachtwoordViewModel(PlantenDataService.Instance());
        }
    }
}

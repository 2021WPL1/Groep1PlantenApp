﻿using PlantenApplicatie.Data;
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
    /// Interaction logic for CreateGebruiker.xaml
    /// </summary>
    public partial class CreateGebruiker : Window
    {
        private CreateGebruikerViewModel viewModel;
        public CreateGebruiker()
        {
            viewModel = new CreateGebruikerViewModel(PlantenDataService.Instance());
            DataContext = viewModel;
            viewModel.addRollen();
            InitializeComponent();
        }
    }
}

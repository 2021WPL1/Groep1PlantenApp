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
using PlantenApplicatie.DOMAIN.Models;


namespace PlantenApplicatie
{
    /// <summary>
    /// Interaction logic for ResultatenWindow.xaml
    /// </summary>
    public partial class ResultatenWindow : Window
    {
        public ResultatenWindow(Plant plant)
        {
            InitializeComponent();
            fillLabels(plant);

        }

        private void fillLabels(Plant plant)
        {
            lblLatinName.Content = plant.Fgsv;
            lblDutchName.Content = plant.NederlandsNaam;
            lblType.Content = plant.Type;
            lblFamily.Content = plant.Familie;
            lblGeslacht.Content = plant.Geslacht;
            lblSoort.Content = plant.Soort;
            lblVariant.Content = plant.Variant;
            lblPlantdichtheidMax.Content = plant.PlantdichtheidMax;
            lblPlantdichtheidMin.Content = plant.PlantdichtheidMin;
            lblStatus.Content = plant.Status;

        }

        private void btnAddToFavorite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

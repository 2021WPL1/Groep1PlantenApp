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
using PlantenApplicatie.DATA.Models;


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


        //Stephanie en Jelle    
        //Deze functie zorgt ervoor dat de labels waar de latijnse naam in moeten komen, verdeeld worden over 2 labels.
        //om zo de volledige naam duidelijk weer te geven.
        private void fillLabels(Plant plant)
        {
            bool enter = false;
            for (int i = 0; i < plant.Fgsv.Length; i++)
            {
                string letter = plant.Fgsv.Substring(i, 1);
                if (letter == " " && i >= 25 && enter == false)
                {
                    enter = true;
                }
                else
                {
                    if (enter != true)
                    {
                        lblLatinName.Content += letter;
                    }
                    else
                    {
                        lblLatinName2.Content += letter;
                    }
                }
            }
            if (plant.Type != null)
            {
                lblType.Content = plant.Type;
            }
            else
            {
                lblType.Content = " ";
            }

            lblType.Content = plant.Type;
            lblFamily.Content = plant.Familie;
            lblGeslacht.Content = plant.Geslacht;
            lblSoort.Content = plant.Soort;
            lblVariant.Content = plant.Variant;
            lblPlantdichtheidMax.Content = plant.PlantdichtheidMax;
            lblPlantdichtheidMin.Content = plant.PlantdichtheidMin;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

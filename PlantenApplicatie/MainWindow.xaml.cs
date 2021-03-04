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
using PlantenApplicatie.DATA.Models;

namespace PlantenApplicatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Planten2021Context context = new Planten2021Context();
        public MainWindow()
        {
            InitializeComponent();
            addItemsToComboBox(cbxType, "Type");
            addItemsToComboBox(cbxFamilie, "Familie");
            addItemsToComboBox(cbxVariant, "Variant");
            addItemsToComboBox(cbxSoort, "Soort");
            addItemsToComboBox(cbxGeslacht, "Geslacht");

        }

        public void addItemsToComboBox(ComboBox plant, string item)
        {
            switch (item)
            {
                case "Type":
                    var types = context.TfgsvType.ToList();
                    foreach (TfgsvType type in types)
                    {
                        plant.Items.Add(type.Planttypenaam);
                    }
                    break;
                case "Familie":
                    var families = context.TfgsvFamilie.ToList();
                    foreach (TfgsvFamilie familie in families)
                    {
                        plant.Items.Add(familie.Familienaam);
                    }
                    break;
                case "Variant":
                    var varianten = context.TfgsvVariant.ToList();
                    foreach (TfgsvVariant variant in varianten)
                    {
                        plant.Items.Add(variant.Variantnaam);
                    }
                    break;
                case "Soort":
                    var soorten = context.TfgsvSoort.ToList();
                    foreach (TfgsvSoort soort in soorten)
                    {
                        plant.Items.Add(soort.Soortnaam);
                    }
                    break;
                case "Geslacht":
                    var geslachten = context.TfgsvGeslacht.ToList();
                    foreach (TfgsvGeslacht geslacht in geslachten)
                    {
                        plant.Items.Add(geslacht.Geslachtnaam);
                    }
                    break;

                default:
                    break;
            }


        }

        public void addItemsTocbxSoort()
        {

        }

        public void addItemsTocbGeslacht()
        {

        }

        public void addItemsTocbxVariant()
        {

        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxFamilie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

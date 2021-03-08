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
            //Jelle
            InitializeComponent();
            addItemsToComboBox(cbxType);
            addItemsToComboBox(cbxFamilie);
            addItemsToComboBox(cbxVariant);
            addItemsToComboBox(cbxSoort);
            addItemsToComboBox(cbxGeslacht);

        }

        public void addItemsToComboBox(ComboBox plant)
        {
            //Jelle
            switch (plant.Name)
            {
                case "cbxType":
                    var types = context.TfgsvType.ToList();
                    foreach (TfgsvType type in types)
                    {
                        plant.Items.Add(type.Planttypenaam);
                    }
                    break;
                case "cbxFamilie":
                    var families = context.TfgsvFamilie.ToList();
                    foreach (TfgsvFamilie familie in families)
                    {
                        plant.Items.Add(familie.Familienaam);
                    }
                    break;
                case "cbxVariant":
                    var varianten = context.TfgsvVariant.ToList();
                    foreach (TfgsvVariant variant in varianten)
                    {
                        plant.Items.Add(variant.Variantnaam);
                    }
                    break;
                case "cbxSoort":
                    var soorten = context.TfgsvSoort.ToList();
                    foreach (TfgsvSoort soort in soorten)
                    {
                        plant.Items.Add(soort.Soortnaam);
                    }
                    break;
                case "cbxGeslacht":
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

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            if (cbxType.SelectedItem != null)
            {
                ClearItems(cbxFamilie);
                foreach (TfgsvFamilie familie in context.TfgsvFamilie.ToList())
                {
                    var selectedType = context.TfgsvType.FirstOrDefault(s => s.Planttypenaam == cbxType.SelectedItem.ToString());
                    if (selectedType.Planttypeid == familie.TypeTypeid)
                    {
                        lstResult.Items.Add(familie.Familienaam);

                        cbxFamilie.Items.Add(familie.Familienaam);
                    }
                    else
                    {

                    }
                }
            }
            
        }
        private void cbxFamilie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            if (cbxFamilie.SelectedItem != null)
            {
                ClearItems(cbxGeslacht);
                foreach (TfgsvGeslacht geslacht in context.TfgsvGeslacht.ToList())
                {
                    var selectedFamilie = context.TfgsvFamilie.FirstOrDefault(s => s.Familienaam == cbxFamilie.SelectedItem.ToString());
                    if (selectedFamilie.FamileId == geslacht.FamilieFamileId)
                    {
                        lstResult.Items.Add(geslacht.Geslachtnaam);     
                        cbxGeslacht.Items.Add(geslacht.Geslachtnaam);
                    }
                }
            }
        }
        private void cbxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Jelle
            if (cbxSoort.SelectedItem != null)
            {
                ClearItems(cbxVariant);
                foreach (TfgsvVariant variant in context.TfgsvVariant.ToList())
                {
                    var selectedSoort = context.TfgsvSoort.FirstOrDefault(s => s.Soortnaam == cbxSoort.SelectedItem.ToString());
                    if (selectedSoort.Soortid == variant.SoortSoortid)
                    {
                        lstResult.Items.Add(variant.Variantnaam);
                        cbxVariant.Items.Add(variant.Variantnaam);
                    }
                    
                }
            }
        }

        

        private void cbxVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ClearItems(ComboBox comboBox)
        {
            lstResult.Items.Clear();
            comboBox.Items.Clear();
        }

        private void cbxGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hemen
            if (cbxGeslacht.SelectedItem != null)
            {
                ClearItems(cbxSoort);
                foreach (TfgsvSoort soort in context.TfgsvSoort.ToList())
                {
                    var selectedgeslacht = context.TfgsvGeslacht.FirstOrDefault
                            (s => s.Geslachtnaam == cbxGeslacht.SelectedItem.ToString());
                    if (selectedgeslacht.GeslachtId == soort.GeslachtGeslachtId)
                    {
                        lstResult.Items.Add(soort.Soortnaam);
                        cbxSoort.Items.Add(soort.Soortnaam);
                    }
                }
            }
        }

        private void btnStartOpnieuw_Click(object sender, RoutedEventArgs e)
        {
            lstResult.Items.Clear();
            cbxType.Items.Clear();
            cbxFamilie.Items.Clear();
            cbxVariant.Items.Clear();
            cbxSoort.Items.Clear();
            cbxGeslacht.Items.Clear();

            addItemsToComboBox(cbxType);
            addItemsToComboBox(cbxFamilie);
            addItemsToComboBox(cbxVariant);
            addItemsToComboBox(cbxSoort);
            addItemsToComboBox(cbxGeslacht);

            addItemsToComboBox(cbxGeslacht, "Geslacht");
        }
    }
}

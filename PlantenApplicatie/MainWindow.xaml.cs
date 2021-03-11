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
using PlantenApplicatie.DOMAIN.Models;

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
            List<string> cboItems = new List<string>();
            switch (plant.Name)
            {
                case "cbxType":
                    var types = context.TfgsvType.ToList();
                    foreach (TfgsvType type in types)
                    {
                        cboItems.Add(type.Planttypenaam);
                    }
                    break;
                case "cbxFamilie":
                    var families = context.TfgsvFamilie.ToList();
                    foreach (TfgsvFamilie familie in families)
                    {
                        cboItems.Add(familie.Familienaam);
                    }
                    break;
                case "cbxVariant":
                    var varianten = context.TfgsvVariant.ToList();
                    foreach (TfgsvVariant variant in varianten)
                    {
                        cboItems.Add(variant.Variantnaam);
                    }
                    break;
                case "cbxSoort":
                    var soorten = context.TfgsvSoort.ToList();
                    foreach (TfgsvSoort soort in soorten)
                    {
                        cboItems.Add(soort.Soortnaam);
                    }
                    break;
                case "cbxGeslacht":
                    var geslachten = context.TfgsvGeslacht.ToList();
                    foreach (TfgsvGeslacht geslacht in geslachten)
                    {
                        cboItems.Add(geslacht.Geslachtnaam);
                    }
                    break;
                default:
                    break;
            }
            cboItems.Sort();
            cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());
            foreach (string item in cboItems)
            {
                plant.Items.Add(item);
            }
        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            if (cbxType.SelectedItem != null)
            {
                lstResult.Items.Clear();
                cbxFamilie.Items.Clear();
                cbxGeslacht.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();

                var selectedType = context.TfgsvType.FirstOrDefault(s => s.Planttypenaam == cbxType.SelectedItem.ToString());
                var selectedFamilie = context.TfgsvFamilie.FirstOrDefault(s => s.TypeTypeid == selectedType.Planttypeid);
                var selectedgeslacht = context.TfgsvGeslacht.FirstOrDefault(s => s.FamilieFamileId == selectedFamilie.FamileId);
                var selectedSoort = context.TfgsvSoort.FirstOrDefault(s => s.GeslachtGeslachtId == selectedgeslacht.GeslachtId);
                foreach (TfgsvFamilie familie in context.TfgsvFamilie.ToList())
                {
                    if (selectedType.Planttypeid == familie.TypeTypeid)
                    {
                        cbxFamilie.Items.Add(familie.Familienaam);
                    }
                }
                foreach (TfgsvGeslacht geslacht in context.TfgsvGeslacht.ToList())
                {
                    if (selectedFamilie.FamileId == geslacht.FamilieFamileId)
                    {

                        cbxGeslacht.Items.Add(geslacht.Geslachtnaam);
                    }

                }
                foreach (TfgsvSoort soort in context.TfgsvSoort.ToList())
                {
                    if (selectedgeslacht.GeslachtId == soort.GeslachtGeslachtId)
                    {
                        cbxSoort.Items.Add(soort.Soortnaam);
                    }
                }
                foreach (TfgsvVariant variant in context.TfgsvVariant.ToList())
                {
                    if (selectedSoort.Soortid == variant.SoortSoortid)
                    {
                        cbxVariant.Items.Add(variant.Variantnaam);
                    }
                }
                searchResults();
            }
        }

        private void cbxFamilie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            if (cbxFamilie.SelectedItem != null)
            {
                lstResult.Items.Clear();
                cbxGeslacht.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();

                var selectedFamilie = context.TfgsvFamilie.FirstOrDefault(s => s.Familienaam == cbxFamilie.SelectedItem.ToString());
                var selectedgeslacht = context.TfgsvGeslacht.FirstOrDefault(s => s.FamilieFamileId == selectedFamilie.FamileId);
                var selectedSoort = context.TfgsvSoort.FirstOrDefault(s => s.GeslachtGeslachtId == selectedgeslacht.GeslachtId);
                foreach (TfgsvGeslacht geslacht in context.TfgsvGeslacht.ToList())
                {
                    if (selectedFamilie.FamileId == geslacht.FamilieFamileId)
                    {
                        cbxGeslacht.Items.Add(geslacht.Geslachtnaam);
                    }
                }
                foreach (TfgsvSoort soort in context.TfgsvSoort.ToList())
                {
                    if (selectedgeslacht.GeslachtId == soort.GeslachtGeslachtId)
                    {
                        cbxSoort.Items.Add(soort.Soortnaam);
                    }
                }
                foreach (TfgsvVariant variant in context.TfgsvVariant.ToList())
                {
                    if (selectedSoort.Soortid == variant.SoortSoortid)
                    {
                        cbxVariant.Items.Add(variant.Variantnaam);
                    }
                }
                searchResults();
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
                        cbxVariant.Items.Add(variant.Variantnaam);
                    }
                }
                searchResults();
            }
        }

        private void cbxVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stephanie, Senne
            if (cbxVariant.SelectedItem != null)
            {
                lstResult.Items.Clear();
                var selectedVariant = context.TfgsvVariant.First(v => v.Variantnaam == cbxVariant.SelectedItem.ToString());
            }
        }
        private void ClearItems(ComboBox comboBox)
        {
            //Stephanie
            lstResult.Items.Clear();
            comboBox.Items.Clear();
        }

        private void cbxGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Hemen
            if (cbxGeslacht.SelectedItem != null)
            {
                lstResult.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();
                var selectedgeslacht = context.TfgsvGeslacht.FirstOrDefault(s => s.Geslachtnaam == cbxGeslacht.SelectedItem.ToString());
                var selectedSoort = context.TfgsvSoort.FirstOrDefault(s => s.GeslachtGeslachtId == selectedgeslacht.GeslachtId);
                foreach (TfgsvSoort soort in context.TfgsvSoort.ToList())
                {
                    if (selectedgeslacht.GeslachtId == soort.GeslachtGeslachtId)
                    {
                        cbxSoort.Items.Add(soort.Soortnaam);
                    }
                }
                foreach (TfgsvVariant variant in context.TfgsvVariant.ToList())
                {
                    if (selectedSoort.Soortid == variant.SoortSoortid)
                    {
                        cbxVariant.Items.Add(variant.Variantnaam);
                    }
                }
                searchResults();
            }
        }

        private void btnStartOpnieuw_Click(object sender, RoutedEventArgs e)
        {
            //jelle & maarten
            lstResult.Items.Clear();
            cbxFamilie.Items.Clear();
            cbxGeslacht.Items.Clear();
            cbxSoort.Items.Clear();
            cbxType.Items.Clear();
            cbxVariant.Items.Clear();

            addItemsToComboBox(cbxType);
            addItemsToComboBox(cbxFamilie);
            addItemsToComboBox(cbxVariant);
            addItemsToComboBox(cbxSoort);
            addItemsToComboBox(cbxGeslacht);

            txtSearchbox.Clear();
        }
        //Maarten, Hemen & Jelle
        private void searchResults()
        {
            List<Plant> searchResults = context.Plant.ToList();
            List<Plant> SearchResultsCopy = new List<Plant>();
            if (cbxType.SelectedItem != null)
            {
                var selectedType = context.TfgsvType.FirstOrDefault(s => s.Planttypenaam == cbxType.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults.Clear();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedType.Planttypenaam.ToString() == plant.Type)
                    {
                        searchResults.Add(plant);
                    }
                }
            }
            if (cbxFamilie.SelectedItem != null)
            {
                var selectedFamilie = context.TfgsvFamilie.FirstOrDefault(s => s.Familienaam == cbxFamilie.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults = new List<Plant>();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedFamilie.Familienaam.ToString() == plant.Familie)
                    {
                        searchResults.Add(plant);
                    }
                }
            }
            if (cbxGeslacht.SelectedItem != null)
            {
                var selectedGeslacht = context.TfgsvGeslacht.FirstOrDefault(s => s.Geslachtnaam == cbxGeslacht.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults = new List<Plant>();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedGeslacht.Geslachtnaam.ToString() == plant.Geslacht)
                    {
                        searchResults.Add(plant);
                    }
                }
            }
            if (cbxSoort.SelectedItem != null)
            {
                var selectedSoort = context.TfgsvSoort.FirstOrDefault(s => s.Soortnaam == cbxSoort.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults = new List<Plant>();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedSoort.Soortnaam.ToString() == plant.Soort)
                    {
                        searchResults.Add(plant);
                    }
                }
            }
            if (cbxVariant.SelectedItem != null)
            {
                var selectedVariant = context.TfgsvVariant.FirstOrDefault(s => s.Variantnaam == cbxVariant.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults = new List<Plant>();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedVariant.Variantnaam.ToString() == plant.Variant)
                    {
                        searchResults.Add(plant);
                    }
                }
            }
            foreach (Plant plant1 in searchResults)
            {
                lstResult.Items.Add(plant1.Fgsv);
            }
            
        }

        private void txtSearchbox_KeyUp(object sender, KeyEventArgs e)
        {//Hermes, Senne
            List<string> SearchResults = new List<string>();
            string SearchValue = txtSearchbox.Text.ToLower().Trim();

            if (lstResult.Items.Count!=0)
            {
                SearchResults = ComboboxResult();
            }
            else if(cbxFamilie.SelectedItem == null && cbxGeslacht.SelectedItem == null && cbxSoort.SelectedItem == null && cbxVariant.SelectedItem == null)
            {
                List<Plant> AllPlants = context.Plant.ToList();
                foreach (Plant plant in AllPlants)
                {
                    SearchResults.Add(plant.Fgsv);
                }
            }
            else
            {
                lstResult.Items.Clear();
                searchResults();
            }

            if (SearchValue.Length != 0)
            {
                List<string> NewSearchResults = new List<string>();

                foreach (string plant in SearchResults)
                {
                    if (plant.ToLower().Contains(SearchValue))
                    {
                        NewSearchResults.Add(plant);
                    }
                }

                NewSearchResults.Sort();
                lstResult.Items.Clear();

                foreach (string plant in NewSearchResults)
                {
                    lstResult.Items.Add(plant);
                }
            }
            else 
            {
                lstResult.Items.Clear();
                if (cbxFamilie.SelectedItem != null || cbxGeslacht.SelectedItem != null || cbxSoort.SelectedItem != null || cbxVariant.SelectedItem != null)
                {
                    searchResults();
                }
            }
        }
        private List<string> ComboboxResult()
        {//Hermes, Senne
            List<string> SearchResults;
            lstResult.Items.Clear();
            searchResults();
            SearchResults = lstResult.Items.Cast<string>().ToList();

            return SearchResults;
        }
    }
}

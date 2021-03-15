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
        //Nieuwe context aanmaken
        private static Planten2021Context context = new Planten2021Context();
        public MainWindow()
        {
            //Jelle
            InitializeComponent();
            

            //Items toevoegen aan comboboxen
            addItemsToComboBox(cbxType);
            addItemsToComboBox(cbxFamilie);
            addItemsToComboBox(cbxVariant);
            addItemsToComboBox(cbxSoort);
            addItemsToComboBox(cbxGeslacht);
            
        }
        
        //Functie om items toe te voegen aan combobox
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
            //sort() is standaard alfabetisch, items worden gesorteerd.
            cboItems.Sort();
            cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());
            foreach (string item in cboItems)
            {
                plant.Items.Add(item);
            }
        }
        //Resultaten weergeven aan de hand van de gelesecteerde keuze
        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            txtSearchbox.Clear();

            if (cbxType.SelectedItem != null)
            {
                //Alles leegmaken
                lstResult.Items.Clear();
                cbxFamilie.Items.Clear();
                cbxGeslacht.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();
                List<long> typeId = new List<long>();
                List<long> familieId = new List<long>();
                List<long> geslachtId = new List<long>();
                List<long> soortId = new List<long>();


                var selectedType = context.TfgsvType.Where(s => s.Planttypenaam == cbxType.SelectedItem.ToString());
                foreach (var type in selectedType)
                {
                    typeId.Add(type.Planttypeid);
                }

                foreach (int id in typeId)
                {
                    fillFamilieCombobox(id);
                    var selectedFamilie = context.TfgsvFamilie.Where(s => s.FamileId == id);  
                    foreach (var familie in selectedFamilie)
                    {
                        familieId.Add(familie.FamileId);
                    }
                }

                foreach (int id in familieId)
                {
                    fillGeslachtCombobox(id);
                    var selectedGeslacht = context.TfgsvGeslacht.Where(s => s.FamilieFamileId == id);
                    foreach (var geslacht in selectedGeslacht)
                    {
                        geslachtId.Add(geslacht.GeslachtId);
                    }
                }

                foreach (int id in geslachtId)
                {
                    fillSoortCombobox(id);
                    var selectedSoort = context.TfgsvSoort.Where(s => s.GeslachtGeslachtId == id);
                    foreach (var soort in selectedSoort)
                    {
                        soortId.Add(soort.Soortid);
                    }
                }
                foreach (int id in soortId)
                {
                    fillVariantCombobox(id);
                }
                searchResults();
            }
        }

        private void cbxFamilie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Maarten
            txtSearchbox.Clear();

            if (cbxFamilie.SelectedItem != null)
            {
                lstResult.Items.Clear();
                cbxGeslacht.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();
                List<long> familieId = new List<long>();
                List<long> geslachtId = new List<long>();
                List<long> soortId = new List<long>();


                var selectedFamilie = context.TfgsvFamilie.Where(s => s.Familienaam == cbxFamilie.SelectedItem.ToString());
                foreach (var familie in selectedFamilie)
                {
                    familieId.Add(familie.FamileId);
                }
                foreach (int id in familieId)
                {
                    fillGeslachtCombobox(id);
                    var selectedGeslacht = context.TfgsvGeslacht.Where(s => s.FamilieFamileId == id);
                    foreach (var geslacht in selectedGeslacht)
                    {
                        geslachtId.Add(geslacht.GeslachtId);
                    }
                }

                foreach (int id in geslachtId)
                {
                    fillSoortCombobox(id);
                    var selectedSoort = context.TfgsvSoort.Where(s => s.GeslachtGeslachtId == id);
                    foreach (var soort in selectedSoort)
                    {
                        soortId.Add(soort.Soortid);
                    }
                }
                foreach (int id in soortId)
                {
                    fillVariantCombobox(id);
                }
                
                searchResults();
            }
        }

        private void cbxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Jelle
            txtSearchbox.Clear();

            if (cbxSoort.SelectedItem != null)
            {
                ClearItems(cbxVariant);
                var selectedSoort = context.TfgsvSoort.Where(s => s.Soortnaam == cbxSoort.SelectedItem.ToString());
                foreach (var soort in selectedSoort)
                {
                    fillVariantCombobox(soort.Soortid);
                }
                searchResults();
            }
        }

        private void cbxVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stephanie, Senne
            txtSearchbox.Clear();

            if (cbxVariant.SelectedItem != null)
            {
                lstResult.Items.Clear();
                var selectedVariant = context.TfgsvVariant.FirstOrDefault(v => v.Variantnaam == cbxVariant.SelectedItem.ToString());
                searchResults();
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
            txtSearchbox.Clear();

            if (cbxGeslacht.SelectedItem != null)
            {
                lstResult.Items.Clear();
                cbxSoort.Items.Clear();
                cbxVariant.Items.Clear();
                List<long> geslachtId = new List<long>();
                List<long> soortId = new List<long>();

                var selectedgeslacht = context.TfgsvGeslacht.Where(s => s.Geslachtnaam == cbxGeslacht.SelectedItem.ToString());
                foreach (var geslacht in selectedgeslacht)
                {
                    geslachtId.Add(geslacht.GeslachtId);
                }
                foreach (int id in geslachtId)
                {
                    fillSoortCombobox(id);
                    var selectedSoort = context.TfgsvSoort.Where(s => s.GeslachtGeslachtId == id);
                    foreach (var geslacht in selectedSoort)
                    {
                        soortId.Add(id);
                    }
                }
                foreach (int id in soortId)
                {
                    fillVariantCombobox(id);
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

            cbxType.IsEnabled = true;
            cbxFamilie.IsEnabled = true;
            cbxGeslacht.IsEnabled = true;
            cbxSoort.IsEnabled = true;
            cbxVariant.IsEnabled = true;

            txtSearchbox.Clear();
        }
        //Maarten, Hemen & Jelle
        //Weergave resultaten in de listbox aan de hand van de geselecteerde kenmerken
        private void searchResults()
        {
            List<Plant> searchResults = context.Plant.ToList();
            List<Plant> SearchResultsCopy = new List<Plant>();
            if (cbxType.SelectedItem != null)
            {
                var selectedType = context.TfgsvType.FirstOrDefault(s => s.Planttypenaam == cbxType.SelectedItem.ToString());
                SearchResultsCopy = searchResults;
                searchResults = new List<Plant>();
                foreach (Plant plant in SearchResultsCopy)
                {
                    if (selectedType.Planttypenaam.ToString() == plant.Type)
                    {
                        //Voeg de plant toe aan zoekresultaat
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
            List<string> allresults = lstResult.Items.Cast<string>().ToList();
            allresults.Sort();
            lstResult.Items.Clear();
            foreach (string plant in allresults)
            {
                lstResult.Items.Add(plant);
            }
        }

        private void txtSearchbox_KeyUp(object sender, KeyEventArgs e)
        {//Hermes, Senne
            List<string> SearchResults = ComboboxResult();
            string SearchValue = txtSearchbox.Text.ToLower().Trim();
            //Indien alles leeg
            if(cbxFamilie.SelectedItem == null && cbxGeslacht.SelectedItem == null && cbxSoort.SelectedItem == null && cbxVariant.SelectedItem == null)
            {
                //lijst aanmaken van alle planten
                List<Plant> AllPlants = context.Plant.ToList();
                //elke plant wordt afzonderlijk gecontroleerd en wordt toegevoegd aan searchresults.
                foreach (Plant plant in AllPlants)
                {
                    SearchResults.Add(plant.Fgsv);
                }
            }
            else
            {
                //Indien er wel iets geselecteerd is worden de listboxes leeg gemaakt en opnieuw gevuld met zoekresultaten adhv filters.
                lstResult.Items.Clear();
                searchResults();
            }
            //als er iets in de tekstbox is ingevuld
            if (SearchValue.Length != 0)
            {
                List<string> NewSearchResults = new List<string>();

                foreach (string plant in SearchResults)
                {
                    if (plant.ToLower().Contains(SearchValue))
                    {
                        //Lege searchresult vullen met "naam" van plant
                        NewSearchResults.Add(plant);
                    }
                }

                NewSearchResults.Sort();
                lstResult.Items.Clear();

                foreach (string plant in NewSearchResults)
                {
                    //gefilterd op alfabet plant toevoegen aan resultaten.
                    lstResult.Items.Add(plant);
                }
            }
            else 
            {
                //indien tekstbox leeg is wordt lstresult leeggemaakt
                lstResult.Items.Clear();
                if (cbxFamilie.SelectedItem != null || cbxGeslacht.SelectedItem != null || cbxSoort.SelectedItem != null || cbxVariant.SelectedItem != null)
                {
                    //toont de gekozen selecties.
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
        private void lstResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {//Jelle & Stephanie
            try
            {
                //Bij dubbelklikken op resultaat wordt een nieuw venster geopend met de plant en al zijn informatie
                var plant = context.Plant.FirstOrDefault(s => s.Fgsv == lstResult.SelectedItem.ToString());
                ResultatenWindow resultatenWindow = new ResultatenWindow(plant);
                resultatenWindow.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Gelieve een plant te selecteren.");
            }
        }
        //Vull functies, als typeId overeenkomt, item toevoegen aan combobox
        private void fillFamilieCombobox(long typeId)
        {
            List<string> cboItems = new List<string>();
            foreach (TfgsvFamilie familie in context.TfgsvFamilie.ToList())
            {
                if (typeId == familie.TypeTypeid)
                {
                    cboItems.Add(familie.Familienaam);
                }
            }
            if (cboItems.Count != 0)
            {
                cbxFamilie.IsEnabled = true;
                cboItems.Sort();
                cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());
                foreach (string item in cboItems)
                {
                    cbxFamilie.Items.Add(item);
                }
            }
            else
            {
                cbxFamilie.IsEnabled = false;
            }
            
        }
        //Jelle & Maarten
        private void fillGeslachtCombobox(long familieId)
        {
            List<string> cboItems = new List<string>();
            foreach (TfgsvGeslacht geslacht in context.TfgsvGeslacht.ToList())
            {
                if (familieId == geslacht.FamilieFamileId)
                {
                    cboItems.Add(geslacht.Geslachtnaam);
                }

            }
            if (cboItems.Count != 0)
            {
                cbxGeslacht.IsEnabled = true;
                cboItems.Sort();
                cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());
                foreach (string item in cboItems)
                {
                    cbxGeslacht.Items.Add(item);
                }
            }
            else
            {
                cbxGeslacht.IsEnabled = false;
            }
            
        }
        //Jelle & Maarten
        private void fillSoortCombobox(long geslachtId)
        {
            List<string> cboItems = new List<string>();
            foreach (TfgsvSoort soort in context.TfgsvSoort.ToList())
            {
                if (geslachtId == soort.GeslachtGeslachtId)
                {
                    cboItems.Add(soort.Soortnaam);
                }
            }
            if (cboItems.Count != 0)
            {
                cbxVariant.IsEnabled = true;
                cboItems.Sort();
                cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());
                foreach (string item in cboItems)
                {
                    cbxSoort.Items.Add(item);
                }
            }
            else
            {
                cbxVariant.IsEnabled = false;
            }
        }
        //Jelle & Maarten
        private void fillVariantCombobox(long soortId)
        {
            List<string> cboItems = new List<string>();
            foreach (TfgsvVariant variant in context.TfgsvVariant.ToList())
            {
                if (soortId == variant.SoortSoortid)
                {
                    cboItems.Add(variant.Variantnaam);
                }
            }
            if (cboItems.Count != 0)
            {
                cbxVariant.IsEnabled = true;
                cboItems.Sort();
                cboItems = cboItems.ConvertAll(d => d.Substring(0, 1).ToUpper() + d.Substring(1).ToLower());

                foreach (string item in cboItems)
                {
                    cbxVariant.Items.Add(item);
                }
            }
            else
            {
                cbxVariant.IsEnabled = false;
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    //Maarten & Stephanie
    class ResultatenViewModel : ViewModelBase
    {
        //Senne & Hermes
        public RelayCommand<Window> EditSchermCommand { get; set; }
        //Jelle & Hemen 
        //Command maken voor form te sluiten
        public RelayCommand<Window> CloseResultCommand { get; private set; }

        //Jelle & Stephanie
        //ObservableCollection die gelinkt wordt via binding aan de juiste listboxen
        //Verschil ObservableCollection en lists: observablecollection zorgt ervoor dat als je in je frontend dingen aanpast, dat dit automatisch mee aangepast wordt,
        //dit komt doordat de INotifyCollectionChanged hier ingebouwd is (bij lists niet). In dit geval kan je een list gebruiken
        //wij gebruiken Observable om in de toekomst de optie tot uitbreiden te voorzien.
        public ObservableCollection<string> BeheerSelectedPlant { get; set; }
        public ObservableCollection<string> GetSelectedPlantLevensvorm { get; set; }
        public ObservableCollection<string> GetSelectedPlantSociabiliteit { get; set; }
        public ObservableCollection<string> GetSelectedPlantLevensduurConcurrentiekracht { get; set; }
        public ObservableCollection<string> SelectedPlantBladKleur { get; set; }
        public ObservableCollection<string> SelectedPlantBloeiKleur { get; set; }
        public ObservableCollection<string> SelectedPlantAbioHabitat { get; set; }   

        //Maarten & Stephanie
        private PlantenDataService _plantenDataService;
     
        //Constructor, dit wordt gebruikt om waarden in te stellen
        public ResultatenViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
            //Dit dient om het resultatenscherm te sluiten
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);

            //Senne & Hermes
            //Om commands via buttons door te geven
            EditSchermCommand = new RelayCommand<Window>(this.EditScherm);

            //Jelle & Stephanie
            //Instellen van nieuwe ObservableCollections voor gebruik om informatie weer te geven in de UI
            BeheerSelectedPlant = new ObservableCollection<string>();
            GetSelectedPlantLevensvorm = new ObservableCollection<string>();
            GetSelectedPlantSociabiliteit = new ObservableCollection<string>();
            GetSelectedPlantLevensduurConcurrentiekracht = new ObservableCollection<string>();
            SelectedPlantBladKleur = new ObservableCollection<string>();
            SelectedPlantBloeiKleur = new ObservableCollection<string>();
            SelectedPlantAbioHabitat = new ObservableCollection<string>();
        }

        //Senne & Hermes
        //Opent een nieuw scherm naar Edit pagina.
        private void EditScherm(Window window)
        {
            EditWindow window = new EditWindow(PlantenResultaat, LoggedInGebruiker);
            window.ShowDialog();
        }
        //Maarten
        public string Foto { get; set; }

        //Stephanie & Maarten
        //Geeft de data van de plant door
        public Fenotype Fenotype { get; set; }

        public Abiotiek Abiotiek { get; set; }

        public Commensalisme Commensalisme { get; set; }

        public ExtraEigenschap ExtraEigenschap { get; set; }

        public Plant PlantenResultaat { get; set; }

        //Jelle
        //Maken van gebruiker
        public Gebruiker LoggedInGebruiker { get; set; }
        //Jelle
        //Maken van visibility om te linken via databinding met gui
        public Visibility RolButtonsVisibility { get; set; }

        //Jelle
        //functie om gebruiker info te geven om te gebruiken doorheen de viewmodel
        public void LoadLoggedInUser(Gebruiker gebruiker)
        {
            LoggedInGebruiker = gebruiker;
        }
        //Jelle
        //Functie voor de visibility van de speciale buttons die bij sommige rollen niet beschikbaar mogen zijn.
        public void EnableRolButtons()
        {
            switch (LoggedInGebruiker.Rol)
            {
                case "Gebruiker":
                    RolButtonsVisibility = Visibility.Hidden;
                    break;
                case "Data-collector":
                    RolButtonsVisibility = Visibility.Hidden;
                    break;
                case "Manager":
                    RolButtonsVisibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        //Jelle & Hemen
        //Command die gelinkt is aan close button om form te sluiten
        public void CloseResult(Window window)
        {
            if (window != null)
            {
                MainWindow main = new MainWindow();
                window.Close();
                main.ShowDialog();
            }
        }

        //Command om labels op te vullen
        //deze wordt opgeroepen vóór het laden van de lijsten
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
            //Geeft elke keer de gevraagde informatie per opgezochte plant
            Fenotype = _plantenDataService.GetFenotype(plant.PlantId);
            Abiotiek = _plantenDataService.GetAbiotiek(plant.PlantId);
            Commensalisme = _plantenDataService.GetCommensalisme(plant.PlantId);
            ExtraEigenschap = _plantenDataService.GetExtraEigenschap(plant.PlantId);
            var foto = _plantenDataService.getFotoViaPlantId(plant.PlantId);
            if (foto != null)
            {
                var Path = System.IO.Directory.GetCurrentDirectory();
                var aangepastPath = Path.Replace("bin\\Debug\\netcoreapp3.1", foto.UrlLocatie);
                Foto = aangepastPath;
            }

            //Jelle
            var _getSelectedBeheerMaand = _plantenDataService.GetBeheerMaand(plant.PlantId);

            foreach (var beheerMaand in _getSelectedBeheerMaand)
            {
                string text = "Beheerdaad: " + beheerMaand.Beheerdaad;
                text += "\r\nMaand(en):";
                if (beheerMaand.Jan == true) { text += " - Januari"; }
                if (beheerMaand.Feb == true) { text += " - Februari"; }
                if (beheerMaand.Mrt == true) { text += " - Maart"; }
                if (beheerMaand.Apr == true) { text += " - April"; }
                if (beheerMaand.Mei == true) { text += " - Mei"; }
                if (beheerMaand.Jun == true) { text += " - Juni"; }
                if (beheerMaand.Jul == true) { text += " - Juli"; }
                if (beheerMaand.Aug == true) { text += " - Augustus"; }
                if (beheerMaand.Sept == true) { text += " - September"; }
                if (beheerMaand.Okt == true) { text += " - Oktober"; }
                if (beheerMaand.Nov == true) { text += " - November"; }
                if (beheerMaand.Dec == true) { text += " - December"; }

                text += "\r\nFrequentie: " + beheerMaand.FrequentiePerJaar;
                text += "\r\nOmschrijving: " + beheerMaand.Omschrijving;
                
                BeheerSelectedPlant.Add(text);
            }
            //Jelle & Stephanie
            //Filter alle getcommMulti rijen die het plantId bevat
            var _getSelectedPlantCommMulti = _plantenDataService.GetCommMulti(plant.PlantId);

            //Foreach vervolgd door een switch om op te splitsen in juiste tabellen
            foreach (var commMulti in _getSelectedPlantCommMulti)
            {
                switch (commMulti.Eigenschap)
                {
                    case "Socialibiteit":
                        GetSelectedPlantSociabiliteit.Add(commMulti.Waarde);
                        break;
                    case "Levensvorm":
                        GetSelectedPlantLevensvorm.Add(commMulti.Waarde);
                        break;
                    default:
                        break;
                }
            }
            //Stephanie & Jelle
            var _fenotypeMulti = _plantenDataService.GetFenoMultiKleur(plant.PlantId);
            
            foreach (var FenoMulti in _fenotypeMulti)
            {
                string listText = FenoMulti.Maand + " - " + FenoMulti.Waarde;
                switch (FenoMulti.Eigenschap)
                {
                    case "blad":
                        SelectedPlantBladKleur.Add(listText);
                        break;
                    case "bloei": 
                        SelectedPlantBloeiKleur.Add(listText);
                        break;
                    default:
                        break;
                }
            }
            //Stephanie & Jelle
            var _abiotiekMulti = _plantenDataService.GetAbiotiekMulti(plant.PlantId);

            foreach (var AbioMulti in _abiotiekMulti)
            {
                SelectedPlantAbioHabitat.Add(AbioMulti.Waarde);
            }
        }
    }
}

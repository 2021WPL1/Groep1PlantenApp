using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PlantenApplicatie.UI.ViewModel
{
    class GebruikerResultModel : ViewModelBase
    { 
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
        public ObservableCollection<string> SelectedPlantAbioHabitats { get; set; }

        //Jelle & Hemen
        //Plant voor in labels
        private Plant _plantenResultaat;
        //Maarten & Stephanie
        private PlantenDataService _plantenDataService;
        private Fenotype _fenotype;
        private Abiotiek _abiotiek;
        private Commensalisme _commensalisme;
        private ExtraEigenschap _extraEigenschap;
        private Foto _foto;
        //private BeheerMaand _beheerMaand;

        //Jelle & Stephanie
        //Private lists om observable collections op te vullen
        private List<string> _beheerSelectedPlant = new List<string>();
        private List<string> _getSelectedPlantLevensvorm = new List<string>();
        private List<string> _getSelecteedPlantSociabiliteit = new List<string>();
        private List<string> _selectedPlantBladKleur = new List<string>();
        private List<string> _selectedPlantBloeiKleur = new List<string>();
        private List<string> _getSelectedPlantLevensduurConcurrentiekracht = new List<string>();
        private List<string> _selectedPlantAbioHabitat = new List<string>();

        //Jelle & Stephanie
        //Lists om multi data in te stoppen, dit is om de plantgegevens met meerdere waarden te gebruiken om de juiste waarden te krijgen.
        private List<CommensalismeMulti> _getSelectedPlantCommMulti = new List<CommensalismeMulti>();
        private List<FenotypeMulti> _fenotypeMulti = new List<FenotypeMulti>();
        private List<AbiotiekMulti> _abiotiekMulti = new List<AbiotiekMulti>();

        //Jelle
        private List<BeheerMaand> _getSelectedBeheerMaand = new List<BeheerMaand>();

        //Constructor, dit wordt gebruikt om waarden in te stellen
        public GebruikerResultModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
            //Dit dient om het resultatenscherm te sluiten
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);

            //Jelle & Stephanie
            //Instellen van nieuwe ObservableCollections voor gebruik om informatie weer te geven in de UI
            BeheerSelectedPlant = new ObservableCollection<string>();
            GetSelectedPlantLevensvorm = new ObservableCollection<string>();
            GetSelectedPlantSociabiliteit = new ObservableCollection<string>();
            GetSelectedPlantLevensduurConcurrentiekracht = new ObservableCollection<string>();
            SelectedPlantBladKleur = new ObservableCollection<string>();
            SelectedPlantBloeiKleur = new ObservableCollection<string>();
            SelectedPlantAbioHabitats = new ObservableCollection<string>();
        }

        //Senne & Hermes
        //Opent een nieuw scherm naar Edit pagina.
        private void EditScherm()
        {
            EditWindow window = new EditWindow(_plantenResultaat);
            window.ShowDialog();
        }
        public Foto Foto
        {
            get { return _foto; }
            set
            {
                _foto = value;
            }
        }

        //Stephanie & Maarten
        //Geeft de data van de plant door
        public Fenotype Fenotype
        {
            get { return _fenotype; }
            set
            {
                _fenotype = value;
            }
        }

        public Abiotiek Abiotiek
        {
            get { return _abiotiek; }
            set
            {
                _abiotiek = value;
            }
        }

        public Commensalisme Commensalisme
        {
            get { return _commensalisme; }
            set
            {
                _commensalisme = value;
            }

        }

        public ExtraEigenschap ExtraEigenschap
        {
            get { return _extraEigenschap; }
            set
            {
                _extraEigenschap = value;
            }
        }

        /*public BeheerMaand BeheerMaand
        {
            get { return _beheerMaand; }
            set
            {
                _beheerMaand = value;
            }
        }*/

        public Plant PlantenResultaat
        {
            get { return _plantenResultaat; }
            set
            {
                _plantenResultaat = value;
            }
        }

        //Jelle & Stephanie
        //Laadt lijsten voor UI elementen
        public void LoadLists()
        {
            //Maak eerst alle elementen leeg
            BeheerSelectedPlant.Clear();
            GetSelectedPlantLevensvorm.Clear();
            GetSelectedPlantSociabiliteit.Clear();
            GetSelectedPlantLevensduurConcurrentiekracht.Clear();
            SelectedPlantBladKleur.Clear();
            SelectedPlantBloeiKleur.Clear();
            SelectedPlantAbioHabitats.Clear();

            //Vul ObservableCollections met informatie uit de lijsten
            //De informatie komt uit fillLabels
            foreach (var month in _beheerSelectedPlant)
            {
                BeheerSelectedPlant.Add(month);
            }
            foreach (var text in _getSelectedPlantLevensvorm)
            {
                GetSelectedPlantLevensvorm.Add(text);
            }
            foreach (var text in _getSelecteedPlantSociabiliteit)
            {
                GetSelectedPlantSociabiliteit.Add(text);
            }
            foreach (var text in _getSelectedPlantLevensduurConcurrentiekracht)
            {
                GetSelectedPlantLevensduurConcurrentiekracht.Add(text);
            }
            foreach (var bladkleur in _selectedPlantBladKleur)
            {
                SelectedPlantBladKleur.Add(bladkleur);
            }
            foreach (var bloeikleur in _selectedPlantBloeiKleur)
            {
                SelectedPlantBloeiKleur.Add(bloeikleur);
            }
            foreach (var habitat in _selectedPlantAbioHabitat)
            {
                SelectedPlantAbioHabitats.Add(habitat);
            }
        }

        //Jelle & Hemen
        //Command die gelinkt is aan close button om form te sluiten
        public void CloseResult(Window window)
        {
            if (window != null)
            {
                window.Close();
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
            Foto = _plantenDataService.getFotoViaPlantId(plant.PlantId);
            // BeheerMaand = _plantenDataService.GetBeheerMaand(plant.PlantId);

            //Jelle
            _getSelectedBeheerMaand = _plantenDataService.GetBeheerMaand(plant.PlantId);

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

                _beheerSelectedPlant.Add(text);
            }

            //Jelle & Stephanie
            //controleert of de beheermaand maand true is, als dat zo is komt hij in de lijst, anders niet
            /*if (BeheerMaand.Jan == true) { _beheerSelectedPlant.Add("Januari"); }
            if (BeheerMaand.Feb == true) { _beheerSelectedPlant.Add("Februari"); }
            if (BeheerMaand.Mrt == true) { _beheerSelectedPlant.Add("Maart"); }
            if (BeheerMaand.Apr == true) { _beheerSelectedPlant.Add("April"); }
            if (BeheerMaand.Mei == true) { _beheerSelectedPlant.Add("Mei"); }
            if (BeheerMaand.Jun == true) { _beheerSelectedPlant.Add("Juni"); }
            if (BeheerMaand.Jul == true) { _beheerSelectedPlant.Add("Juli"); }
            if (BeheerMaand.Aug == true) { _beheerSelectedPlant.Add("Augustus"); }
            if (BeheerMaand.Sept == true) { _beheerSelectedPlant.Add("September"); }
            if (BeheerMaand.Okt == true) { _beheerSelectedPlant.Add("Oktober"); }
            if (BeheerMaand.Nov == true) { _beheerSelectedPlant.Add("November"); }
            if (BeheerMaand.Dec == true) { _beheerSelectedPlant.Add("December"); }*/

            //Jelle & Stephanie
            //Filter alle getcommMulti rijen die het plantId bevat
            _getSelectedPlantCommMulti = _plantenDataService.GetCommMulti(plant.PlantId);

            //Foreach vervolgd door een switch om op te splitsen in juiste tabellen
            foreach (var commMulti in _getSelectedPlantCommMulti)
            {
                switch (commMulti.Eigenschap)
                {
                    case "Socialibiteit":
                        _getSelecteedPlantSociabiliteit.Add(commMulti.Waarde);
                        break;
                    case "Levensvorm":
                        _getSelectedPlantLevensvorm.Add(commMulti.Waarde);
                        break;
                    default:
                        break;
                }
            }
            //Stephanie & Jelle
            _fenotypeMulti = _plantenDataService.GetFenoMultiKleur(plant.PlantId);

            foreach (var FenoMulti in _fenotypeMulti)
            {
                string listText = FenoMulti.Maand + " - " + FenoMulti.Waarde;
                switch (FenoMulti.Eigenschap)
                {
                    case "blad":
                        _selectedPlantBladKleur.Add(listText);
                        break;
                    case "bloei":
                        _selectedPlantBloeiKleur.Add(listText);
                        break;
                    default:
                        break;
                }
            }
            //Stephanie & Jelle
            _abiotiekMulti = _plantenDataService.GetAbiotiekMulti(plant.PlantId);

            foreach (var AbioMulti in _abiotiekMulti)
            {
                _selectedPlantAbioHabitat.Add(AbioMulti.Waarde);
            }
        }
    }
}

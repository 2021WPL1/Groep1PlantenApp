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
        public ICommand EditSchermCommand { get; set; }
        //Jelle & Hemen 
        //Command maken voor form te sluiten
        public RelayCommand<Window> CloseResultCommand { get; private set; }

        //Jelle & Stephanie
        //Lists voor boxen met meerdere waarden
        public ObservableCollection<string> BeheerAllSelectedPlantMonths { get; set; }
        public ObservableCollection<string> GetSelectedPlantLevensvorm { get; set; }
        public ObservableCollection<string> GetSelectedPlantSociabiliteit { get; set; }
        public ObservableCollection<string> GetSelectedPlantLevensduurConcurrentiekracht { get; set; }


        //Jelle & Hemen
        //Plant voor in labels
        private Plant _plantenResultaat;

        private PlantenDataService _plantenDataService;
        private Fenotype _fenotype;
        private Abiotiek _abiotiek;
        private Commensalisme _commensalisme;
        private ExtraEigenschap _extraEigenschap;
        private BeheerMaand _beheerMaand;

        //Jelle & Stephanie
        //Private lists voor observable collections op te vullen
        private List<string> _beheerAllSelectedPlantMonths = new List<string>();
        private List<string> _getSelecteedPlantLevensvorm = new List<string>();
        private List<string> _getSelecteedPlantSociabiliteit = new List<string>();
        private List<string> _getSelectedPlantLevensduurConcurrentiekracht = new List<string>();
        private List<CommensalismeMulti> _getSelectedPlantCommMulti = new List<CommensalismeMulti>();

        public ResultatenViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);

            //Senne & Hermes
            this.EditSchermCommand = new DelegateCommand(EditScherm);

            //Jelle & Stephanie
            BeheerAllSelectedPlantMonths = new ObservableCollection<string>();
            GetSelectedPlantLevensvorm = new ObservableCollection<string>();
            GetSelectedPlantSociabiliteit = new ObservableCollection<string>();
            GetSelectedPlantLevensduurConcurrentiekracht = new ObservableCollection<string>();
        }


        private void EditScherm()
        {
            EditWindow window = new EditWindow(_plantenResultaat);
            window.ShowDialog();
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

        public BeheerMaand BeheerMaand
        {
            get { return _beheerMaand; }
            set
            {
                _beheerMaand = value;
            }
        }

        public Plant PlantenResultaat
        {
            get { return _plantenResultaat; }
            set
            {
                _plantenResultaat = value;
            }
        }

        //Jelle & Stephanie
        //Laad lijsten voor listboxes
        public void LoadLists()
        {
            BeheerAllSelectedPlantMonths.Clear();
            foreach (var month in _beheerAllSelectedPlantMonths)
            {
                BeheerAllSelectedPlantMonths.Add(month);
            }
            GetSelectedPlantLevensvorm.Clear();
            GetSelectedPlantSociabiliteit.Clear();
            GetSelectedPlantLevensduurConcurrentiekracht.Clear();
            foreach (var text in _getSelecteedPlantLevensvorm)
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
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
            //Geeft elke keer de gevraagde informatie per opgezochte plant
            Fenotype = _plantenDataService.GetFenotype(plant.PlantId);
            Abiotiek = _plantenDataService.GetAbiotiek(plant.PlantId);
            Commensalisme = _plantenDataService.GetCommensalisme(plant.PlantId);
            ExtraEigenschap = _plantenDataService.GetExtraEigenschap(plant.PlantId);
            BeheerMaand = _plantenDataService.GetBeheerMaand(plant.PlantId);

            //Jelle & Stephanie
            //Filters voor lists
            if (BeheerMaand.Jan == true) { _beheerAllSelectedPlantMonths.Add("Januari"); }
            if (BeheerMaand.Feb == true) { _beheerAllSelectedPlantMonths.Add("Februari"); }
            if (BeheerMaand.Mrt == true) { _beheerAllSelectedPlantMonths.Add("Maart"); }
            if (BeheerMaand.Apr == true) { _beheerAllSelectedPlantMonths.Add("April"); }
            if (BeheerMaand.Mei == true) { _beheerAllSelectedPlantMonths.Add("Mei"); }
            if (BeheerMaand.Jun == true) { _beheerAllSelectedPlantMonths.Add("Juni"); }
            if (BeheerMaand.Jul == true) { _beheerAllSelectedPlantMonths.Add("Juli"); }
            if (BeheerMaand.Aug == true) { _beheerAllSelectedPlantMonths.Add("Augustus"); }
            if (BeheerMaand.Sept == true) { _beheerAllSelectedPlantMonths.Add("September"); }
            if (BeheerMaand.Okt == true) { _beheerAllSelectedPlantMonths.Add("Oktober"); }
            if (BeheerMaand.Nov == true) { _beheerAllSelectedPlantMonths.Add("November"); }
            if (BeheerMaand.Dec == true) { _beheerAllSelectedPlantMonths.Add("December"); }

            //Jelle & Stephanie
            //Filtered alle getcommmulti rijen die het plantid bevattet
            _getSelectedPlantCommMulti = _plantenDataService.GetCommMulti(plant.PlantId);
            //Foreach vervolg door een switch om op te splitsen in juiste tabellen
            foreach (var commMulti in _getSelectedPlantCommMulti)
            {
                switch (commMulti.Eigenschap)
                {
                    case "Socialibiteit":
                        _getSelecteedPlantSociabiliteit.Add(commMulti.Waarde);
                        break;
                    case "Levensvorm":
                        _getSelecteedPlantLevensvorm.Add(commMulti.Waarde);
                        break;
                    case "Levensduur/Concurrentiekracht":
                        _getSelectedPlantLevensduurConcurrentiekracht.Add(commMulti.Waarde);
                        break;
                    default:
                        break;
                }
            }


        }
    }
}

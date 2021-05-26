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

        //Stephanie
        public ObservableCollection<string> SelectedPlantBladKleur { get; set; }
        public ObservableCollection<string> SelectedPlantBloeiKleur { get; set; }

        private List<string> _selectedPlantBladKleur = new List<string>();
        private List<string> _selectedPlantBloeiKleur = new List<string>();
        private List<FenotypeMulti> _fenotypeMulti = new List<FenotypeMulti>();
        //Stephanie
        public ObservableCollection<string> SelectedPlantAbioHabitats { get; set; }


        private List<string> _selectedPlantAbioHabitat = new List<string>();
        private List<AbiotiekMulti> _abiotiekMulti = new List<AbiotiekMulti>();

        //Jelle & Hemen
        //Plant voor in labels
        private Plant _plantenResultaat;
        //Maarten & Stephanie
        private PlantenDataService _plantenDataService;
        private Fenotype _fenotype;
        private Abiotiek _abiotiek;
        private Commensalisme _commensalisme;
        private ExtraEigenschap _extraEigenschap;
        private BeheerMaand _beheerMaand;



        public ResultatenViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
            this.CloseResultCommand = new RelayCommand<Window>(this.CloseResult);

            //Senne & Hermes
            this.EditSchermCommand = new DelegateCommand(EditScherm);

            SelectedPlantBladKleur = new ObservableCollection<string>();
            SelectedPlantBloeiKleur = new ObservableCollection<string>();
            SelectedPlantAbioHabitats = new ObservableCollection<string>();
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

        //Stephanie
        public void LoadList()
        {
           SelectedPlantBladKleur.Clear();
           SelectedPlantBloeiKleur.Clear();
           foreach (var bladkleur in _selectedPlantBladKleur)
           {
               SelectedPlantBladKleur.Add(bladkleur);
           }

           foreach (var bloeikleur in _selectedPlantBloeiKleur)
           {
               SelectedPlantBloeiKleur.Add(bloeikleur);
           }

           SelectedPlantAbioHabitats.Clear();

           foreach (var habitat in _selectedPlantAbioHabitat)
           {
               SelectedPlantAbioHabitats.Add(habitat);
           }
        }
        //Stephanie
        public void LoadHabitat()
        {

            _selectedPlantAbioHabitat.Clear();
            foreach (var habitat in SelectedPlantAbioHabitats)
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
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
            //Geeft elke keer de gevraagde informatie per opgezochte plant
            Fenotype = _plantenDataService.GetFenotype(plant.PlantId);
            Abiotiek = _plantenDataService.GetAbiotiek(plant.PlantId);
            Commensalisme = _plantenDataService.GetCommensalisme(plant.PlantId);
            ExtraEigenschap = _plantenDataService.GetExtraEigenschap(plant.PlantId);
            BeheerMaand = _plantenDataService.GetBeheerMaand(plant.PlantId);

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
            //Stephanie
            _abiotiekMulti = _plantenDataService.GetAbiotiekMulti(plant.PlantId);

            foreach (var AbioMulti in _abiotiekMulti)
            {
                string listText = AbioMulti.Eigenschap + " - " + AbioMulti.Waarde;
            }
        }
    }
}

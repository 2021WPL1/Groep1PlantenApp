using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    public class EditViewModel :ViewModelBase
    {//Senne & Hermes
        private PlantenDataService _plantenDataService;

        //Command voor opslaan
        public ICommand OpslaanCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HabitatToevoegenCommand { get; set; }
        public ICommand HabitatVerwijderenCommand { get; set; }

        //Observable collections voor de binding
        //Filters
        public ObservableCollection<TfgsvType> FilterTfgsvTypes { get; set; }
        public ObservableCollection<TfgsvFamilie> FilterTfgsvFamilie { get; set; }
        public ObservableCollection<TfgsvGeslacht> FilterTfgsvGeslacht { get; set; }
        public ObservableCollection<TfgsvSoort> FilterTfgsvSoort { get; set; }
        public ObservableCollection<TfgsvVariant> FilterTfgsvVariant { get; set; }
        //Fenotype
        //Abio
        public ObservableCollection<AbioBezonning> AbioBezonning { get; set; }
        public ObservableCollection<AbioGrondsoort> AbioGrondsoort { get; set; }
        public ObservableCollection<AbioVoedingsbehoefte> AbioVoedingsbehoefte { get; set; }
        public ObservableCollection<AbioVochtbehoefte> AbioVochtbehoefte { get; set; }
        public ObservableCollection<AbioReactieAntagonischeOmg> AbioReactieAntagonischeOmg { get; set; }
        public ObservableCollection<AbioHabitat> AbioAllHabitats { get; set; }
        public ObservableCollection<AbioHabitat> AbioAddedHabitats { get; set; }
        //Commersialisme
        //Extra Eigenschappen
        //Beheer Eigenschappen

        //Lists om de observable collections op te vullen
        //Filters
        private List<TfgsvType> _filtertypes;
        private List<TfgsvFamilie> _filterfamilies;
        private List<TfgsvGeslacht> _filtergeslachten;
        private List<TfgsvSoort> _filtersoorten;
        private List<TfgsvVariant> _filtervarianten;
        //Fenotype
        //Abio
        private List<AbioBezonning> _abiobezonning;
        private List<AbioGrondsoort> _abiogrondsoort;
        private List<AbioVoedingsbehoefte> _abiovoedingsbehoefte;
        private List<AbioVochtbehoefte> _abiovochtbehoefte;
        private List<AbioReactieAntagonischeOmg> _abioReactie;
        private List<AbioHabitat> _abioAllHabitats;
        private List<AbioHabitat> _abioAddedHabitats;
        //Commersialisme
        //Extra Eigenschappen
        //Beheer Eigenschappen

        //Geselecteerde waardes
        //Filters
        private TfgsvType _filterselectedType;
        private TfgsvFamilie _filterselectedFamilie;
        private TfgsvGeslacht _filterselectedGeslacht;
        private TfgsvSoort _filterselectedSoort;
        private TfgsvVariant _filterselectedVariant;

        private string _filterNewType;
        private string _filterNewFamilie;
        private string _filterNewGeslacht;
        private string _filterNewSoort;
        private string _filterNewVariant;
        //Fenotype
        //Abio
        private AbioBezonning _abioselectedBezonning;
        private AbioGrondsoort _abioselectedGrondsoort;
        private AbioVoedingsbehoefte _abioselectedVoedingsbehoefte;
        private AbioVochtbehoefte _abioselectedVochtbehoefte;
        private AbioReactieAntagonischeOmg _abioselectedReactie;
        private AbioHabitat _abioselectedAllHabitat;
        private AbioHabitat _abioselectedAddedHabitat;
        //Commersialisme
        //Extra Eigenschappen
        //Beheer Eigenschappen

        public EditViewModel(PlantenDataService plantenDataService)
        {
            //Senne & Hermes
            this._plantenDataService = plantenDataService;

            OpslaanCommand = new DelegateCommand(Opslaan);
            BackCommand = new DelegateCommand(Back);
            HabitatToevoegenCommand = new DelegateCommand(HabitatToevoegen);
            HabitatVerwijderenCommand = new DelegateCommand(HabitatVerwijderen);

            //Filters
            FilterTfgsvTypes = new ObservableCollection<TfgsvType>();
            FilterTfgsvFamilie = new ObservableCollection<TfgsvFamilie>();
            FilterTfgsvGeslacht = new ObservableCollection<TfgsvGeslacht>();
            FilterTfgsvSoort = new ObservableCollection<TfgsvSoort>();
            FilterTfgsvVariant = new ObservableCollection<TfgsvVariant>();
            //Fenotype
            //Abio
            AbioBezonning = new ObservableCollection<AbioBezonning>();
            AbioGrondsoort = new ObservableCollection<AbioGrondsoort>();
            AbioVoedingsbehoefte = new ObservableCollection<AbioVoedingsbehoefte>();
            AbioVochtbehoefte = new ObservableCollection<AbioVochtbehoefte>();
            AbioReactieAntagonischeOmg = new ObservableCollection<AbioReactieAntagonischeOmg>();
            AbioAllHabitats = new ObservableCollection<AbioHabitat>();
            AbioAddedHabitats = new ObservableCollection<AbioHabitat>();
            //Commersialisme
            //Extra Eigenschappen
            //Beheer Eigenschappen
        }

        public void FillDataFromPlant(Plant plant)
        {
            //Senne & Hermes
            //Filters
            FilterSelectedType = _filtertypes.SingleOrDefault(f =>
                f.Planttypenaam == _plantenDataService.GetFilterType(plant.TypeId).Planttypenaam);
            FilterSelectedFamilie = _filterfamilies.FirstOrDefault(f =>
                f.Familienaam == _plantenDataService.GetFilterFamilie(plant.FamilieId).Familienaam);
            FilterSelectedGeslacht = _filtergeslachten.SingleOrDefault(f =>
                f.Geslachtnaam == _plantenDataService.GetFilterGeslacht(plant.GeslachtId)
                    .Geslachtnaam);
            FilterSelectedSoort = _filtersoorten.FirstOrDefault(f =>
                f.Soortnaam == _plantenDataService.GetFilterSoort(plant.SoortId).Soortnaam);
            FilterSelectedVariant = _filtervarianten.SingleOrDefault(f =>
                f.Variantnaam == _plantenDataService.GetFilterVariant(plant.VariantId)
                    .Variantnaam);
            //Fenotype
            //Abio
            AbioSelectedBezonning =
                _abiobezonning.SingleOrDefault(a => a.Naam == _plantenDataService.GetAbiotiek(plant.PlantId).Bezonning);
            AbioSelectedGrondsoort = _abiogrondsoort.SingleOrDefault(a =>
                a.Grondsoort == _plantenDataService.GetAbiotiek(plant.PlantId).Grondsoort);
            AbioSelectedVoedingsbehoefte = _abiovoedingsbehoefte.SingleOrDefault(a =>
                a.Voedingsbehoefte == _plantenDataService.GetAbiotiek(plant.PlantId).Voedingsbehoefte);
            AbioSelectedVochtbehoefte = _abiovochtbehoefte.SingleOrDefault(a =>
                a.Vochtbehoefte == _plantenDataService.GetAbiotiek(plant.PlantId).Vochtbehoefte);
            AbioSelectedReactie = _abioReactie.SingleOrDefault(a =>
                a.Antagonie == _plantenDataService.GetAbiotiek(plant.PlantId).AntagonischeOmgeving);

            foreach (var abiotiekMulti in _plantenDataService.GetAbiotiekMulti(plant.PlantId))
            {
                _abioAddedHabitats.Add(_abioAllHabitats.FirstOrDefault(a => a.Afkorting == abiotiekMulti.Waarde));
            }
            ReloadHabitatlist();
            //Commersialisme
            //Extra Eigenschappen
            //Beheer Eigenschappen
        }
        private void Opslaan()
        {//Senne & Hermes
            /* TEST -> werkt nog niet
            //Filters
            var filterselectedType = FilterSelectedType.Planttypenaam;
            var filterselectedFamilie = FilterSelectedFamilie.Familienaam;
            var filterselectedGeslacht = FilterSelectedGeslacht.Geslachtnaam;
            var filterselectedSoort = FilterSelectedSoort.Soortnaam;
            var filterselectedVariant = FilterSelectedVariant.Variantnaam;
            //Fenotype
            //Abio
            var abioselectedBezonning = AbioSelectedBezonning.Naam;
            var abioselectedGrondsoort = AbioSelectedGrondsoort.Grondsoort;
            var abioselectedVoedingbehoefte = AbioSelectedVoedingsbehoefte.Voedingsbehoefte;
            var abioselectedVochtbehoefte = AbioSelectedVochtbehoefte.Vochtbehoefte;
            var abioselectedReactie = AbioSelectedReactie.Antagonie;
            var abioselectedHabitats = AbioAddedHabitats.Select(x => x.Afkorting).ToList();
            //Commersialisme
            //Extra Eigenschappen
            //Beheer Eigenschappen
            */
        }
        private void Back()
        {//window sluiten

        }
        //Habitat toevoegen aan listbox (die moeten toegevoegd worden aan de plant)
        private void HabitatToevoegen()
        {
            //Senne & Hermes

            if (_abioAddedHabitats!=null)
            {
                if (!_abioAddedHabitats.Contains(AbioSelectedAllHabitat))
                {
                    _abioAddedHabitats.Add(AbioSelectedAllHabitat);

                }
                else
                {
                    MessageBox.Show("Habitat is al toegevoegd");
                }
            }
            ReloadHabitatlist();
        }
        //habitat verwijderen uit de listbox van geselecteerde
        private void HabitatVerwijderen()
        {
            //Senne & Hermes

            if (_abioselectedAddedHabitat!=null)
            {
                _abioAddedHabitats.Remove(_abioselectedAddedHabitat);
            }
            ReloadHabitatlist();
        }

        public void InitializeAll()
        {
            //Senne & Hermes

            //Filters
            _filtertypes = _plantenDataService.GetTfgsvTypes();
            _filterfamilies = _plantenDataService.GetTfgsvFamilies();
            _filtergeslachten = _plantenDataService.GetTfgsvGeslachten();
            _filtersoorten = _plantenDataService.GetTfgsvSoorten();
            _filtervarianten = _plantenDataService.GetTfgsvVarianten();
            //Fenotype
            //Abio
            _abiobezonning = _plantenDataService.GetAbioBezonning();
            _abiogrondsoort = _plantenDataService.GetAbioGrondsoort();
            _abiovoedingsbehoefte = _plantenDataService.GetAbioVoedingsbehoefte();
            _abiovochtbehoefte = _plantenDataService.GetAbioVochtbehoefte();
            _abioReactie = _plantenDataService.GetAbioReactieAntagonischeOmg();
            _abioAllHabitats = _plantenDataService.GetHabitats();
            _abioAddedHabitats = new List<AbioHabitat>();
            //Commersialisme
            //Extra Eigenschappen
            //Beheer Eigenschappen


            LoadAll();
        }

        public void LoadAll()
        {
            //Senne & Hermes

            LoadFilters();
            LoadFenotype();
            LoadAbio();
            LoadCommersialisme();
            LoadExtraEigenschappen();
            LoadBeheerEigenschappen();
        }

        public void LoadFilters()
        {
            //Senne & Hermes

            FilterTfgsvTypes.Clear();
            FilterTfgsvFamilie.Clear();
            FilterTfgsvGeslacht.Clear();
            FilterTfgsvSoort.Clear();
            FilterTfgsvVariant.Clear();

            //Families
            foreach (var tfgsvType in _filtertypes.OrderBy(f => f.Planttypenaam))
            {
                FilterTfgsvTypes.Add(tfgsvType);
            }
            //Families
            foreach (var tfgsvFamily in _filterfamilies.OrderBy(f=>f.Familienaam))
            {
                FilterTfgsvFamilie.Add(tfgsvFamily);
            }
            //Geslacht
            foreach (var tfgsvGeslacht in _filtergeslachten.OrderBy(f=>f.Geslachtnaam))
            {
                FilterTfgsvGeslacht.Add(tfgsvGeslacht);
            }
            //Soorten
            foreach (var tfgsvSoort in _filtersoorten.OrderBy(f=>f.Soortnaam))
            {
                FilterTfgsvSoort.Add(tfgsvSoort);
            }
            //Varianten
            foreach (var tfgsvVariant in _filtervarianten.OrderBy(f=>f.Variantnaam))
            {
                FilterTfgsvVariant.Add(tfgsvVariant);
            }
        }

        public void LoadFenotype()
        {
            //Senne & Hermes

        }

        public void LoadAbio()
        {
            //Senne & Hermes

            AbioBezonning.Clear();
            AbioGrondsoort.Clear();
            AbioVoedingsbehoefte.Clear();
            AbioVochtbehoefte.Clear();
            AbioReactieAntagonischeOmg.Clear();
            AbioAllHabitats.Clear();

            //Bezonning
            foreach (var bezonning in _abiobezonning)
            {
                AbioBezonning.Add(bezonning);
            }
            //Grondsoort
            foreach (var grondsoort in _abiogrondsoort)
            {
                AbioGrondsoort.Add(grondsoort);
            }
            //Voeding
            foreach (var voedingsbehoefte in _abiovoedingsbehoefte)
            {
                AbioVoedingsbehoefte.Add(voedingsbehoefte);
            }
            //Vochtigheid
            foreach (var vochtbehoefte in _abiovochtbehoefte)
            {
                AbioVochtbehoefte.Add(vochtbehoefte);
            }
            //antagonistische omgeving
            foreach (var reactie in _abioReactie)
            {
                AbioReactieAntagonischeOmg.Add(reactie);
            }
            //Habitat
            foreach (var habitat in _abioAllHabitats)
            {
                AbioAllHabitats.Add(habitat);
            }
        }

        public void ReloadHabitatlist()
        {
            //Senne & Hermes

            AbioAddedHabitats.Clear();
            foreach (var habitat in _abioAddedHabitats)
            {
                AbioAddedHabitats.Add(habitat);
            }
        }

        public void LoadCommersialisme()
        {

        }

        public void LoadExtraEigenschappen()
        {

        }

        public void LoadBeheerEigenschappen()
        {

        }

        //Binding voor geselecteerde waardes
        //Filters
        public TfgsvType FilterSelectedType
        {
            //Senne & Hermes

            get { return _filterselectedType; }
            set
            {
                if (_filterselectedType==null)
                {
                    _filterNewType = "Empty";//zorgen dat hij niet null is
                    FilterNewType = null;
                }
                _filterselectedType = value;
                OnPropertyChanged();
            }
        }
        public TfgsvFamilie FilterSelectedFamilie
        {
            //Senne & Hermes

            get { return _filterselectedFamilie; }
            set
            {
                if (_filterselectedFamilie == null)
                {
                    _filterNewFamilie = "Empty";//zorgen dat hij niet null is
                    FilterNewFamilie = null;
                }
                _filterselectedFamilie = value;
                OnPropertyChanged();
            }
        }
        public TfgsvGeslacht FilterSelectedGeslacht
        {
            //Senne & Hermes

            get { return _filterselectedGeslacht; }
            set
            {
                if (_filterselectedGeslacht == null)
                {
                    _filterNewGeslacht = "Empty";//zorgen dat hij niet null is
                    FilterNewGeslacht = null;
                }
                _filterselectedGeslacht = value;
                OnPropertyChanged();
            }
        }
        public TfgsvSoort FilterSelectedSoort
        {
            //Senne & Hermes

            get { return _filterselectedSoort; }
            set
            {
                if (_filterselectedSoort == null)
                {
                    _filterNewSoort = "Empty";//zorgen dat hij niet null is
                    FilterNewSoort = null;
                }
                _filterselectedSoort = value;
                OnPropertyChanged();
            }
        }
        public TfgsvVariant FilterSelectedVariant
        {
            //Senne & Hermes

            get { return _filterselectedVariant; }
            set
            {
                if (_filterselectedVariant == null)
                {
                    _filterNewVariant = "Empty";//zorgen dat hij niet null is
                    FilterNewVariant = null;
                }
                _filterselectedVariant = value;
                OnPropertyChanged();
            }
        }

        public string FilterNewType
        {
            //Senne & Hermes

            get { return _filterNewType; }
            set
            {
                if (_filterNewType==null)
                {
                    _filterselectedType = new TfgsvType(){Planttypenaam = "Empty"};//zorgen dat hij niet null is
                    FilterSelectedType = null;
                }
                _filterNewType = value;
                OnPropertyChanged();
            }
        }
        public string FilterNewFamilie
        {
            //Senne & Hermes

            get { return _filterNewFamilie; }
            set
            {
                if (_filterNewFamilie == null)
                {
                    _filterselectedFamilie = new TfgsvFamilie() { Familienaam = "Empty" };//zorgen dat hij niet null is
                    FilterSelectedFamilie = null;
                }
                _filterNewFamilie = value;
                OnPropertyChanged();
            }
        }
        public string FilterNewGeslacht
        {
            //Senne & Hermes

            get { return _filterNewGeslacht; }
            set
            {
                if (_filterNewGeslacht == null)
                {
                    _filterselectedGeslacht = new TfgsvGeslacht() { Geslachtnaam = "Empty" };//zorgen dat hij niet null is
                    FilterSelectedGeslacht = null;
                }
                _filterNewGeslacht = value;
                OnPropertyChanged();
            }
        }
        public string FilterNewSoort
        {
            //Senne & Hermes

            get { return _filterNewSoort; }
            set
            {
                if (_filterNewSoort == null)
                {
                    _filterselectedSoort = new TfgsvSoort() { Soortnaam = "Empty" };//zorgen dat hij niet null is
                    FilterSelectedSoort = null;
                }
                _filterNewSoort = value;
                OnPropertyChanged();
            }
        }
        public string FilterNewVariant
        {
            //Senne & Hermes

            get
            {
                return _filterNewVariant;
            }
            set
            {
                if (_filterNewVariant == null)
                {
                    _filterselectedVariant = new TfgsvVariant() { Variantnaam = "Empty" };//zorgen dat hij niet null is
                    FilterSelectedVariant = null;
                }
                _filterNewVariant = value;
                OnPropertyChanged();
            }
        }
        //Fenotype
        //Abio
        public AbioBezonning AbioSelectedBezonning
        {
            //Senne & Hermes

            get { return _abioselectedBezonning; }
            set
            {
                _abioselectedBezonning = value;
                OnPropertyChanged();
            }
        }

        public AbioGrondsoort AbioSelectedGrondsoort
        {
            //Senne & Hermes

            get { return _abioselectedGrondsoort; }
            set
            {
                _abioselectedGrondsoort = value;
                OnPropertyChanged();
            }
        }

        public AbioVoedingsbehoefte AbioSelectedVoedingsbehoefte
        {
            //Senne & Hermes

            get { return _abioselectedVoedingsbehoefte; }
            set
            {
                _abioselectedVoedingsbehoefte = value;
                OnPropertyChanged();
            }
        }

        public AbioVochtbehoefte AbioSelectedVochtbehoefte
        {
            //Senne & Hermes

            get { return _abioselectedVochtbehoefte; }
            set
            {
                _abioselectedVochtbehoefte = value;
                OnPropertyChanged();
            }
        }

        public AbioReactieAntagonischeOmg AbioSelectedReactie
        {
            //Senne & Hermes

            get { return _abioselectedReactie; }
            set
            {
                _abioselectedReactie = value;
                OnPropertyChanged();
            }
        }

        public AbioHabitat AbioSelectedAllHabitat
        {
            //Senne & Hermes

            get { return _abioselectedAllHabitat; }
            set
            {
                _abioselectedAllHabitat = value;
                OnPropertyChanged();
            }
        }

        public AbioHabitat AbioSelectedAddedHabitat
        {
            //Senne & Hermes

            get { return _abioselectedAddedHabitat; }
            set
            {
                _abioselectedAddedHabitat = value;
                OnPropertyChanged();
            }
        }
        //Commersialisme
        //Extra Eigenschappen
        //Beheer Eigenschappen
    }
}

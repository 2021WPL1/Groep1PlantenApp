using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;
using Window = System.Windows.Window;

namespace PlantenApplicatie.UI.ViewModel
{
    public class EditViewModel :ViewModelBase
    {//Senne & Hermes
        private PlantenDataService _plantenDataService;

        //Command voor opslaan
        public ICommand OpslaanCommand { get; set; }
        public RelayCommand<Window> BackCommand { get; private set; }
        public ICommand HabitatToevoegenCommand { get; set; }
        public ICommand HabitatVerwijderenCommand { get; set; }
        public ICommand LevensduurToevoegenCommand { get; set; }
        public ICommand LevensduurVerwijderenCommand { get; set; }
        public ICommand SocialbiliteitToevoegenCommand { get; set; }
        public ICommand SocialbiliteitVerwijderenCommand { get; set; }

        //Observable collections voor de binding
        //Filters
        public ObservableCollection<TfgsvType> FilterTfgsvTypes { get; set; }
        public ObservableCollection<TfgsvFamilie> FilterTfgsvFamilie { get; set; }
        public ObservableCollection<TfgsvGeslacht> FilterTfgsvGeslacht { get; set; }
        public ObservableCollection<TfgsvSoort> FilterTfgsvSoort { get; set; }
        public ObservableCollection<TfgsvVariant> FilterTfgsvVariant { get; set; }
        //Fenotype
        public ObservableCollection<FenoBloeiwijze> FenoBloeiwijze { get; set; }
        public ObservableCollection<FenoHabitus> FenoHabitus { get; set; }
        public ObservableCollection<FenoBladgrootte> FenoBladgrootte { get; set; } //bladhoogte, min & max bloeihoogte
        public ObservableCollection<FenoKleur> FenoKleur { get; set; } //bladkleur & bloeikleur
        public ObservableCollection<FenoMaand> FenoMaand { get; set; } //bladkleur & bloeikleur
        public ObservableCollection<FenoBladvorm> FenoBladvorm { get; set; }
        public ObservableCollection<FenoRatioBloeiBlad> FenoRatio { get; set; }
        public ObservableCollection<FenoSpruitfenologie> FenoSpruit { get; set; }
        public ObservableCollection<FenoLevensvorm> FenoLevensvorm { get; set; }
        //Abio
        public ObservableCollection<AbioBezonning> AbioBezonning { get; set; }
        public ObservableCollection<AbioGrondsoort> AbioGrondsoort { get; set; }
        public ObservableCollection<AbioVoedingsbehoefte> AbioVoedingsbehoefte { get; set; }
        public ObservableCollection<AbioVochtbehoefte> AbioVochtbehoefte { get; set; }
        public ObservableCollection<AbioReactieAntagonischeOmg> AbioReactieAntagonischeOmg { get; set; }
        public ObservableCollection<AbioHabitat> AbioAllHabitats { get; set; }
        public ObservableCollection<AbioHabitat> AbioAddedHabitats { get; set; }
        //Commersialisme
        public ObservableCollection<CommOntwikkelsnelheid> CommOntwikkelSnelheid { get; set; }
        public ObservableCollection<CommLevensvorm> CommAllLevensvorm { get; set; }
        public ObservableCollection<CommLevensvorm> CommAddedLevensvorm { get; set; }
        public ObservableCollection<CommSocialbiliteit> CommAllSocialbiliteit { get; set; }
        public ObservableCollection<CommSocialbiliteit> CommAddedSocialbiliteit { get; set; }
        //Extra Eigenschappen
        public ObservableCollection<ExtraNectarwaarde> ExtraNectarwaarde { get; set; }
        public ObservableCollection<ExtraPollenwaarde> ExtraPollenwaarde { get; set; }
       
        //Beheer Eigenschappen
        /*
         * Beheertype
         * BeheerPlantenNaam
         * BeheerHandeling
         * Maanden
         * Aantal m²/u
         * Frequentie per jaar
         * Opmerking
         */

        //Lists om de observable collections op te vullen
        //Filters
        private List<TfgsvType> _filtertypes;
        private List<TfgsvFamilie> _filterfamilies;
        private List<TfgsvGeslacht> _filtergeslachten;
        private List<TfgsvSoort> _filtersoorten;
        private List<TfgsvVariant> _filtervarianten;
        //Fenotype
        private List<FenoBloeiwijze> _fenoBloeiwijze;
        private ImageSource[] _fenoBloeiAllImages;
        private List<FenoHabitus> _fenoHabitus;
        private List<FenoBladgrootte> _fenoBladgrootte;
        private List<FenoKleur> _fenoKleur;
        private List<FenoMaand> _fenoMaand;
        private List<FenoBladvorm> _fenoBladvorm;
        private List<FenoRatioBloeiBlad> _fenoRatio;
        private List<FenoSpruitfenologie> _fenoSpruit;
        private List<FenoLevensvorm> _fenoLevensvorm;
        //Abio
        private List<AbioBezonning> _abiobezonning;
        private List<AbioGrondsoort> _abiogrondsoort;
        private List<AbioVoedingsbehoefte> _abiovoedingsbehoefte;
        private List<AbioVochtbehoefte> _abiovochtbehoefte;
        private List<AbioReactieAntagonischeOmg> _abioReactie;
        private List<AbioHabitat> _abioAllHabitats;
        private List<AbioHabitat> _abioAddedHabitats;
        //Commersialisme
        private List<CommOntwikkelsnelheid> _commOntwikkelSnelheid;
        private List<CommLevensvorm> _commAllLevensvorm;
        private List<CommLevensvorm> _commAddedLevensvorm;
        private List<CommSocialbiliteit> _commAllSocialbiliteit;
        private List<CommSocialbiliteit> _commAddedSocialbiliteit;
        //Extra Eigenschappen
        private List<ExtraNectarwaarde> _extraNectarwaarde;
        private List<ExtraPollenwaarde> _extraPollenwaarde;
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
        private FenoBloeiwijze _fenoselectedBloeiwijze;
        private ImageSource _fenoselectedBloeiwijzeImage;
        private FenoHabitus _fenoselectedHabitus;
        private FenoBladgrootte _fenoselectedMaxBladgrootte;
        private FenoMaand _fenoselectedMaxBladgrootteMaand;
        private FenoBladgrootte _fenoselectedMaxBloeihoogte;
        private FenoMaand _fenoselectedMaxBloeihoogteMaand;
        private FenoBladgrootte _fenoselectedMinBloeihoogte;
        private FenoMaand _fenoselectedMinBloeihoogteMaand;
        private FenoKleur _fenoselectedBladKleur;
        private FenoMaand _fenoselectedBladKleurMaand;
        private FenoKleur _fenoselectedBloeiKleur;
        private FenoMaand _fenoselectedBloeiKleurMaand;
        private FenoBladvorm _fenoselectedBladvorm;
        private FenoRatioBloeiBlad _fenoselectedRatio;
        private FenoSpruitfenologie _fenoselectedSpruit;
        private FenoLevensvorm _fenoselectedLevensvorm;
        //Abio
        private AbioBezonning _abioselectedBezonning;
        private AbioGrondsoort _abioselectedGrondsoort;
        private AbioVoedingsbehoefte _abioselectedVoedingsbehoefte;
        private AbioVochtbehoefte _abioselectedVochtbehoefte;
        private AbioReactieAntagonischeOmg _abioselectedReactie;
        private AbioHabitat _abioselectedAllHabitat;
        private AbioHabitat _abioselectedAddedHabitat;
        //Commersialisme
        private CommOntwikkelsnelheid _commselectedOntwikkelSnelheid;
        private CommLevensvorm _commselectedAllLevensvorm;
        private CommLevensvorm _commselectedAddedLevensvorm;
        private CommSocialbiliteit _commselectedAllSocialbiliteit;
        private CommSocialbiliteit _commselectedAddedSocialbiliteit;
        //Extra Eigenschappen
        private ExtraNectarwaarde _extraselectedNectarwaarde;
        private ExtraEigenschap _extraselectedBijvriendelijk;
        private ExtraEigenschap _extraselectedEetbaarKruid;
        private ExtraPollenwaarde _extraselectedPollenwaarde;
        private ExtraEigenschap _extraselectedVlindervriendelijk;
        private ExtraEigenschap _extraselectedGeurend;
        private ExtraEigenschap _extraselectedVorstgevoelig;
        //Beheer Eigenschappen

        public EditViewModel(PlantenDataService plantenDataService)
        {
            //Senne & Hermes
            this._plantenDataService = plantenDataService;

            OpslaanCommand = new DelegateCommand(Opslaan);
            BackCommand = new RelayCommand<Window>(Back);
            HabitatToevoegenCommand = new DelegateCommand(HabitatToevoegen);
            HabitatVerwijderenCommand = new DelegateCommand(HabitatVerwijderen);
            LevensduurToevoegenCommand = new DelegateCommand(LevensduurToevoegen);
            LevensduurVerwijderenCommand = new DelegateCommand(LevensduurVerwijderen);
            SocialbiliteitToevoegenCommand = new DelegateCommand(SocialbiliteitToevoegen);
            SocialbiliteitVerwijderenCommand = new DelegateCommand(SocialbiliteitVerwijderen);

            //Filters
            FilterTfgsvTypes = new ObservableCollection<TfgsvType>();
            FilterTfgsvFamilie = new ObservableCollection<TfgsvFamilie>();
            FilterTfgsvGeslacht = new ObservableCollection<TfgsvGeslacht>();
            FilterTfgsvSoort = new ObservableCollection<TfgsvSoort>();
            FilterTfgsvVariant = new ObservableCollection<TfgsvVariant>();
            //Fenotype
            FenoBloeiwijze = new ObservableCollection<FenoBloeiwijze>();
            FenoHabitus = new ObservableCollection<FenoHabitus>();
            FenoBladgrootte = new ObservableCollection<FenoBladgrootte>();
            FenoKleur = new ObservableCollection<FenoKleur>();
            FenoMaand = new ObservableCollection<FenoMaand>();
            FenoBladvorm = new ObservableCollection<FenoBladvorm>();
            FenoRatio = new ObservableCollection<FenoRatioBloeiBlad>();
            FenoSpruit = new ObservableCollection<FenoSpruitfenologie>();
            FenoLevensvorm = new ObservableCollection<FenoLevensvorm>();
            //Abio
            AbioBezonning = new ObservableCollection<AbioBezonning>();
            AbioGrondsoort = new ObservableCollection<AbioGrondsoort>();
            AbioVoedingsbehoefte = new ObservableCollection<AbioVoedingsbehoefte>();
            AbioVochtbehoefte = new ObservableCollection<AbioVochtbehoefte>();
            AbioReactieAntagonischeOmg = new ObservableCollection<AbioReactieAntagonischeOmg>();
            AbioAllHabitats = new ObservableCollection<AbioHabitat>();
            AbioAddedHabitats = new ObservableCollection<AbioHabitat>();
            //Commersialisme
            CommOntwikkelSnelheid = new ObservableCollection<CommOntwikkelsnelheid>();
            CommAllLevensvorm = new ObservableCollection<CommLevensvorm>();
            CommAddedLevensvorm = new ObservableCollection<CommLevensvorm>();
            CommAllSocialbiliteit = new ObservableCollection<CommSocialbiliteit>();
            CommAddedSocialbiliteit = new ObservableCollection<CommSocialbiliteit>();
            //Extra Eigenschappen
            //Beheer Eigenschappen
        }

        public void FillDataFromPlant(Plant plant)
        {
            //Senne & Hermes
            //Filters
            FilterSelectedType = _filtertypes.FirstOrDefault(f =>
                f.Planttypenaam == _plantenDataService.GetFilterType(plant.TypeId).Planttypenaam);
            FilterSelectedFamilie = _filterfamilies.FirstOrDefault(f =>
                f.Familienaam == _plantenDataService.GetFilterFamilie(plant.FamilieId).Familienaam);
            FilterSelectedGeslacht = _filtergeslachten.FirstOrDefault(f =>
                f.Geslachtnaam == _plantenDataService.GetFilterGeslacht(plant.GeslachtId)
                    .Geslachtnaam);
            FilterSelectedSoort = _filtersoorten.FirstOrDefault(f =>
                f.Soortnaam == _plantenDataService.GetFilterSoort(plant.SoortId).Soortnaam);

            if ( _plantenDataService.GetFilterVariant(plant.VariantId) != null)
            {
                FilterSelectedVariant = _filtervarianten.FirstOrDefault(f =>
                    f.Variantnaam == _plantenDataService.GetFilterVariant(plant.VariantId)
                        .Variantnaam);
            }
            //Fenotype
            FenoSelectedBloeiwijze =
                _fenoBloeiwijze.FirstOrDefault(f =>
                    f.Naam == _plantenDataService.GetFenotype(plant.PlantId).Bloeiwijze);
            FenoSelectedHabitus =
                _fenoHabitus.FirstOrDefault(f => f.Naam == _plantenDataService.GetFenotype(plant.PlantId).Habitus);
            FenoSelectedMaxBladgrootte = _fenoBladgrootte.FirstOrDefault(f =>
                f.Bladgrootte == _plantenDataService.GetFenoMaxBladHoogte(plant.PlantId));
            FenoSelectedMaxBladgrootteMaand = _plantenDataService.GetFenoMaxBladHoogteMaand(plant.PlantId);
            FenoSelectedMaxBloeihoogte = _fenoBladgrootte.FirstOrDefault(f =>
                f.Bladgrootte == _plantenDataService.GetFenoMaxBloeiHoogte(plant.PlantId));
            FenoSelectedMaxBloeihoogteMaand = _plantenDataService.GetFenoMaxBloeiHoogteMaand(plant.PlantId);
            FenoSelectedMinBloeihoogte = _fenoBladgrootte.FirstOrDefault(f =>
                f.Bladgrootte == _plantenDataService.GetFenoMinBloeiHoogte(plant.PlantId));
            FenoSelectedMinBloeihoogteMaand = _plantenDataService.GetFenoMinBloeiHoogteMaand(plant.PlantId);
            FenoSelectedBladKleur = _plantenDataService.GetFenoBladKleur(plant.PlantId);
            FenoSelectedBladKleurMaand = _plantenDataService.GetFenoBladMaand(plant.PlantId);
            FenoSelectedBloeiKleur = _plantenDataService.GetFenoBloeiKleur(plant.PlantId);
            FenoSelectedBloeiKleurMaand = _plantenDataService.GetFenoBloeiMaand(plant.PlantId);
            FenoSelectedBladvorm =
                _fenoBladvorm.FirstOrDefault(f => f.Vorm == _plantenDataService.GetFenotype(plant.PlantId).Bladvorm);
            FenoSelectedRatio = _fenoRatio.FirstOrDefault(f =>
                f.Waarde == _plantenDataService.GetFenotype(plant.PlantId).RatioBloeiBlad);
            FenoSelectedSpruit = _fenoSpruit.FirstOrDefault(f =>
                f.Fenologie == _plantenDataService.GetFenotype(plant.PlantId).Spruitfenologie);
            FenoSelectedLevensvorm = _fenoLevensvorm.FirstOrDefault(f =>
                f.Levensvorm == _plantenDataService.GetFenotype(plant.PlantId).Levensvorm);
            //Abio
            AbioSelectedBezonning =
                _abiobezonning.FirstOrDefault(a => a.Naam == _plantenDataService.GetAbiotiek(plant.PlantId).Bezonning);
            AbioSelectedGrondsoort = _abiogrondsoort.FirstOrDefault(a =>
                a.Grondsoort == _plantenDataService.GetAbiotiek(plant.PlantId).Grondsoort);
            AbioSelectedVoedingsbehoefte = _abiovoedingsbehoefte.FirstOrDefault(a =>
                a.Voedingsbehoefte == _plantenDataService.GetAbiotiek(plant.PlantId).Voedingsbehoefte);
            AbioSelectedVochtbehoefte = _abiovochtbehoefte.FirstOrDefault(a =>
                a.Vochtbehoefte == _plantenDataService.GetAbiotiek(plant.PlantId).Vochtbehoefte);
            AbioSelectedReactie = _abioReactie.FirstOrDefault(a =>
                a.Antagonie == _plantenDataService.GetAbiotiek(plant.PlantId).AntagonischeOmgeving);

            foreach (var abiotiekMulti in _plantenDataService.GetAbiotiekMulti(plant.PlantId))
            {
                _abioAddedHabitats.Add(_abioAllHabitats.FirstOrDefault(a => a.Afkorting == abiotiekMulti.Waarde));
            }
            ReloadHabitatlist();
            //Commersialisme
            CommSelectedOntwikkelSnelheid = _commOntwikkelSnelheid.FirstOrDefault(c =>
                c.Snelheid == _plantenDataService.GetCommensalisme(plant.PlantId).Ontwikkelsnelheid);
            foreach (var commMulti in _plantenDataService.GetCommLevensvormFromPlant(plant.PlantId))
            {
                _commAddedLevensvorm.Add(_commAllLevensvorm.FirstOrDefault(c => c.Levensvorm == commMulti.Waarde));
            }
            ReloadLevensvorm();
            foreach (var commensalismeMulti in _plantenDataService.GetCommSocialbiliteitFromPlant(plant.PlantId))
            {
                _commAddedSocialbiliteit.Add(
                    _commAllSocialbiliteit.FirstOrDefault(c => c.Sociabiliteit == commensalismeMulti.Waarde));
            }
            ReloadSocialbiliteit();
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
        private void Back(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
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
        //habitat verwijderen uit de listbox van geselecteerde habitus
        private void HabitatVerwijderen()
        {
            //Senne & Hermes

            if (AbioSelectedAddedHabitat!=null)
            {
                _abioAddedHabitats.Remove(AbioSelectedAddedHabitat);
            }
            ReloadHabitatlist();
        }

        private void LevensduurToevoegen()
        {
            //Senne & Hermes

            if (_commAddedLevensvorm != null)
            {
                if (!_commAddedLevensvorm.Contains(CommSelectedAllLevensvorm))
                {
                    _commAddedLevensvorm.Add(CommSelectedAllLevensvorm);

                }
                else
                {
                    MessageBox.Show("Levensvorm is al toegevoegd");
                }
            }
            ReloadLevensvorm();
        }
        private void LevensduurVerwijderen()
        {
            //Senne & Hermes

            if (CommSelectedAddedLevensvorm != null)
            {
                _commAddedLevensvorm.Remove(CommSelectedAddedLevensvorm);
            }
            ReloadLevensvorm();
        }
        private void SocialbiliteitToevoegen()
        {
            //Senne & Hermes

            if (_commAddedSocialbiliteit != null)
            {
                if (!_commAddedSocialbiliteit.Contains(CommSelectedAllSocialbiliteit))
                {
                    _commAddedSocialbiliteit.Add(CommSelectedAllSocialbiliteit);

                }
                else
                {
                    MessageBox.Show("Socialbiliteit is al toegevoegd");
                }
            }
            ReloadSocialbiliteit();
        }
        private void SocialbiliteitVerwijderen()
        {
            //Senne & Hermes

            if (CommSelectedAddedSocialbiliteit != null)
            {
                _commAddedSocialbiliteit.Remove(CommSelectedAddedSocialbiliteit);
            }
            ReloadSocialbiliteit();
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
            _fenoBloeiAllImages = new ImageSource[6]
            {
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\aar.jpg" ))),
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\scherm.jpg"))),
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\pluim.jpg"))),
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\knop.jpg"))), 
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\margrietachtig.jpg"))),
                new BitmapImage(new Uri(Path.Combine(Environment.CurrentDirectory.Substring(0,Environment.CurrentDirectory.IndexOf("\\bin")),@"View\\Images\\etage.jpg")))
            };
            _fenoBloeiwijze = _plantenDataService.GetFenoBloeiwijze();
            _fenoHabitus = _plantenDataService.GetFenoHabitus();
            _fenoBladgrootte = _plantenDataService.GetFenoBladgrootte();
            _fenoKleur = _plantenDataService.GetFenoKleur();
            _fenoMaand = _plantenDataService.GetFenoMaand();
            _fenoBladvorm = _plantenDataService.GetFenoBladvorm();
            _fenoRatio = _plantenDataService.GetFenoRatio();
            _fenoSpruit = _plantenDataService.GetFenoSpruit();
            _fenoLevensvorm = _plantenDataService.GetFenoLevensvorm();
            //Abio
            _abiobezonning = _plantenDataService.GetAbioBezonning();
            _abiogrondsoort = _plantenDataService.GetAbioGrondsoort();
            _abiovoedingsbehoefte = _plantenDataService.GetAbioVoedingsbehoefte();
            _abiovochtbehoefte = _plantenDataService.GetAbioVochtbehoefte();
            _abioReactie = _plantenDataService.GetAbioReactieAntagonischeOmg();
            _abioAllHabitats = _plantenDataService.GetHabitats();
            _abioAddedHabitats = new List<AbioHabitat>();
            //Commersialisme
            _commOntwikkelSnelheid = _plantenDataService.GetCommOntwikkelSnelheid();
            _commAllLevensvorm = _plantenDataService.GetCommLevensvorm();
            _commAddedLevensvorm = new List<CommLevensvorm>();
            _commAllSocialbiliteit = _plantenDataService.GetCommSocialbiliteit();
            _commAddedSocialbiliteit = new List<CommSocialbiliteit>();
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

            FenoBloeiwijze.Clear();
            FenoHabitus.Clear();
            FenoBladgrootte.Clear();
            FenoKleur.Clear();
            FenoMaand.Clear();
            FenoBladvorm.Clear();
            FenoRatio.Clear();
            FenoSpruit.Clear();
            FenoLevensvorm.Clear();

            //bloeiwijze
            foreach (var bloeiwijze in _fenoBloeiwijze)
            {
                FenoBloeiwijze.Add(bloeiwijze);
            }
            //habitus
            foreach (var habitus in _fenoHabitus)
            {
                FenoHabitus.Add(habitus);
            }
            //bladgrootte
            foreach (var bladgrootte in _fenoBladgrootte)
            {
                FenoBladgrootte.Add(bladgrootte);
            }
            //kleur
            foreach (var kleur in _fenoKleur)
            {
                FenoKleur.Add(kleur);
            }
            //maand
            foreach (var maand in _fenoMaand)
            {
                FenoMaand.Add(maand);
            }
            //bladvorm
            foreach (var bladvorm in _fenoBladvorm)
            {
                FenoBladvorm.Add(bladvorm);
            }
            //ratiobloeiblad
            foreach (var ratio in _fenoRatio)
            {
                FenoRatio.Add(ratio);
            }
            //spruitfenologie
            foreach (var spruit in _fenoSpruit)
            {
                FenoSpruit.Add(spruit);
            }
            //levensvorm volgens R
            foreach (var levensvorm in _fenoLevensvorm)
            {
                FenoLevensvorm.Add(levensvorm);
            }
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
            //Senne & Hermes
            CommOntwikkelSnelheid.Clear();
            CommAllLevensvorm.Clear();
            CommAllSocialbiliteit.Clear();

            //ontwikkelsnelheid
            foreach (var ontwikkelsnelheid in _commOntwikkelSnelheid)
            {
                CommOntwikkelSnelheid.Add(ontwikkelsnelheid);
            }
            //levensvorm
            foreach (var levensvorm in _commAllLevensvorm)
            {
                CommAllLevensvorm.Add(levensvorm);
            }
            //socialbiliteit
            foreach (var socialbiliteit in _commAllSocialbiliteit)
            {
                CommAllSocialbiliteit.Add(socialbiliteit);
            }
        }
        public void ReloadLevensvorm()
        {
            //Senne & Hermes
            //enkel de toegevoegde levensvormen refreshen

            CommAddedLevensvorm.Clear();
            foreach (var levensvorm in _commAddedLevensvorm)
            {
                CommAddedLevensvorm.Add(levensvorm);
            }
        }
        public void ReloadSocialbiliteit()
        {
            //Senne & Hermes
            //enkel de toegevoegde Sociabiliteiten refreshen

            CommAddedSocialbiliteit.Clear();
            foreach (var socialbiliteit in _commAddedSocialbiliteit)
            {
                CommAddedSocialbiliteit.Add(socialbiliteit);
            }
        }
        public void LoadExtraEigenschappen()
        {
            ExtraNectarwaarde.Clear();
            ExtraPollenwaarde.Clear();

            foreach (var nectarwaarde in _extraNectarwaarde)
            {
                ExtraNectarwaarde.Add(nectarwaarde);
            }
            foreach (var pollenwaarde in _extraPollenwaarde)
            {
                ExtraPollenwaarde.Add(pollenwaarde);
            }
        }
        public void LoadBeheerEigenschappen()
        {

        }
        public void FenoShowBloeiwijzeImage()
        {
            switch (_fenoselectedBloeiwijze.Id)
            {
                case 1:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[0];
                    break;
                case 2:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[1];
                    break;
                case 3:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[2];
                    break;
                case 4:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[3];
                    break;
                case 5:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[4];
                    break;
                case 6:
                    FenoSelectedBloeiwijzeImage = _fenoBloeiAllImages[5];
                    break;
                default:
                    FenoSelectedBloeiwijzeImage = null;
                    break;
            }
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
        public FenoBloeiwijze FenoSelectedBloeiwijze
        {
            get { return _fenoselectedBloeiwijze;}
            set
            {
                _fenoselectedBloeiwijze = value;
                OnPropertyChanged();
                FenoShowBloeiwijzeImage();
            }
        }
        public ImageSource FenoSelectedBloeiwijzeImage
        {
            get { return _fenoselectedBloeiwijzeImage; }
            set
            {
                _fenoselectedBloeiwijzeImage = value;
                OnPropertyChanged();
            }
        }
        public FenoHabitus FenoSelectedHabitus
        {
            get { return _fenoselectedHabitus; }
            set
            {
                _fenoselectedHabitus = value;
                OnPropertyChanged();
            }
        }
        public FenoBladgrootte FenoSelectedMaxBladgrootte
        {
            get { return _fenoselectedMaxBladgrootte; }
            set
            {
                _fenoselectedMaxBladgrootte = value;
                OnPropertyChanged();
            }
        }
        public FenoMaand FenoSelectedMaxBladgrootteMaand
        {
            get { return _fenoselectedMaxBladgrootteMaand; }
            set
            {
                _fenoselectedMaxBladgrootteMaand = value;
                OnPropertyChanged();
            }
        }
        public FenoBladgrootte FenoSelectedMaxBloeihoogte
        {
            get { return _fenoselectedMaxBloeihoogte; }
            set
            {
                _fenoselectedMaxBloeihoogte = value;
                OnPropertyChanged();
            }
        }
        public FenoMaand FenoSelectedMaxBloeihoogteMaand
        {
            get { return _fenoselectedMaxBloeihoogteMaand; }
            set
            {
                _fenoselectedMaxBloeihoogteMaand = value;
                OnPropertyChanged();
            }
        }
        public FenoBladgrootte FenoSelectedMinBloeihoogte
        {
            get { return _fenoselectedMinBloeihoogte; }
            set
            {
                _fenoselectedMinBloeihoogte = value;
                OnPropertyChanged();
            }
        }
        public FenoMaand FenoSelectedMinBloeihoogteMaand
        {
            get { return _fenoselectedMinBloeihoogteMaand; }
            set
            {
                _fenoselectedMinBloeihoogteMaand = value;
                OnPropertyChanged();
            }
        }
        public FenoKleur FenoSelectedBladKleur
        {
            get { return _fenoselectedBladKleur; }
            set
            {
                _fenoselectedBladKleur = value;
                OnPropertyChanged();
            }
        }
        public FenoMaand FenoSelectedBladKleurMaand
        {
            get { return _fenoselectedBladKleurMaand; }
            set
            {
                _fenoselectedBladKleurMaand = value;
                OnPropertyChanged();
            }
        }
        public FenoKleur FenoSelectedBloeiKleur
        {
            get { return _fenoselectedBloeiKleur; }
            set
            {
                _fenoselectedBloeiKleur = value;
                OnPropertyChanged();
            }
        }
        public FenoMaand FenoSelectedBloeiKleurMaand
        {
            get { return _fenoselectedBloeiKleurMaand; }
            set
            {
                _fenoselectedBloeiKleurMaand = value;
                OnPropertyChanged();
            }
        }
        public FenoBladvorm FenoSelectedBladvorm
        {
            get { return _fenoselectedBladvorm; }
            set
            {
                _fenoselectedBladvorm = value;
                OnPropertyChanged();
            }
        }
        public FenoRatioBloeiBlad FenoSelectedRatio
        {
            get { return _fenoselectedRatio; }
            set
            {
                _fenoselectedRatio = value;
                OnPropertyChanged();
            }
        }
        public FenoSpruitfenologie FenoSelectedSpruit
        {
            get { return _fenoselectedSpruit; }
            set
            {
                _fenoselectedSpruit = value;
                OnPropertyChanged();
            }
        }
        public FenoLevensvorm FenoSelectedLevensvorm
        {
            get { return _fenoselectedLevensvorm; }
            set
            {
                _fenoselectedLevensvorm = value;
                OnPropertyChanged();
            }
        }
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
        public CommOntwikkelsnelheid CommSelectedOntwikkelSnelheid
        {
            get { return _commselectedOntwikkelSnelheid; }
            set
            {
                _commselectedOntwikkelSnelheid = value;
                OnPropertyChanged();
            }
        }
        public CommLevensvorm CommSelectedAllLevensvorm
        {
            get { return _commselectedAllLevensvorm; }
            set
            {
                _commselectedAllLevensvorm = value;
                OnPropertyChanged();
            }
        }
        public CommLevensvorm CommSelectedAddedLevensvorm
        {
            get { return _commselectedAddedLevensvorm; }
            set
            {
                _commselectedAddedLevensvorm = value;
                OnPropertyChanged();
            }
        }
        public CommSocialbiliteit CommSelectedAllSocialbiliteit
        {
            get { return _commselectedAllSocialbiliteit; }
            set
            {
                _commselectedAllSocialbiliteit = value;
                OnPropertyChanged();
            }
        }
        public CommSocialbiliteit CommSelectedAddedSocialbiliteit
        {
            get { return _commselectedAddedSocialbiliteit; }
            set
            {
                _commselectedAddedSocialbiliteit = value;
                OnPropertyChanged();
            }
        }
        //Extra Eigenschappen
        public ExtraNectarwaarde ExtraSelectedNectarwaarde
        {
            get { return _extraselectedNectarwaarde; }
            set
            {
                _extraselectedNectarwaarde = value;
                OnPropertyChanged();
            }
        }
        public ExtraEigenschap ExtraSelectedBijvriendelijk
        {
            get { return _extraselectedBijvriendelijk; }
            set
            {
                _extraselectedBijvriendelijk = value;
                OnPropertyChanged();
            }
        }
        public ExtraEigenschap ExtraSelectedEetbaarKruid
        {
            get { return _extraselectedEetbaarKruid; }
            set
            {
                _extraselectedEetbaarKruid = value;
                OnPropertyChanged();
            }
        }
        public ExtraPollenwaarde ExtraSelectedPollenwaarde
        {
            get { return _extraselectedPollenwaarde; }
            set
            {
                _extraselectedPollenwaarde = value;
                OnPropertyChanged();
            }
        }
        public ExtraEigenschap ExtraSelectedVlindervriendelijk
        {
            get { return _extraselectedVlindervriendelijk; }
            set
            {
                _extraselectedVlindervriendelijk = value;
                OnPropertyChanged();
            }
        }
        public ExtraEigenschap ExtraSelectedGeurend
        {
            get { return _extraselectedGeurend; }
            set
            {
                _extraselectedGeurend = value;
                OnPropertyChanged();
            }
        }
        public ExtraEigenschap ExtraSelectedVorstgevoelig
        {
            get { return _extraselectedVorstgevoelig; }
            set
            {
                _extraselectedVorstgevoelig = value;
                OnPropertyChanged();
            }
        }

        //Beheer Eigenschappen
    }
}

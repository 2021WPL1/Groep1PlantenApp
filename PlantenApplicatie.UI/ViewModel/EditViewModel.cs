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
    {
        private PlantenDataService _plantenDataService;

        //Command voor opslaan
        public ICommand OpslaanCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand HabitatToevoegenCommand { get; set; }
        public ICommand HabitatVerwijderenCommand { get; set; }

        //Observable collections voor de binding
        //Filters
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
            this._plantenDataService = plantenDataService;

            OpslaanCommand = new DelegateCommand(Opslaan);
            BackCommand = new DelegateCommand(Back);
            HabitatToevoegenCommand = new DelegateCommand(HabitatToevoegen);
            HabitatVerwijderenCommand = new DelegateCommand(HabitatVerwijderen);

            //Filters
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

        private void Opslaan()
        {
            //Filters
            //Fenotype
            //Abio
            var abioselectedBezonning = AbioSelectedBezonning.Naam;
            var abioselectedGrondsoort = AbioSelectedGrondsoort.Grondsoort;
            var abioselectedVoedingbehoefte = AbioSelectedVoedingsbehoefte.Voedingsbehoefte;
            var abioselectedVochtbehoefte = AbioSelectedVochtbehoefte.Vochtbehoefte;
            var abioselectedReactie = AbioSelectedReactie.Antagonie;
            List<string> abioselectedHabitats = new List<string>();
            foreach (var habitat in AbioAddedHabitats)
            {
                if (!abioselectedHabitats.Contains(habitat.Afkorting))
                {
                    abioselectedHabitats.Add(habitat.Afkorting);
                }
            }
            //Commersialisme
            //Extra Eigenschappen
            //Beheer Eigenschappen
        }
        private void Back()
        {

        }
        //Habitat toevoegen aan listbox (die moeten toegevoegd worden aan de plant)
        private void HabitatToevoegen()
        {
            if (_abioselectedAllHabitat!=null)
            {
                _abioAddedHabitats.Add(_abioselectedAllHabitat);
            }
            ReloadHabitatlist();
        }
        //habitat verwijderen uit de listbox van geselecteerde
        private void HabitatVerwijderen()
        {
            if (_abioselectedAddedHabitat!=null)
            {
                _abioAddedHabitats.Remove(_abioselectedAddedHabitat);
            }
            ReloadHabitatlist();
        }

        public void InitializeAll()
        {
            //Filters
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
            LoadFilters();
            LoadFenotype();
            LoadAbio();
            LoadCommersialisme();
            LoadExtraEigenschappen();
            LoadBeheerEigenschappen();
        }

        public void LoadFilters()
        {

        }

        public void LoadFenotype()
        {

        }

        public void LoadAbio()
        {
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
        //Fenotype
        //Abio
        public AbioBezonning AbioSelectedBezonning
        {
            get { return _abioselectedBezonning; }
            set
            {
                _abioselectedBezonning = value;
                OnPropertyChanged();
            }
        }

        public AbioGrondsoort AbioSelectedGrondsoort
        {
            get { return _abioselectedGrondsoort; }
            set
            {
                _abioselectedGrondsoort = value;
                OnPropertyChanged();
            }
        }

        public AbioVoedingsbehoefte AbioSelectedVoedingsbehoefte
        {
            get { return _abioselectedVoedingsbehoefte; }
            set
            {
                _abioselectedVoedingsbehoefte = value;
                OnPropertyChanged();
            }
        }

        public AbioVochtbehoefte AbioSelectedVochtbehoefte
        {
            get { return _abioselectedVochtbehoefte; }
            set
            {
                _abioselectedVochtbehoefte = value;
                OnPropertyChanged();
            }
        }

        public AbioReactieAntagonischeOmg AbioSelectedReactie
        {
            get { return _abioselectedReactie; }
            set
            {
                _abioselectedReactie = value;
                OnPropertyChanged();
            }
        }

        public AbioHabitat AbioSelectedAllHabitat
        {
            get { return _abioselectedAllHabitat; }
            set
            {
                _abioselectedAllHabitat = value;
                OnPropertyChanged();
            }
        }

        public AbioHabitat AbioSelectedAddedHabitat
        {
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

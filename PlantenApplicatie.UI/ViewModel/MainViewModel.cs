using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using PlantenApplicatie.UI.ViewModel;

namespace PlantenApplicatie.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //bool die ervoor zorgt dat de selected filters niet gecleared worden
        private bool _loadCheck;

        private PlantenDataService _plantenDataService;

        //lists met alle tfgsv in
        private List<TfgsvType> _types;
        private List<TfgsvFamilie> _families;
        private List<TfgsvGeslacht> _geslachten;
        private List<TfgsvSoort> _soorten;
        private List<TfgsvVariant> _varianten;

        private List<Plant> _plantResults;
             
        //list voor de binding
        public ObservableCollection<TfgsvType> TfgsvTypes { get; set; }
        public ObservableCollection<TfgsvFamilie> TfgsvFamilie { get; set; }
        public ObservableCollection<TfgsvGeslacht> TfgsvGeslacht { get; set; }
        public ObservableCollection<TfgsvSoort> TfgsvSoort { get; set; }
        public ObservableCollection<TfgsvVariant> TfgsvVariant { get; set; }
        public ObservableCollection<Plant> PlantResults { get; set; }

        //geselecteerde filters
        private TfgsvType _selectedType;
        private TfgsvFamilie _selectedFamilie;
        private TfgsvGeslacht _selectedGeslacht;
        private TfgsvSoort _selectedSoort;
        private TfgsvVariant _selectedVariant;

        public MainViewModel(PlantenDataService plantenDataService)
        {
            TfgsvTypes = new ObservableCollection<TfgsvType>();
            TfgsvFamilie = new ObservableCollection<TfgsvFamilie>();
            TfgsvGeslacht = new ObservableCollection<TfgsvGeslacht>();
            TfgsvSoort = new ObservableCollection<TfgsvSoort>();
            TfgsvVariant = new ObservableCollection<TfgsvVariant>();
            PlantResults = new ObservableCollection<Plant>();

            this._plantenDataService = plantenDataService;
        }

        public void LoadAll()
        {
            var selectedType = _selectedType;
            LoadTypes();
            SelectedType = selectedType;

            var selectedFamilie = _selectedFamilie;
            LoadFamilies();
            SelectedFamilie = selectedFamilie;

            var selectedGeslacht = _selectedGeslacht;
            LoadGeslachten();
            SelectedGeslacht = selectedGeslacht;

            var selectedSoort = _selectedSoort;
            LoadSoorten();
            SelectedSoort = selectedSoort;

            var selectedVariant = _selectedVariant;
            LoadVarianten();
            SelectedVariant = selectedVariant;
        }

        public void InitializeTfgsv()
        {
            _loadCheck = true;
            _types = _plantenDataService.GetTfgsvTypes();
            _families = _plantenDataService.GetTfgsvFamilies();
            _geslachten = _plantenDataService.GetTfgsvGeslachten();
            _soorten = _plantenDataService.GetTfgsvSoorten();
            _varianten = _plantenDataService.GetTfgsvVarianten();
            _plantResults = _plantenDataService.GetPlanten();
        }

        public void LoadTypes()
        {
            var types = _types;
            TfgsvTypes.Clear();
            foreach (var tfgsvType in types)
            {
                TfgsvTypes.Add(tfgsvType);
            }
        }

        public void LoadFamilies()
        {
            var families = _families;
            TfgsvFamilie.Clear();
            foreach (var tfgsvFamily in families)
            {
                TfgsvFamilie.Add(tfgsvFamily);
            }
        }

        public void LoadGeslachten()
        {
            var geslachten = _geslachten;
            TfgsvGeslacht.Clear();
            foreach (var tfgsvGeslacht in geslachten)
            {
                TfgsvGeslacht.Add(tfgsvGeslacht);
            }
        }

        public void LoadSoorten()
        {
            var soorten = _soorten;
            TfgsvSoort.Clear();
            foreach (var tfgsvSoort in soorten)
            {
                TfgsvSoort.Add(tfgsvSoort);
            }
        }

        public void LoadVarianten()
        {
            var varianten = _varianten;
            TfgsvVariant.Clear();
            foreach (var tfgsvVariant in varianten)
            {
                TfgsvVariant.Add(tfgsvVariant);
            }
        }

        public TfgsvType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged();
                if (_selectedType != null && _loadCheck)
                {
                    UpdateFilters("Type");
                }
            }
        }
        public TfgsvFamilie SelectedFamilie
        {
            get { return _selectedFamilie; }
            set
            {
                _selectedFamilie = value;
                OnPropertyChanged();
                if (_selectedFamilie != null && _loadCheck)
                {
                    UpdateFilters("Familie");
                }
            }
        }
        public TfgsvGeslacht SelectedGeslacht
        {
            get { return _selectedGeslacht; }
            set
            {
                _selectedGeslacht = value;
                OnPropertyChanged();
                if (_selectedGeslacht != null && _loadCheck)
                {
                    UpdateFilters("Geslacht");
                }
            }
        }
        public TfgsvSoort SelectedSoort
        {
            get { return _selectedSoort; }
            set
            {
                _selectedSoort = value;
                OnPropertyChanged();
                if (_selectedSoort != null && _loadCheck)
                {
                    UpdateFilters("Soort");
                }
            }
        }
        public TfgsvVariant SelectedVariant
        {
            get { return _selectedVariant; }
            set
            {
                _selectedVariant = value;
                OnPropertyChanged();
                if (_selectedVariant != null && _loadCheck)
                {
                    UpdateFilters("Variant");
                }
            }
        }

        private void UpdateFilters(string typefilter)
        {
            //toont enkel de filters volgens gekozen bovenstaande filters
            ListResults();
            switch (typefilter)
            {
                case "Type":
                    var fgsv = _plantenDataService.GetFilteredFamilies(_selectedType.Planttypeid);

                    SelectedFamilie = null;
                    SelectedGeslacht = null;
                    SelectedSoort = null;
                    SelectedVariant = null;

                    _families = (List<TfgsvFamilie>)fgsv[0];
                    _geslachten = (List<TfgsvGeslacht>)fgsv[1];
                    _soorten = (List<TfgsvSoort>)fgsv[2];
                    _varianten = (List<TfgsvVariant>)fgsv[3];
                    break;
                case "Familie":
                    var gsv = _plantenDataService.GetFilteredGeslachten(_selectedFamilie.FamileId);

                    SelectedGeslacht = null;
                    SelectedSoort = null;
                    SelectedVariant = null;

                    _geslachten = (List<TfgsvGeslacht>)gsv[0];
                    _soorten = (List<TfgsvSoort>)gsv[1];
                    _varianten = (List<TfgsvVariant>)gsv[2];
                    break;
                case "Geslacht":
                    var sv = _plantenDataService.GetFilteredSoorten(_selectedGeslacht.GeslachtId);

                    SelectedSoort = null;
                    SelectedVariant = null;

                    _soorten = (List<TfgsvSoort>)sv[0];
                    _varianten = (List<TfgsvVariant>)sv[1];
                    break;
                case "Soort":
                    SelectedVariant = null;

                    _varianten = _plantenDataService.GetFilteredVarianten(_selectedSoort.Soortid);
                    break;
            }

            _loadCheck = false;
            LoadAll();
            _loadCheck = true;
        }

        List<Plant> plantTypeResults;
        List<Plant> plantFamilieResults;
        List<Plant> plantGeslachtResults;
        List<Plant> plantSoortResults;
        List<Plant> plantVariantResults;

        public void LoadPlanten()
        {
            var plantResults = _plantResults;
            PlantResults.Clear();
            foreach (var plantResult in plantResults)
            {
                PlantResults.Add(plantResult);
            }
        }
        private void ListResults()
        {
            
            if (_selectedType != null)
            {
                _plantResults = _plantenDataService.GetPlantResults("Type", _selectedType.Planttypeid, _plantResults);
                plantTypeResults = _plantResults;
            }
            if (_selectedFamilie != null)
            {
                _plantResults = _plantenDataService.GetPlantResults("Familie", _selectedType.Planttypeid, _plantResults);
                plantFamilieResults = _plantResults;
            }
            if (_selectedGeslacht != null)
            {
                _plantResults = _plantenDataService.GetPlantResults("Geslacht", _selectedType.Planttypeid, _plantResults);
                plantGeslachtResults = _plantResults;
            }
            if (_selectedSoort != null)
            {
                _plantResults = _plantenDataService.GetPlantResults("Soort", _selectedType.Planttypeid, _plantResults);
                plantSoortResults = _plantResults;
            }
            if (_selectedVariant != null)
            {
                _plantResults = _plantenDataService.GetPlantResults("Variant", _selectedType.Planttypeid, _plantResults);
                plantVariantResults = _plantResults;
            }
            LoadPlanten();
        }
    }
}

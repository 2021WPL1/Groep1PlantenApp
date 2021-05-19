using PlantenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PlantenApplicatie.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void fillLabels(Plant plant)
        {
            bool enter = false;
            for (int i = 0; i < plant.Fgsv.Length; i++)
            {
                string letter = plant.Fgsv.Substring(i, 1);
                if (letter == " " && i >= 25 && enter == false)
                {
                    enter = true;
                }
                else
                {
                    if (enter != true)
                    {
                        lblLatinName.Content += letter;
                    }
                    else
                    {
                        lblLatinName2.Content += letter;
                    }
                }
            }
            if (plant.Type != null)
            {
                lblType.Content = plant.Type;
            }
            else
            {
                lblType.Content = " ";
            }

            lblType.Content = plant.Type;
            lblFamily.Content = plant.Familie;
            lblGeslacht.Content = plant.Geslacht;
            lblSoort.Content = plant.Soort;
            lblVariant.Content = plant.Variant;
            lblPlantdichtheidMax.Content = plant.PlantdichtheidMax;
            lblPlantdichtheidMin.Content = plant.PlantdichtheidMin;
        }
    }
}
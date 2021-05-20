using System;
using System.Collections.Generic;
using System.Text;
using PlantenApplicatie.Data;
using PlantenApplicatie.Domain.Models;
using Prism.Commands;

namespace PlantenApplicatie.UI.ViewModel
{
    class ResultaatViewModel : ViewModelBase
    {
        public Plant PlantenResultaat;
        public ResultaatViewModel(PlantenDataService plantenDataService)
        {
            PlantenResultaat = new Plant();
        }

        //Stephanie en Maarten
        //Hiermee krijg je een plant terug adhv zijn Id.
        /*public Plant GetPlantWithId(long Id)
        {
            return context.Plant.SingleOrDefault(p => p.PlantId == Id);
        }*/
        public void fillLabels(Plant plant)
        {
            PlantenResultaat = plant;
            /*bool enter = false;
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
            lblPlantdichtheidMin.Content = plant.PlantdichtheidMin;*/
        }
    }
}

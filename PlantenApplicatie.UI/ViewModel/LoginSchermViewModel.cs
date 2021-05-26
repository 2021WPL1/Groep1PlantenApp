using PlantenApplicatie.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantenApplicatie.UI.ViewModel
{
    class LoginSchermViewModel : ViewModelBase
    {
        private PlantenDataService _plantenDataService; 
        public LoginSchermViewModel(PlantenDataService plantenDataService)
        {
            this._plantenDataService = plantenDataService;
        }
    }
}

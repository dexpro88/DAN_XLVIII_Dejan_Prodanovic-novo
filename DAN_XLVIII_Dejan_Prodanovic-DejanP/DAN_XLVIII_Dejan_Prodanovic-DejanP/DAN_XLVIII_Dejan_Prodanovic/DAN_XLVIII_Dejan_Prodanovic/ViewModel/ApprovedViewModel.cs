using DAN_XLVIII_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XLVIII_Dejan_Prodanovic.ViewModel
{
    class ApprovedViewModel:ViewModelBase
    {
        ApprovedWindow view;
        public ApprovedViewModel(ApprovedWindow view)
        {
            this.view = view;
            
        }
    }
}

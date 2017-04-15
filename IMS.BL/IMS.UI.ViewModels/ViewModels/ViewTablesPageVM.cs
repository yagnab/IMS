using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMS.BL;

namespace IMS.UI.ViewModels
{
    public class ViewTablesPageVM : INotifyPropertyChanged
    {
        private List<object> _TableOptions;
        public List<object> TableOptions
        {
            get
            {
                return _TableOptions;
            }
            set
            {
                _TableOptions = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> _CurrentTable;
        public List<dynamic> CurrentTable
        {
            get
            {
                return _CurrentTable;
            }
            set
            {
                _CurrentTable = value;
                OnPropertyChanged();
            }
        }

        //will tell view that a property has changed
        //may need to rerender a control
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}

using ExpenseTracker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ExpenseTracker.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class AddExpenseViewModel : ViewModelBase
    {
        #region Fields
        private const string _selectStr = "Select..";
        private readonly IDataService _dataService;
        private IPageNavigationService _navigationService;
        #endregion

        #region Properties
        private string _cost;

        public string Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        private List<string> _types;

        public IEnumerable<string> Types
        {
            get
            {
                if (_types == null)
                {
                    _types = new List<string>() { _selectStr };
                    _types.AddRange(Utils.GetExpenseTypes());
                }
                return _types;
            }
        }

        private string _selectedType;

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                RaisePropertyChanged("SelectedType");
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (String.IsNullOrWhiteSpace(Description) || SelectedType == _selectStr || String.IsNullOrWhiteSpace(Cost))
                    {
                        MessageBox.Show("Please fill all the fileds.");
                        return;
                    }
                    _dataService.AddData(int.Parse(_cost), _selectedType, _description, _date.ToString("yyyy-MM-dd"));
                    ClearData();
                });
            }
        }

        public ICommand SummaryCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _navigationService.NavigateTo("Summary");
                });
            }
        }

        public bool IsSaveBtnEnabled
        {
            get { return true; }
        }

        public bool IsSummaryBtnEnbled
        {
            get { return true; }
        }
        #endregion

        #region Constructors and Methods
        /// <summary>
        /// Initializes a new instance of the AddExpenseViewModel class.
        /// </summary>
        public AddExpenseViewModel(IDataService dataService, IPageNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            ClearData();
        }

        private void ClearData()
        {
            Description = Cost = String.Empty;
            Date = DateTime.Now;
            SelectedType = _selectStr;
        }
        #endregion
    }
}
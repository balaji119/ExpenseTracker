using ExpenseTracker.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExpenseTracker.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class SummaryViewModel : ViewModelBase
    {

        #region Fields
        private const string _allTypes = "All types";
        private IDataService _dataService;
        private IPageNavigationService _navigationService;
        #endregion

        #region Properties
        private IEnumerable<Expense> _expenseList;

        public IEnumerable<Expense> ExpenseList
        {
            get
            {
                return _expenseList = GetExpList();
            }
            set
            {
                _expenseList = value;
                RaisePropertyChanged("ExpenseList");
            }
        }

        private List<string> _types;

        public IEnumerable<string> Types
        {
            get
            {
                if (_types == null)
                {
                    _types = new List<string>() { _allTypes };
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
                ExpenseList = GetExpList();
                RaisePropertyChanged("SelectedType");
            }
        }

        private int _TotalExpense;

        public int TotalExpense
        {
            get { return _TotalExpense; }
            set
            {
                _TotalExpense = value;
                RaisePropertyChanged("TotalExpense");
            }
        }
        #endregion

        #region Constructors and Mehtods
        /// <summary>
        /// Initializes a new instance of the SummaryViewModel class.
        /// </summary>
        public SummaryViewModel(IDataService dataService, IPageNavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        private IEnumerable<Expense> GetExpList()
        {
            List<Expense> expensefil = null;
            _dataService.GetData((expences, exp) =>
            {
                expensefil = expences.ToList();
                if (_selectedType != _allTypes)
                {
                    expensefil = expensefil.Where(a => a.Type == _selectedType).ToList();
                }
                TotalExpense = expensefil.Sum(x => x.Amount);
            });
            return expensefil;
        }
        #endregion
    }
}
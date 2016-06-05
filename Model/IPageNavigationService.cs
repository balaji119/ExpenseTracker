using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Model
{
    public interface IPageNavigationService : INavigationService
    {
        object Parameter { get; }  
    }
}

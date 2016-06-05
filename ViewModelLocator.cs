/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:ExpenseTracker.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using ExpenseTracker.Model;
using System;

namespace ExpenseTracker.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IDataService, ExpenseDataService>();
            SimpleIoc.Default.Register<AddExpenseViewModel>();
            SimpleIoc.Default.Register<SummaryViewModel>();

            var navigationService = new PageNavigationService();
            navigationService.Configure("Summary", new Uri("View/SummaryView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IPageNavigationService>(() => navigationService);
        }

        /// <summary>
        /// Gets the AddExpenseViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AddExpenseViewModel AddExpenseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddExpenseViewModel>();
            }
        }

        /// <summary>
        /// Gets the SummaryViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SummaryViewModel SummaryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SummaryViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            DatabaseHandler.It.Dispose();
        }
    }
}
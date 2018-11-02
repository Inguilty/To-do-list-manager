using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TodoListManager.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private string _alert;
        protected readonly IMvxNavigationService NavigationService;

        public BaseViewModel()
        {
                
        }
        protected BaseViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public string AlertMessage
        {
            get => _alert;
            set
            {
                _alert = value;
                RaisePropertyChanged();
            }
        }
        public string Title { get; protected set; }
        public virtual void OnResume()
        {
            RaiseAllPropertiesChanged();
        }

        public virtual void ViewDispose()
        {
            NavigationService.Close(this);
        }
        public virtual void Validate() { }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TodoListManager.Core.ViewModels
{
    public abstract class BaseViewModel<TParameter> : MvxViewModel<TParameter>
    {
        private string _alert;
        protected readonly IMvxNavigationService NavigationService;

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

        protected virtual void ViewDispose(IMvxViewModel model)
        {
            NavigationService.Close(model);
        }
        public virtual void Validate() { }
    }
}

﻿using System;
using System.Linq;
using XamlActions.DI;
using XamlActions.ViewServices;

namespace XamlActions {
    public class ViewModelBase : ObservableObject {
        private bool _isBusy;
        private bool _notifyUsingDispatcher = true;
        private static INavigationService _navigationService;
        private static IDialogService _dialogService;
        private static IDispatcher _dispatcher;
        private static IDesignModeChecker _designModeChecker;

        static ViewModelBase() {
            DefaultRegistration.EnsureRegistered();
            _navigationService = ServiceLocator.Default.Resolve<INavigationService>();
            _dialogService = ServiceLocator.Default.Resolve<IDialogService>();
            _dispatcher = ServiceLocator.Default.Resolve<IDispatcher>();
            _designModeChecker = ServiceLocator.Default.Resolve<IDesignModeChecker>();
        }

        public bool IsBusy {
            get { return _isBusy; }
            set {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public bool NotifyUsingDispatcher {
            get { return _notifyUsingDispatcher; }
            set { _notifyUsingDispatcher = value; }
        }

        public INavigationService NavigationService {
            get { return _navigationService; }
            set { _navigationService = value; }
        }

        public IDialogService DialogService {
            get { return _dialogService; }
            set { _dialogService = value; }
        }

        public IDispatcher Dispatcher {
            get { return _dispatcher; }
            set { _dispatcher = value; }
        }

        protected override void RaisePropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression) {
            Action action = () => RaisePropertyChangedToAvoidCs1911(propertyExpression);
            if (NotifyUsingDispatcher && _dispatcher != null && !IsInDesignMode()) {
                _dispatcher.Run(action);
                return;
            }
            action.Invoke();
        }

        //http://msdn.microsoft.com/en-us/library/ms228459(v=vs.90).aspx
        private void RaisePropertyChangedToAvoidCs1911<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression) {
            base.RaisePropertyChanged(propertyExpression);
        }

        public static bool IsInDesignMode() {
            return _designModeChecker.IsInDesignMode();
        }
    }
}

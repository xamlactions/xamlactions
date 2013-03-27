using System;

namespace XamlActions.ViewServices {
    public interface IDispatcher {
        void Run(Action action);
    }
}
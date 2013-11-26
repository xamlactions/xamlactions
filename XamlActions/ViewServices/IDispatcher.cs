using System;
using System.Threading.Tasks;

namespace XamlActions.ViewServices {
    public interface IDispatcher {
        Task Run(Action action);
    }
}
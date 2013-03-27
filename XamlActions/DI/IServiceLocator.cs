using System;

namespace XamlActions.DI {
    public interface IServiceLocator {
        void Register<T, TClass> () where T: class where TClass : T;
        TIntf Resolve<TIntf>() where TIntf : class;
        void Register<T>(T instance);
        void Register<T>(Type type);
        void Register<T>(Func<T> functionToCreateObject) where T : class;
    }
}
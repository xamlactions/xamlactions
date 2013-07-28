using System;

namespace XamlActions.DI {
    public interface IServiceLocator {
        void Register<T, TClass> () where T: class where TClass : T;
        TIntf Resolve<TIntf>() where TIntf : class;
        object Resolve(Type type);
        void Register<T>(T instance);
        void Register<T>(Type type);
        void Register<T>(Func<object> functionToCreateObject) where T : class;
        bool IsRegistered<T>();
    }
}
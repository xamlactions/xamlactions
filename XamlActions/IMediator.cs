using System;

namespace XamlActions {
    public interface IMediator {
        void Subscribe<T>(Action<T> action);
        void Publish(object message);
    }
}
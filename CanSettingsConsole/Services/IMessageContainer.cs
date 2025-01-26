using System;

namespace CanSettingsConsole2.Services
{
    public interface IMessageContainer
    {
        void RegisterMessage(string message, Action<object> action);
        void InvokeMessage(string message);
    }
}

using System;
using System.Collections.Generic;

namespace CanSettingsConsole2.Services
{
    public class MessageContainer
        : IMessageContainer
    {
        public const string PLUS_MESSAGE = nameof(PLUS_MESSAGE);
        public const string MINUS_MESSAGE = nameof(MINUS_MESSAGE);
        public const string SAVE_MESSAGE = nameof(SAVE_MESSAGE);
        public static readonly MessageContainer Instance = new MessageContainer();  
        static readonly Dictionary<string, List<Action<object>>> _storage = new Dictionary<string, List<Action<object>>> ();
        public void InvokeMessage(string message)
        {
            _storage.TryGetValue(message, out var list);

            list?.ForEach(action => { action(message); });
        }

        public void RegisterMessage(string message, Action<object> action)
        {
            _storage.TryGetValue(message , out var list);
            if (list == null)
            {
                list = new List<Action<object>>();
                _storage[message] = list;
            }

            list.Add(action);
        }
    }
}

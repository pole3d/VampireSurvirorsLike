using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityCommonTools;

namespace UnityCommon
{
    class GlobalEvent : SimpleSingleton<GlobalEvent>
    {
        Dictionary<string, Action> _events = new Dictionary<string, Action>();

        public static void Register(string name , Action action)
        {
            Instance._events.Add(name, action);
        }
        
        public static void UnRegister(string name)
        {
            Instance._events.Remove(name);

        }

        public static void Play(string name)
        {
            if (Instance._events.ContainsKey(name) == false)
                return;

            Instance._events[name]?.Invoke();
        }


    }
}

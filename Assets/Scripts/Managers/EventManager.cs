using System.Collections.Generic;
using System;
using Lfish.Pattern;

namespace Lfish.Manager
{
    public class EventManager : SingletonCommon<EventManager>
    {
        private Dictionary<EventTypes, Action<object>> m_dict = new Dictionary<EventTypes, Action<object>>();

        public void StartListener(EventTypes eventType, Action<object> action)
        {
            if (!m_dict.ContainsKey(eventType))
            {
                m_dict.Add(eventType, action);
            }
            else
            {
                m_dict[eventType] += action;
            }
        }

        public void StopListener(EventTypes eventType, Action<object> action)
        {
            if (m_dict.ContainsKey(eventType))
            {
                m_dict[eventType] -= action;
                if (m_dict[eventType] == null)
                    m_dict.Remove(eventType);
            }
        }

        public void StopListener(EventTypes eventType)
        {
            if (m_dict.ContainsKey(eventType))
            {
                m_dict.Remove(eventType);
            }
        }

        public void FireEvent(EventTypes eventType, object args = null)
        {
            Action<object> action;
            if (m_dict.TryGetValue(eventType, out action))
            {
                action.Invoke(args);
            }
        }
    }
}

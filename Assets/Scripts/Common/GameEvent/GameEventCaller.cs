using Common.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class NamedGameEvent
{
    public string Name => _name;
    public List<GameFeedback> Events => _events;

    [SerializeReference] string _name;
    [SerializeReference][Instantiable(type: typeof(GameFeedback))] List<GameFeedback> _events;
}


public class GameEventCaller : MonoBehaviour
{
    [SerializeField] List<NamedGameEvent> _events;
    [SerializeField] string _default;

    void Start()
    {
        if (string.IsNullOrEmpty(_default) == false)
        {
            var namedEvents = GetGameEvent(_default);
            GameEventsManager.PlayEvents(namedEvents.Events, gameObject);
        }
    }


    public void PlayEvent(string name)
    {
        var namedEvents = GetGameEvent(name);
        if ( namedEvents == null)
        {
            Debug.LogError($"can't find event {name}");
            return;
        }

        GameEventsManager.PlayEvents(namedEvents.Events, gameObject);
    }

    NamedGameEvent GetGameEvent(string name)
    {
        return _events.FirstOrDefault( (e) => e.Name == name);
    }


}

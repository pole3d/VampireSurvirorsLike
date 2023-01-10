using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    static GameEventsManager s_instance;

    public static GameEventsManager Instance => s_instance;
    public List<GameEvent> GameEvents;
    public List<GameObject> Parameters;
    public HashSet<string> Triggers = new HashSet<string>();

    Dictionary<string, GameObject> _gameObjectParameters = new Dictionary<string, GameObject>();
    Dictionary<string, GameEvent> _events;
    List<GameEventInstance> _currentEvents = new List<GameEventInstance>();

    private void Awake()
    {
        s_instance = this;

        _events = new Dictionary<string, GameEvent>(GameEvents.Count);

        foreach (var gameEvent in GameEvents)
        {
            _events.Add(gameEvent.name.ToLower(), gameEvent);
        }

        foreach (var parameter in Parameters)
        {
            AddParameter(parameter);
        }
    }

    public static bool TryGetParameter(string name , out GameObject go)
    {
        s_instance._gameObjectParameters.TryGetValue(name, out go);
        
        return go != null;
    }

    public static void AddParameter(GameObject go)
    {
        if (s_instance._gameObjectParameters.ContainsKey(go.name) == false)
            s_instance._gameObjectParameters.Add(go.name, go);
        else
        {
            s_instance._gameObjectParameters[go.name] = go;
        }
    }

    public static void SetTrigger(string trigger)
    {
        if (s_instance.Triggers.Contains(trigger))
            return;

        s_instance.Triggers.Add(trigger);
    }

    public static void PlayEvent(string eventName)
    {
        PlayEvent(eventName, null, null);
    }

    public static void PlayEvent(string eventName, MonoBehaviour behavior)
    {
        PlayEvent(eventName, behavior.gameObject);
    }

    public static void PlayEventIfNotPlaying(string eventName, MonoBehaviour behavior)
    {
        if (IsPlaying(behavior))
        {
            return;
        }

        PlayEvent(eventName, behavior.gameObject);
    }

    static bool IsPlaying(MonoBehaviour behavior)
    {
        foreach (var item in s_instance._currentEvents)
        {
            if (item.GameObject == behavior.gameObject)
                return true;
        }

        return false;
    }

    public static GameEventInstance PlayEvent(string eventName, GameObject gameObject, params GameObject[] parameters)
    {
        if (s_instance._events.TryGetValue(eventName.ToLower(), out GameEvent gameEvent))
        {
            return PlayEvent(gameEvent, gameObject, parameters);
        }
        else
        {
            Debug.LogError($"can't find event {eventName}");
            return null;
        }
    }

    public static void StopEventsWithTag(string tag)
    {
        for (int i = s_instance._currentEvents.Count - 1; i >= 0; i--)
        {
            if (s_instance._currentEvents[i].Tag == tag)
                s_instance._currentEvents.RemoveAt(i);
        }
    }

    public static GameEventInstance PlayEvent(GameEvent @event, GameObject gameObject, params GameObject[] parameters)
    {
        return PlayEvents(@event.Feedbacks, gameObject, parameters);
    }

    public static GameEventInstance PlayEvents(List<GameFeedback> feedbacks, GameObject gameObject,
        params GameObject[] parameters)
    {
        GameEventInstance instance = new GameEventInstance(@feedbacks);

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                if (parameter != null)
                    instance.AddParameter(parameter);
            }
        }

        if (gameObject != null)
            instance.PushGameObject(gameObject);

        if (instance.Execute() == false)
        {
            s_instance._currentEvents.Add(instance);
        }

        return instance;
    }

    private void Update()
    {
        for (int i = _currentEvents.Count - 1; i >= 0; i--)
        {
            if (_currentEvents[i].Execute())
                _currentEvents.RemoveAt(i);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventInstance
{
    public bool EventResult = true;
    public List<GameFeedback> Feedbacks;
    public GameObject GameObject => _gameobjects.Count > 0 ? _gameobjects.Peek() : null;
    public Dictionary<string, float> Values = new Dictionary<string, float>();
    public Dictionary<string, GameObject> _gameObjectParameters = new Dictionary<string, GameObject>();
    public string Tag => _tag;

    Stack<GameObject> _gameobjects = new Stack<GameObject>();
    int _currentIndex;
    string _tag;

    public GameEventInstance(List<GameFeedback> feedbacks)
    {
        Feedbacks = feedbacks;
    }

    public void SetTag(string tag)
    {
        _tag = tag;
    }
    
    public void Stop()
    {
        _currentIndex = Feedbacks.Count;
    }

    public void AddParameter(GameObject go)
    {
        if (_gameObjectParameters.ContainsKey(go.name) == false)
            _gameObjectParameters.Add(go.name, go);
        else
        {
            _gameObjectParameters[go.name] = go;
        }
    }


    public void PushGameObject(string name)
    {
        if (_gameObjectParameters.TryGetValue(name, out GameObject value))
        {
            _gameobjects.Push(value);
        }
        else if (GameEventsManager.TryGetParameter(name, out value))
        {
            _gameobjects.Push(value);
        }
        else
        {
            GameObject go = GameObject;

            Transform child = null;

            if (go != null)
                child = go.transform.Find(name);

            go = null;

            if (child != null)
                go = child.gameObject;

            if (go == null)
                go = GameObject.Find(name);

            if (go == null)
            {
                go = FindInactive(name);
                Debug.LogError($"{name} found in inactive gameobject");

            }

            if (go == null)
            {
                Debug.LogError($"Can't find {name}");
                return;
            }

            _gameobjects.Push(go);
        }
    }

    GameObject FindInactive(string name)
    {

            UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
 
            GameObject[] rootObjects = activeScene.GetRootGameObjects();
 
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
 
 
            for (int i = 0; i < rootObjects.Length; i++)
            {
                if (rootObjects[i].name == name)
                    return rootObjects[i];
            }
 
            for (int i = 0; i < allObjects.Length; i++)
            {
                if (allObjects[i].transform.root)
                {
                    for (int i2 = 0; i2 < rootObjects.Length; i2++)
                    {
                        if (allObjects[i].transform.root == rootObjects[i2].transform && allObjects[i] != rootObjects[i2])
                        {
                            if (allObjects[i].name == name)
                                return allObjects[i];
                        }
                    }
                }
            }

            return null;
    }

    public void PopGameObject()
    {
        _gameobjects.Pop();
    }

    public bool CheckTrigger(string trigger)
    {
        if (GameEventsManager.Instance.Triggers.Contains(trigger))
        {
            GameEventsManager.Instance.Triggers.Remove(trigger);
            return true;
        }

        return false;
    }

    public float GetValue(string name)
    {
        if (Values.TryGetValue(name, out float value))
            return value;
        return 0;
    }

    public void SetValue(string name, float value)
    {
        Values[name] = value;
    }

    public void AddValue(string name, float value)
    {
        Values[name] = GetValue(name) + value;
    }

    public bool Execute()
    {
        for (; _currentIndex < Feedbacks.Count; _currentIndex++)
        {
            if (Feedbacks[_currentIndex].Enabled == false)
                continue;

            GameFeedback current = Feedbacks[_currentIndex];

            if (current is ConditionalFeedback)
            {
                bool isTrue = current.Execute(this);

                if (isTrue == false)
                {
                    while (current is EndCondition == false && _currentIndex < Feedbacks.Count - 1)
                    {
                        _currentIndex++;
                        current = Feedbacks[_currentIndex];
                    }
                }
            }
            else
            {
                bool continueFeedbacks = current.Execute(this);
                if (continueFeedbacks == false)
                    return false;
            }
        }

        return _currentIndex >= Feedbacks.Count;
    }

    public void PushGameObject(GameObject gameObject)
    {
        _gameobjects.Push(gameObject);
    }
}
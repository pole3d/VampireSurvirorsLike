using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagement : MonoBehaviour
{
    public static ScenesManagement Instance
    {
        get
        {
            if (s_instance == null)
            {
                GameObject go = new GameObject("ScenesManagement");
                go.AddComponent<ScenesManagement>();

            }

            return s_instance;
        }
    }
    static ScenesManagement s_instance;

    //public Dictionary<string, StoredData> _dataContainer = new Dictionary<string, StoredData>();
    public Dictionary<string, object> _dataContainer = new Dictionary<string, object>();
    
    
    
    
    Stack<string> _scenesStack = new Stack<string>();

    public void Initialize()
    {
        
    }
    
    void Awake()
    {
        if ( s_instance != null )
        {
            Debug.LogError("ScenesManagement already exists");
            return;
        }
        
        
        s_instance = this;
        gameObject.transform.parent = null;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    internal void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddData(string dataName, object data)
    {
        Debug.Log($"Add Data {dataName}");

        _dataContainer.Add(dataName, data);
    }


    public void LoadScene(string sceneName , bool push = true)
    {
        Debug.Log($"Load scene {sceneName}");

        if ( push)
            _scenesStack.Push(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadPreviousScene()
    {
        string from = _scenesStack.Pop();
        Debug.Log($"Load scene {from}");

        UnityEngine.SceneManagement.SceneManager.LoadScene(from);
    }

    public bool HasPreviousScene()
    {
        return _scenesStack.Count > 0;
    }

    public void DeleteValues(string container = "")
    {
        _dataContainer.Remove(container);
    }


    public void SetValue(string dataName, int value)
    {
        if ( _dataContainer.TryGetValue(dataName , out object data) == false )
        {
            _dataContainer.Add(dataName, value);
        }
        else
        {
            _dataContainer[dataName] = value;

        }
    }

    public bool HasValue(string dataName)
    {
        return _dataContainer.ContainsKey(dataName);
    }
    
    public void SetValue(string dataName, float value)
    {
        if ( _dataContainer.TryGetValue(dataName , out object data) == false )
        {
            _dataContainer.Add(dataName, value);
        }
        else
        {
            _dataContainer[dataName] = value;
        }
    }
    
    public void SetValue(string dataName, object value)
    {
        if ( _dataContainer.TryGetValue(dataName , out object data) == false )
        {
            _dataContainer.Add(dataName, value);
        }
        else
        {
            _dataContainer[dataName] = value;

        }
    }
    
    public void SetValue(string containerName, string dataName, object value)
    {
        if ( _dataContainer.TryGetValue(containerName , out object data) == false )
        {
            var dictionary = new Dictionary<string, object> {{dataName, value}};

            _dataContainer.Add(containerName, dictionary);
        }
        else
        {
            if (data is Dictionary<string, object> dictionary) 
                dictionary[dataName] = value;
        }
    }

    public void SetValue(string dataName, string value)
    {
        if ( _dataContainer.TryGetValue(dataName , out object data) == false )
        {
            _dataContainer.Add(dataName, value);
        }
        else
        {
            _dataContainer[dataName] = value;

        }
    }
    
    public int GetIntValue(string containerName, string dataName )
    {
        if (_dataContainer.TryGetValue(containerName, out object dataDictionary) == false)
        {
            return -1;
        }

        var dictionary = dataDictionary as Dictionary<string, object>;
        if (dictionary!.TryGetValue(dataName, out object data) == false)
        {
            return -1;
        }
        
        return (int)data;
    }

    public int GetIntValue(string dataName)
    {
        if (_dataContainer.TryGetValue(dataName, out object data) == false)
        {
            return -1;
        }

        return (int)data;
    }
    
    public float GetFloatValue(string dataName)
    {
        if (_dataContainer.TryGetValue(dataName, out object data) == false)
        {
            return -1;
        }

        return (float)data;
    }

    public string GetStringValue(string dataName)
    {
        if (_dataContainer.TryGetValue(dataName, out object data) == false)
        {
            return String.Empty;
        }

        if (data == null)
            return String.Empty;

        return data.ToString();
    }
    
    public string GetStringValue(string containerName, string dataName )
    {
        if (_dataContainer.TryGetValue(containerName, out object dataDictionary) == false)
        {
            return String.Empty;
        }

        var dictionary = dataDictionary as Dictionary<string, object>;
        if (dictionary!.TryGetValue(dataName, out object data) == false)
        {
            return String.Empty;
        }
        
        return data.ToString();
    }

    public bool HasData(string paramName)
    {
        if (_dataContainer == null)
            return false;

        return _dataContainer.ContainsKey(paramName);
    }

    public T GetData<T>(string dataName) where T : class
    {
        if (_dataContainer.TryGetValue(dataName, out object data) == false)
        {
            return null;
        }

        return data as T; 
    }
    
    public object GetData(string dataName) 
    {
        if (_dataContainer.TryGetValue(dataName, out object data) == false)
        {
            return null;
        }

        return data;
    }



}

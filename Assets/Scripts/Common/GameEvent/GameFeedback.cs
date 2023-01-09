using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common.Tools;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameFeedback 
{

    public bool Enabled { get; set; }

    public GameFeedback()
    {
        Enabled = true;
    }

    protected T GetComponent<T>(GameEventInstance gameEvent) where T : Component
    {
        T component = gameEvent.GameObject.GetComponent<T>();
        if (component != null)
            return component;

        component = gameEvent.GameObject.GetComponentInChildren<T>();

        if (component == null)
            component = gameEvent.GameObject.GetComponentInChildren<T>(true);

        
        return component;
    }

    /// <summary>
    /// Returns true when the feedback is done
    /// </summary>
    /// <param name="gameEvent"></param>
    /// <returns></returns>
    public virtual bool Execute(GameEventInstance gameEvent )
    {
        return true;
    }

    protected string ShowVector3(Vector3 vector)
    {
        StringBuilder sb = new StringBuilder();
        if (Mathf.Abs(vector.x) > Mathf.Epsilon)
            sb.Append($"x:{vector.x}");
        if (Mathf.Abs(vector.y) > Mathf.Epsilon)
            sb.Append($"y:{vector.y}");
        if (Mathf.Abs(vector.z) > Mathf.Epsilon)
            sb.Append($"z:{vector.z}");

        return sb.ToString();
    }

}
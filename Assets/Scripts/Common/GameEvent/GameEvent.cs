using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
[Serializable]
public class GameEvent : ScriptableObject
{
    [TextArea]
    public string Comments;

    [SerializeReference]
    public List<GameFeedback> Feedbacks = new List<GameFeedback>();

  
}

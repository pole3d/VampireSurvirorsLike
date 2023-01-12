using Common.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the ai of an enemy's type 
/// </summary>
[CreateAssetMenu]
public class AIData : ScriptableObject
{
    [Serializable]
    public class AISequence
    {
        [SerializeReference]
        [Instantiable(typeof(GameFeedback))]
        public List<GameFeedback> Feedbacks = new List<GameFeedback>();
    }

    public List<AISequence> Sequences;

}
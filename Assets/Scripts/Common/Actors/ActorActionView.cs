using Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityCommon.Graphics.Actors
{
    /// <summary>
    /// View d'une actor action
    /// </summary>
    [Serializable]
    public class ActorActionView
    {
        public string Name;
        public string NextAction;

        [SerializeReference]
        [Instantiable(typeof(GameFeedback))]
        public List<GameFeedback> Feedbacks = new List<GameFeedback>();


        [HideInInspector]
        public float CurrentTimer;
    }

}
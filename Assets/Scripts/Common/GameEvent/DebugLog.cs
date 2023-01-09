using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DebugLog : GameFeedback
{
    public string Text = null;

    public override bool Execute(GameEventInstance gameEvent)
    {
        Debug.Log(Text);
        return true;
    }


}


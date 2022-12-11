using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Team { get; set; }      
    public float Life { get; set; }
    public float LifeMax { get; set; }

    public virtual void Hit(float damage)
    {
   
    }
}

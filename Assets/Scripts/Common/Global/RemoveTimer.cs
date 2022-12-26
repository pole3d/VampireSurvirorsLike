using UnityEngine;
using System.Collections;


namespace UnityCommon
{
    public class RemoveTimer : MonoBehaviour
    {
        public float Timer = 1;

        void Start()
        {
            GameObject.Destroy(gameObject, Timer);
        }


    }

}
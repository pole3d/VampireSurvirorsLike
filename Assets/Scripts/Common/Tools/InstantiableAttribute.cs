using System;
using UnityEngine;

namespace Common.Tools
{
    public class InstantiableAttribute : PropertyAttribute
    {
        public Type Type;

        public InstantiableAttribute(Type type)
        {
            Type = type;
        }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityCommonTools
{
    public class SimpleSingleton<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new T();

                return s_instance;
            }
        }

        static T s_instance;

       
    }
}

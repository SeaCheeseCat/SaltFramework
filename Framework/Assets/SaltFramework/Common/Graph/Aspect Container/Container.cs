using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace echo.common.container
{
    public interface IContainer
    {
        T AddAspect<T> (string key = null) where T : IAspect, new ();
        T AddAspect<T> (T aspect, string key = null) where T : IAspect;
        T GetAspect<T> (string key = null) where T : IAspect;
        bool RemoveAspect<T> (string key = null) where T : IAspect;
        ICollection<IAspect> Aspects ();
    }

    public class Container : IContainer
    {
        Dictionary<string, IAspect> aspects = new Dictionary<string, IAspect> ();
        List<IAspect> aspectList = new List<IAspect> ();

        public T AddAspect<T> (string key = null) where T : IAspect, new ()
        {
            return AddAspect<T> (new T (), key);
        }

        public T AddAspect<T> (T aspect, string key = null) where T : IAspect
        {
            key = key ?? typeof (T).Name;
            aspects.Add (key, aspect);
            aspect.Container = this;
            aspectList.Add (aspect);
            return aspect;
        }

        public T GetAspect<T> (string key = null) where T : IAspect
        {
            key = key ?? typeof (T).Name;
            T aspect = aspects.ContainsKey (key) ? (T) aspects[key] : default (T);
            return aspect;
        }

        public bool RemoveAspect<T> (string key = null) where T : IAspect
        {
            key = key ?? typeof (T).Name;
            if (aspects.ContainsKey (key))
            {
                aspectList.Remove (aspects[key]);
                aspects.Remove (key);
                return true;
            }
            return false;
        }

        public ICollection<IAspect> Aspects ()
        {
            return aspectList;
        }

    }
}
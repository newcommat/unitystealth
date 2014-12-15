using System;

using UnityEngine;

using System.Linq;
using System.Collections.Generic;



public static class GameObjectExtensions
{

    public static List<T> GetAllComponentsWhichImplementInterface<T>() where T : class
    {
        MonoBehaviour[] gameObjects = GameObject.FindObjectsOfType<MonoBehaviour>();

        List<T> list = new List<T>();

        foreach (MonoBehaviour behaviour in gameObjects)
        {
            T component = behaviour.GetComponent(typeof(T)) as T;

            if (component != null)
            {
                list.Add(component);
            }
        }

        return list;

    }
    /// <summary>

    /// Returns all monobehaviours (casted to T)

    /// </summary>

    /// <typeparam name="T">interface type</typeparam>

    /// <param name="gObj"></param>

    /// <returns></returns>

    public static T[] GetInterfaces<T>(this GameObject gObj)
    {

        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

        var mObjs = gObj.GetComponents<MonoBehaviour>();



        return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();

    }



    /// <summary>

    /// Returns the first monobehaviour that is of the interface type (casted to T)

    /// </summary>

    /// <typeparam name="T">Interface type</typeparam>

    /// <param name="gObj"></param>

    /// <returns></returns>

    public static T GetInterface<T>(this GameObject gObj)
    {

        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

        return gObj.GetInterfaces<T>().FirstOrDefault();

    }



    /// <summary>

    /// Returns the first instance of the monobehaviour that is of the interface type T (casted to T)

    /// </summary>

    /// <typeparam name="T"></typeparam>

    /// <param name="gObj"></param>

    /// <returns></returns>

    public static T GetInterfaceInChildren<T>(this GameObject gObj)
    {

        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

        return gObj.GetInterfacesInChildren<T>().FirstOrDefault();

    }



    /// <summary>

    /// Gets all monobehaviours in children that implement the interface of type T (casted to T)

    /// </summary>

    /// <typeparam name="T"></typeparam>

    /// <param name="gObj"></param>

    /// <returns></returns>

    public static T[] GetInterfacesInChildren<T>(this GameObject gObj)
    {

        if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");



        var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();



        return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();

    }

}
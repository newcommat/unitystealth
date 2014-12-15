using System;
using System.Collections.Generic;
using UnityEngine;

public interface Event
{ };

public delegate void Callback<T>(T arg1) where T : Event;

static internal class EventDispatcher
{
#pragma warning disable 0414
    //Ensures that the MessengerHelper will be created automatically upon start of the game.
    static private MessengerHelper messengerHelper = (new GameObject("MessengerHelper")).AddComponent<MessengerHelper>();
#pragma warning restore 0414

    static private Dictionary<System.Type, Delegate> eventTable = new Dictionary<System.Type, Delegate>();

    static public void RegisterCallback<T>(Callback<T> callback) where T : Event
    {
        if (!eventTable.ContainsKey(typeof(T)))
        {
            eventTable.Add(typeof(T), null);
        }

        eventTable[typeof(T)] = (Callback<T>)eventTable[typeof(T)] + callback;
    }

    static public void RemoveCallback<T>(Callback<T> callback) where T : Event
    {
        if (eventTable.ContainsKey(typeof(T)))
        {
            eventTable[typeof(T)] = (Callback<T>)eventTable[typeof(T)] - callback;
        }
    }

    static public void DispatchEvent<T>(T eventObject) where T: Event
    {
        Delegate d;

        if (eventTable.TryGetValue(typeof(T), out d))
        {
            Callback<T> callback = d as Callback<T>;

            if (callback != null)
            {
                callback(eventObject);
            }
            else
            {
                throw new System.Exception("Null callback being dispatched in event dispatcher!!");
            }
        }
    }

    static public void Clearup()
    {
        eventTable.Clear();
    }
}

//This manager will ensure that the messenger's eventTable will be cleaned up upon loading of a new level.
public sealed class MessengerHelper : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Clean up eventTable every time a new level loads.
    public void OnDisable()
    {
        EventDispatcher.Clearup();
    }
}
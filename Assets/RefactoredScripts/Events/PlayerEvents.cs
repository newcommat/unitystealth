using UnityEngine;
using System.Collections;

public class PlayerDiedEvent : Event { }

public class PlayerHealthUpdatedEvent : Event { 
    public float health;

    public PlayerHealthUpdatedEvent(float health)
    {
        this.health = health;
    }
}

public class PlayerPickupEvent : Event
{
    public GameObject pickupObject;

    public PlayerPickupEvent(GameObject pickupObject)
    {
        this.pickupObject = pickupObject;
    }
}

public class PlayerShoutedEvent : Event {}
using UnityEngine;
using System.Collections;

public class PlayerInventory : MonoBehaviour {
    public bool hasKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.pickup)
        {
            EventDispatcher.DispatchEvent<PlayerPickupEvent>(new PlayerPickupEvent(other.gameObject));
            hasKey = true;
            Destroy(other.gameObject);
        }
    }
}

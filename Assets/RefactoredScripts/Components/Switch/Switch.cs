using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour, IInteraction {

    public GameObject laser;
    public Material unlockedMat;

    private bool disabled = false;

    void IInteraction.Interact()
    {
        if (!disabled)
        {
            disabled = true;

            laser.SetActive(false);
            laser.GetComponent<LaserGridDetector>().SetEnabled(false);

            Renderer screen = GetComponent<Transform>().Find("prop_switchUnit_screen").GetComponent<Renderer>();
            screen.material = unlockedMat;
            GetComponent<AudioSource>().Play();
        }
    }
}

    using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

    public float onTime;
    public float offTime;

    private float timer;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	
        if (renderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        else if (!renderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
	}

    void SwitchBeam()
    {
        // Reset the timer
        timer = 0f;

        // Disable render of the model and the pointlight
        renderer.enabled = !renderer.enabled;
        light.enabled = !light.enabled;
    }
}

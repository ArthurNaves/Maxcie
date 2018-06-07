using UnityEngine;

public class LightPosts : MonoBehaviour {
    [SerializeField] Light[] lights;

    public void ToggleLights()
    {
        foreach (Light light in lights) light.enabled = !light.enabled;
    }
    public void TurnOnLights()
    {
        foreach (Light light in lights) light.enabled = true;
    }
}

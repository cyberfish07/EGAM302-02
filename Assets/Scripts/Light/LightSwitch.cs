using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light targetLight;
    public Transform standPoint;
    public bool isOn
    {
        get { return targetLight.enabled; }
    }

    public void Interact()
    {
        targetLight.enabled = !targetLight.enabled;
    }
}

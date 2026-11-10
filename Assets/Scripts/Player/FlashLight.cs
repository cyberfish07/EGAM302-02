using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLight : MonoBehaviour
{
    public GameObject flashlight;

    public bool isOn
    {
        get { return flashlight.activeSelf; }
    }

    void Start()
    {
        flashlight.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            flashlight.SetActive(!flashlight.activeSelf);
        }
    }
}
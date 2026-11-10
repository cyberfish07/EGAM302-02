using UnityEngine;

public class ZombiePerception : MonoBehaviour
{
    public LightSwitch[] lightSwitches;
    public FlashLight flashlight;
    public float sightRange = 10f;
    public float sightHalfAngle = 60f;
    public float flashlightHalfAngle = 18f;
    
    public LightSwitch SeenLight()
    {
        float cosLimit = Mathf.Cos(sightHalfAngle * Mathf.Deg2Rad);

        foreach (LightSwitch s in lightSwitches)
            {
                if (!s.isOn) continue;

                Vector3 toLamp = s.targetLight.transform.position - transform.position;

                if (toLamp.magnitude > sightRange) continue;

                float dot = Vector3.Dot(transform.forward, toLamp.normalized);
                if (dot >= cosLimit)
                {
                    return s;
                }
            }
        return null;
    }

    public bool Lit()
    {
        if (!flashlight.isOn) return false;
        Vector3 toZombie = (transform.position - flashlight.transform.position).normalized;
        float cosLimit = Mathf.Cos(flashlightHalfAngle * Mathf.Deg2Rad);

        return Vector3.Dot(flashlight.transform.forward, toZombie) >= cosLimit;
    }
}
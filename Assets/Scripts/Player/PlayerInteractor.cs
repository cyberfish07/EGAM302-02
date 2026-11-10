using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public float range = 2f;
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range);

            foreach (Collider hit in hits)
            {
                IInteractable target = hit.GetComponentInParent<IInteractable>();
                if (target != null)
                {
                    target.Interact();
                    break;
                }
            }
        }
    }
}

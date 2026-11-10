using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    public float speed = 5f;
    public float turnSpeed = 10f;
    public InputActionReference moveAction;
    Transform cam;

    CharacterController controller;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 forward = cam.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 dir = forward * input.y + right * input.x;

        controller.Move(dir * speed * Time.deltaTime);

        if (forward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}

using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    float cameraPitch = 0f;
    bool lockCursor = true;

    void LateUpdate()
    {
        if (lockCursor) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UpdateMouseMovement();
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            lockCursor = !lockCursor;
        }
    }

    void UpdateMouseMovement()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseInput.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        // Modifies camera angle, no transform.Rotate so that we don't rotate player upwards
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        // Modifies player rotation because camera is already a child
        transform.Rotate(Vector3.up * mouseInput.x);
    }

    void Stop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        this.enabled = false;
    }

    void OnEnable() 
    {
        Monster.OnCaughtPlayer += Stop;
    }

    void OnDisable()
    {
        Monster.OnCaughtPlayer -= Stop;
    }
}

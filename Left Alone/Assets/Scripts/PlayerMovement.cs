using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float moveSpeed;

    float horizontalInput;
    float verticalInput;
    

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 dir = transform.right * horizontalInput + transform.forward * verticalInput;
        characterController.Move(Vector3.down * Time.deltaTime * 9.81f);
        characterController.Move(dir * moveSpeed * Time.deltaTime);
    }

    void Stop()
    {
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
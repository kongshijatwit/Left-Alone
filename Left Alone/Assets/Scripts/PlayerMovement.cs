using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float moveSpeed;
    [SerializeField] Transform roomPoints;

    float horizontalInput;
    float verticalInput;
    

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        transform.position = roomPoints.GetChild(Random.Range(0, roomPoints.childCount)).position + new Vector3(0, .75f, 0);
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
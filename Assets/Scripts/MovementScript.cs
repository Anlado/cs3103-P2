using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Vector3 movementVector;
    Vector3 LookVector;


    Rigidbody rb;

    public float movementSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Movement
        Vector3 forward = transform.forward;
        forward.y = 0;
        
        Vector3 right = transform.right;
        right.y = 0;

        Vector3 positionChange = movementVector.y * forward + movementVector.x * right;
        transform.position += positionChange * movementSpeed * Time.deltaTime;

        //Camera controls
        transform.Rotate(new Vector3(0, LookVector.x, 0));

    }

    void OnLook(InputValue inputValue)
    {
        LookVector = inputValue.Get<Vector2>();
    }

    private void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
    }


    




}

using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Vector3 movementVector;
    Vector3 LookVector;

    Rigidbody rb;

    public float movementSpeed;
    float originalSpeed;
    private Coroutine speedBoostRoutine;

    void Awake()
    {
        originalSpeed = movementSpeed;
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

    public void ApplySpeedBoost(float duration)
    {
        if (speedBoostRoutine != null)
        {
            StopCoroutine(speedBoostRoutine);
        }

        speedBoostRoutine = StartCoroutine(SpeedBoost(duration));
    }

    System.Collections.IEnumerator SpeedBoost(float duration)
    {
        originalSpeed = movementSpeed;
        movementSpeed *= 2f; 

        yield return new WaitForSeconds(duration);

        movementSpeed = originalSpeed; 
        speedBoostRoutine = null;
    }

}

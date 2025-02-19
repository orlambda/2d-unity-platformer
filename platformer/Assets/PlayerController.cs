using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool canWalk = true;
    public bool canJump = true;
    public float walkSpeed;
    public float jumpSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (canWalk)
        {
            transform.Translate(Vector2.right * Time.deltaTime * walkSpeed * horizontalInput);
        }
        // Jumping currently relies on rigidbody physics to fall, otherwise would be flying
        if (canJump && (verticalInput > 0))
        {
            transform.Translate(Vector2.up * Time.deltaTime * jumpSpeed * verticalInput);
        }

    }
}

using Unity.Mathematics;
// using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public GameObject groundDetection;
    public GameObject body;
    public GameObject sprite;
    private SpriteRenderer bodySprite;
    public GameObject collisionTestBody;
    public GameObject collisionTestBodyGravityOnly;
    public GameObject collisionTestBodyUpwardsOnly;
    public GameObject collisionTestBodySidewaysOnly;
    public bool canWalk = true;
    public bool canJump = true;
    private bool jumping = false;
    private bool attemptingJump = false;
    public float walkSpeed;
    public float jumpSpeed;
    private ContactFilter2D ignorePlayerColliders;
    public LayerMask layersExceptPlayer;

    private Vector2 velocity;
    private Vector2 velocitySidewaysOnly;
    private Vector2 velocityUpwardsOnly;
    private Vector2 velocityGravityOnly;
    private Vector2 lastVelocity;

    public float gravityFactor = 5;

    private Collider2D bodyCollider;
    private Collider2D testBodyCollider;
    private Collider2D testBodyColliderSidewaysOnly;
    private Collider2D testBodyColliderGravityOnly;
    private Collider2D testBodyColliderUpwardsOnly;
    private Collider2D groundDetectionCollider;
    private int collisionCount;
    private int collisionCountSidewaysOnly;
    private int collisionCountGravityOnly;
    private int collisionCountUpwardsOnly;
    private int groundCollisionCount;

    private Collider2D[] collisions = new Collider2D[8];
    private Collider2D[] groundCollisions = new Collider2D[8];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        bodySprite = sprite.GetComponent<SpriteRenderer>();
        bodyCollider = body.GetComponent<Collider2D>();
        testBodyCollider = collisionTestBody.GetComponent<Collider2D>();
        testBodyColliderGravityOnly = collisionTestBodyGravityOnly.GetComponent<Collider2D>();
        testBodyColliderSidewaysOnly = collisionTestBodySidewaysOnly.GetComponent<Collider2D>();
        testBodyColliderUpwardsOnly = collisionTestBodyUpwardsOnly.GetComponent<Collider2D>();
        groundDetectionCollider = groundDetection.GetComponent<Collider2D>();
        velocity = new Vector2(0, 0);

        ignorePlayerColliders = new ContactFilter2D();
        ignorePlayerColliders.useLayerMask = true;
        ignorePlayerColliders.layerMask = layersExceptPlayer; // or = ~(1<<6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check collision test bodies
        collisionCount = testBodyCollider.Overlap(ignorePlayerColliders, collisions);
        collisionCountSidewaysOnly = testBodyColliderSidewaysOnly.Overlap(ignorePlayerColliders, collisions);
        collisionCountGravityOnly = testBodyColliderGravityOnly.Overlap(ignorePlayerColliders, collisions);
        collisionCountUpwardsOnly = testBodyColliderUpwardsOnly.Overlap(ignorePlayerColliders, collisions);

        // Reset collision test body positions
        collisionTestBody.transform.position = gameObject.transform.position;
        collisionTestBodyGravityOnly.transform.position = gameObject.transform.position;
        collisionTestBodySidewaysOnly.transform.position = gameObject.transform.position;
        collisionTestBodyUpwardsOnly.transform.position = gameObject.transform.position;

        // Move player based on collisions
        // Order of priority:
            // No collisions
            // Sideways only
            // Upwards only if jumping
            // Gravity only if not try trying to jump
        if (collisionCount == 0)
        {
            transform.Translate(velocity);
            // transform.position = new Vector2(0, 0); // this works!
        }
        else if (collisionCountSidewaysOnly == 0)
        {
            transform.Translate(velocitySidewaysOnly);
        }
        else if (jumping && collisionCountUpwardsOnly == 0)
        {
            transform.Translate(velocityUpwardsOnly);
        }
        else if (!attemptingJump && collisionCountGravityOnly == 0)
        {
            transform.Translate(velocityGravityOnly);
        }

        //---------------------------------------------------------------

        // lastVelocity = velocity;
        // Vector2 startingPosition = transform.position;

        velocity = new Vector2(0, 0);
        velocitySidewaysOnly = new Vector2(0, 0);
        velocityGravityOnly = new Vector2(0, 0);
        velocityUpwardsOnly = new Vector2(0, 0);

        
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (horizontalInput < 0) {
            bodySprite.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            bodySprite.flipX = false;
        }

        // Apply sideways movement
        if (canWalk)
        {
            velocity += (Vector2.right * Time.fixedDeltaTime * walkSpeed * horizontalInput);
            velocitySidewaysOnly += (Vector2.right * Time.fixedDeltaTime * walkSpeed * horizontalInput);
        }
        jumping = false;
        attemptingJump = false;
        // Apply upwards movement
        if (canJump && (verticalInput > 0))
        {
            velocity += (Vector2.up * Time.fixedDeltaTime * jumpSpeed * verticalInput);
            velocityUpwardsOnly += (Vector2.up * Time.fixedDeltaTime * jumpSpeed * verticalInput);
            jumping = true;
            attemptingJump = true;
        }
        // Apply gravity
        if (!checkIfGrounded())
        {
            velocity += (Vector2.down * Time.fixedDeltaTime * gravityFactor);
            velocityGravityOnly += (Vector2.down * Time.fixedDeltaTime * gravityFactor);
        }
        
        // Translate collisionTestBody
        collisionTestBody.transform.Translate(velocity);
        collisionTestBodyGravityOnly.transform.Translate(velocityGravityOnly);
        collisionTestBodyUpwardsOnly.transform.Translate(velocityUpwardsOnly);
        collisionTestBodySidewaysOnly.transform.Translate(velocitySidewaysOnly);
    }

    bool checkIfGrounded()
    {
        groundCollisionCount = groundDetectionCollider.Overlap(ignorePlayerColliders, groundCollisions);
        return (groundCollisionCount > 0);
    }
}

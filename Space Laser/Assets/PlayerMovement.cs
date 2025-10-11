using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    
    public GameObject bulletPrefab; // assign in Inspector
    public Transform firePoint;     // assign in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Lock Y axis and rotation
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Get left/right input (-1, 0, 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // Apply movement on X axis only
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

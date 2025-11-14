/*
PlayerMovement.cs (Server ONLY)

Takes state + input, outputs new state.
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 input) {
        rb.linearVelocity = input * speed;
    }
}
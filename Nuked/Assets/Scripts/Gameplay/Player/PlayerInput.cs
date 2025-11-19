/*
PlayerInput.cs (Client ONLY)

Reads WASD, mouse, shoot.

Sends commands.
*/

using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Vector2 moveInput { get; private set; }
    public Vector2 aimDirection { get; private set; }
    public bool shootPressed { get; private set; }

    public bool defusePressed { get; private set; }

    void Update() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePos - transform.position).normalized;

        shootPressed = Input.GetMouseButton(0);

        defusePressed = Input.GetKey(KeyCode.Space);
    }
}
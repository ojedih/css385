/*
PlayerInput.cs (Client ONLY)

Reads WASD, mouse, shoot.

Sends commands.
*/

using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Vector2 MoveInput { get; private set; }
    public Vector2 AimDirection { get; private set; }
    public bool ShootPressed { get; private set; }

    void Update() {
        MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        AimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        ShootPressed = Input.GetMouseButton(0);
    }
}
/*
PlayerMovement.cs (Server ONLY)

Takes state + input, outputs new state.
*/

using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;

    public void Move(PlayerState state, Vector2 input) {
        state.position += input.normalized * speed * Time.fixedDeltaTime;
    }
}
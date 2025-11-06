/*
PlayerState.cs (Server + Client)

Holds the truth: position, rotation, HP, etc.
*/

using UnityEngine;

public class PlayerState : MonoBehaviour {
    public Vector2 position;
    public Vector2 aimDirection;
    public int health;
}
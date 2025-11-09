/*
PlayerRender.cs (Client ONLY)

This is the cosmetics script.
It reads PlayerState and updates the sprite/animation.
*/

using UnityEngine;

public class PlayerRender : MonoBehaviour {
    public PlayerState state;
    
    void Update() {
        transform.right = state.aimDirection;
    }
}
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject winTextObject; // Drag your TMP text here in the Inspector

    private void Start()
    {
        if (winTextObject != null)
            winTextObject.SetActive(false); // Hide at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            if (winTextObject != null)
                winTextObject.SetActive(true); // Show when player touches goal

            Time.timeScale = 0f;
        }
    }
}

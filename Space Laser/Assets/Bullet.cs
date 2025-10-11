using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            other.gameObject.SetActive(false); // hide target
            Destroy(gameObject); // destroy bullet
        } else if(other.CompareTag("Despawn")) {
            Destroy(gameObject);
        }
    }
}

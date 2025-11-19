using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    LineRenderer lr;
    float life = 0.05f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Show(Vector2 start, Vector2 end)
    {
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        Destroy(gameObject, life);
    }
}


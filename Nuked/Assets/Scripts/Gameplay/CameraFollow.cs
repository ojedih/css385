using UnityEngine;
using Mirror;

public class CameraFollow : MonoBehaviour
{
    public float smooth = 10f;
    Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 pos = target.position;
        pos.z = -10f; // camera distance
        transform.position = Vector3.Lerp(transform.position, pos, smooth * Time.deltaTime);
    }
}


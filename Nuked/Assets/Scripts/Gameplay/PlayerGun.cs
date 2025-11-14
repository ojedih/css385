using UnityEngine;
using Mirror;

public class PlayerGun : NetworkBehaviour
{
    public float fireRate = 0.15f;
    public float bulletDistance = 20f;
    public int damage = 10;
    float lastFire;
    public GameObject tracerPrefab;

    PlayerState state;

    void Awake()
    {
        state = GetComponent<PlayerState>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButton(0) && Time.time > lastFire + fireRate)
        {
            lastFire = Time.time;
            CmdShoot(state.aimDirection);
        }
    }

    [Command]
    void CmdShoot(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, bulletDistance);

        if (hit.collider && hit.collider != GetComponent<Collider2D>())
        {
            PlayerState target = hit.collider.GetComponent<PlayerState>();
            if (target != null)
            {
                Debug.Log("Found target");
                target.hp -= damage;
            }
        }

        RpcShootEffect(transform.position, dir);
    }

    [ClientRpc]
    void RpcShootEffect(Vector2 origin, Vector2 dir)
    {
        Vector2 end = origin + dir * bulletDistance;

        GameObject tracer = Instantiate(tracerPrefab);
        tracer.GetComponent<BulletTracer>().Show(origin, end);
    }
}


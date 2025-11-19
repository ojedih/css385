using UnityEngine;
using Mirror;

public class PlayerGun : NetworkBehaviour
{
    public GameObject tracerPrefab;
    public AudioSource gunAudio;
    public PlayerState state;

    public float fireRate = 0.15f;
    public float bulletDistance = 100f;
    public int damage = 10;
    float lastFire;
    
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButton(0) && Time.time > lastFire + fireRate)
        {
            lastFire = Time.time;
            gunAudio.Play();
            CmdShoot(state.aimDirection);
        }
    }

    [Command]
    void CmdShoot(Vector2 dir)
    {
        Vector2 gunOffset = transform.right * 0.6f;
        Vector2 shootOrigin = (Vector2)transform.position + gunOffset;
        
        RaycastHit2D hit = Physics2D.Raycast(shootOrigin, dir, bulletDistance);

        if (hit.collider && hit.collider != GetComponent<Collider2D>())
        {
            PlayerState target = hit.collider.GetComponent<PlayerState>();
            if (target != null)
            {
                Debug.Log("Found target");
                target.hp -= damage;
            }
        }

        RpcShootEffect(shootOrigin, dir);
    }

    [ClientRpc]
    void RpcShootEffect(Vector2 origin, Vector2 direction)
    {
        direction = direction.normalized; // Ensure unit vector
        Vector2 end = origin + direction * bulletDistance;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, bulletDistance);

        if(hit.collider)
            end = hit.point; 

        var t = Instantiate(tracerPrefab, origin, Quaternion.identity);
        t.GetComponent<BulletTracer>().Show(origin, end);
    }
}


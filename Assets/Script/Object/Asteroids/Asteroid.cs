using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float damageAmount = 2f;

    [SerializeField]
    ParticleSystem ps;

    [SerializeField]
    bool destroyOnCollision = false;

    void Start()
    {
        ps.Pause();
        var em = ps.emission;
        em.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damage = collision.gameObject.GetComponent<Damageable>();
        if (damage != null)
        {
            damage.Damage(damageAmount, "Asteroid");
            var em = ps.emission;

            em.enabled = true;
            ps.Play();
            if (destroyOnCollision)
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(gameObject, ps.main.duration);
            }
        }
    }
}

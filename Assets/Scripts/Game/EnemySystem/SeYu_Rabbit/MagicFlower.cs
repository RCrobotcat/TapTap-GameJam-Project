using UnityEngine;

public class MagicFlower : MonoBehaviour
{
    public float AttackRange;    // Attack range
    public float attackDamage;   // Attack damage
    public float attackGap;      // Attack interval
    private float timer;         // Timer

    public float existTime;      // Existence time

    void Start()
    {
        timer = attackGap;
        Destroy(gameObject, existTime);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        AttackPlayer();
    }

    public void AttackPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (timer <= 0)
                {
                    PlayerController.Instance.TakeDamage(attackDamage);
                    timer = attackGap;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}

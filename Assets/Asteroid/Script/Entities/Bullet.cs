using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private string targetTag;

    public void SetBullet(float _damage, string _targetTag, float _speed = 10)
    {
        this.damage= _damage;
        this.speed= _speed;
        this.targetTag= _targetTag;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Damage(IDamageable damageable)
    {
        if (damageable != null)
        {
            damageable.GetDamage(damage);

            // Using a singleton
            GameManager.GetInstance().scoreManager.IncrementScore();

            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);

        // Check the target
        if (!other.gameObject.CompareTag(targetTag))
            return;

        // Using interfaces
        IDamageable damageable = other.GetComponent<IDamageable>();
        Damage(damageable);
    }
}

using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField] private float _maxhealth = 3f;
    [SerializeField] private float _damagethreshold = 0.2f;
    [SerializeField] private GameObject _baddiedeatheffect;
    private float _currenthealth;
    private void Awake()
    {
        _currenthealth = _maxhealth;
    }
    public void DamageBaddie(float damageamount)
    {
        _currenthealth -= damageamount;
        if (_currenthealth <= 0f)
        {
            Die();
        }

    }
    private void Die()
    {
        GameManager.instance.RemoveBadddie(this);
        Instantiate(_baddiedeatheffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;
        if (impactVelocity > _damagethreshold)
        {
            DamageBaddie(impactVelocity);
        }
    }
}

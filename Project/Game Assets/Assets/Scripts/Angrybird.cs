using System.Collections.Generic;
using UnityEngine;

public class Angrybird : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _circlecollider;
    private bool _hasBeenLaunched;
    private bool _shouldFaceVelocityDirection;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circlecollider = GetComponent<CircleCollider2D>();
        _rb.isKinematic = true;
        _circlecollider.enabled = false;
    }

    private void Start()
    {
        _rb.isKinematic = true;
        _circlecollider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFaceVelocityDirection)
        {
            transform.right = _rb.linearVelocity;
        }
    }
    public void LaunchBird(Vector2 direction, float force)
    {
        _rb.isKinematic = false;
        _circlecollider.enabled = true;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
        _hasBeenLaunched = true;
        _shouldFaceVelocityDirection = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVelocityDirection = false;

    }
}

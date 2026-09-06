using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy2 : EnemyTestSuperior
{
    private int _jumpChance;
    
    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        
        FirstMovement();
        StartCoroutine(Jump());
    }
    
    private IEnumerator Jump()
    {
        while (true)
        {
            _jumpChance = Random.Range(0, 3);

            if (_jumpChance >= 2)
                _rb.AddForce(_jumpStrength * Vector2.up);
            
            _jumpChance = Random.Range(0, 3);
            
            if (_jumpChance == 3)
                _rb.AddForce(_jumpStrength * Vector2.up);
            
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void Update()
    {
        Damage();
        Animation();
        //Flip();
        
        _rb.linearVelocityX = _speed;
    }
}

using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : EnemyTestSuperior
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

            if (transform.position.x - target.position.x < 3 && transform.position.x - target.position.x > -3 &&
                transform.position.y - target.position.y < 0 && transform.position.y - target.position.y > -5)
            {
                _rb.AddForce(_jumpStrength * Vector2.up);
                //Debug.Log(transform.position.y - target.position.y);
            }
            
            if (!(transform.position.x - target.position.x < 3 && transform.position.x - target.position.x > -3 &&
                transform.position.y - target.position.y < 5 && transform.position.y - target.position.y > 0))
            {
                //Debug.Log("caca");
                _jumpChance = Random.Range(0, 3);
                
                if (_jumpChance >= 2)
                    _rb.AddForce(_jumpStrength * Vector2.up);
            
                _jumpChance = Random.Range(0, 3);
            
                if (_jumpChance == 3)
                    _rb.AddForce(_jumpStrength * Vector2.up);
            }
            
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void Update()
    {
        Damage();
        Animation();
        //Flip();
        
        _rb.linearVelocityX = _speed;
        if (_rb.linearVelocity.y > 5)
            _rb.linearVelocityY = 5;
    }
}
using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private bool facingRight = true;
    
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _Jumpstrength;
    private Vector2 _moveVector;
    private bool SpacePressed;
    private bool JumpRoutine;
    private bool activeJumpTimer;
    private float jumpTimer;
    
    
    private Collider2D aa;
    [SerializeField] private ParticleSystem _flipParticles;
    [SerializeField] private ParticleSystem _bonkParticles;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _bonkAudio;
    [SerializeField] private AudioClip _runAudio;
    [SerializeField] private AudioClip _flyAudio;

    [SerializeField] private Settings _echapSettings;
    
    private bool _grounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!JumpRoutine && jumpTimer >= 0.1f)
        {
            //Debug.Log("Jump");
            StartCoroutine(Jump());
            JumpRoutine = true;
        }
        
        //Debug.Log(SpacePressed);
        Run();
        Animator();
        Flip();
    }

    private void Update()
    {
     if (activeJumpTimer == true)   
        jumpTimer += Time.deltaTime;
     
     //Debug.Log(_rb.linearVelocity.y);
     if (_rb.linearVelocity.y > 5)
         _rb.linearVelocityY = 5;
    }

    private IEnumerator Jump()
    {
        while (SpacePressed)
        {
            if (SpacePressed == false)
                break;
            
            yield return new WaitForSeconds(0.15f);
            
            if (SpacePressed == false)
                break;
                
            _source.PlayOneShot(_flyAudio, 0.25f);
            _rb.AddForce(_Jumpstrength * Vector2.up);
            //Debug.Log("Jump");
        }
    }

    private void Run()
    {
        var targetSpeed = _moveVector.x * _maxSpeed;
        var accelRate = Mathf.Abs(targetSpeed) > 0.1 ?  _acceleration : _deceleration;
        var speedDiff = targetSpeed - _rb.linearVelocity.x;
        var movement = speedDiff * accelRate;
        
        _rb.AddForce(movement * Vector2.right);
    }

    private void Animator()
    {
        //if (_moveVector.x > 0 && _rb.velocity.x < 0 || _moveVector.x < 0 && _rb.velocity.x > 0 )
        //Debug.Log("çaralentiici");
        _animator.SetBool("Playing", _moveVector.x != 0);
        _animator.SetBool("SlowDown", _moveVector.x > 0 && _rb.linearVelocity.x < 0 || _moveVector.x < 0 && _rb.linearVelocity.x > 0 ? true : false);
        _animator.SetFloat("Moving", Mathf.Abs(_rb.linearVelocity.x));
        _animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x * 0.1f));
        _animator.SetBool("Flying", !IsGrounded());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MainMap")
        {
            _source.PlayOneShot(_bonkAudio);
            _bonkParticles.Play();
        }
    }

    private void OnMove(InputValue value)
    {
        _moveVector = value.Get<Vector2>();
        _moveVector.Normalize();
    }

    private void OnJump(InputValue value)
    {
        SpacePressed = value.isPressed;

        if (SpacePressed == true)
        {
            activeJumpTimer = true;
        }
        else
        { 
            activeJumpTimer = false;
            jumpTimer = 0;
        }
           
        
        if (SpacePressed == true)
        {
            _animator.SetTrigger("Fly");
            _rb.AddForce(_Jumpstrength * Vector2.up);
            _source.PlayOneShot(_flyAudio, 0.5f);
        }
        else
        {
            StopCoroutine(Jump());
            JumpRoutine = false;
        }
    }

    private void OnEchap(InputValue value)
    {
        if (_echapSettings._isPlaying == false)
        {
            if (_echapSettings._isActive == false)
            {
                Time.timeScale = 0;
                _echapSettings.Menu();
            }
            
            else
            {
                Time.timeScale = 1;
                _echapSettings.CloseMenu();
            }
        }
    }

    private void Flip()
    {
        if  (facingRight && _moveVector.x < 0 || !facingRight && _moveVector.x > 0)
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            facingRight = !facingRight;

            if (IsGrounded())
            {
               if (!_flipParticles.isPlaying)
               {
                   var main = _flipParticles.main;
                   main.maxParticles =  Mathf.RoundToInt(Mathf.Abs(_rb.linearVelocity.x*2));
                   var duration = _rb.linearVelocity.x * 0.04f;
                   main.duration =  (Mathf.Abs(duration));
               }
               
               var fliplocalScale = _flipParticles.transform.localScale;
               fliplocalScale.x *= -1;
               _flipParticles.transform.localScale = fliplocalScale;
                           
               _flipParticles.Play(); 
            }
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.35f, _groundLayer); 
    }
}

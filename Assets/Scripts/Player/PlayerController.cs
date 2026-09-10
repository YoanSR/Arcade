using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private bool _facingRight = true;
    
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _Jumpstrength;
    private Vector2 _moveVector;
    
    private bool _grounded;
    
    private bool _spacePressed;
    private bool _jumpRoutine;
    private bool _activeJumpTimer;
    private float _jumpTimer;
    
    [SerializeField] private ParticleSystem _flipParticles;
    [SerializeField] private ParticleSystem _bonkParticles;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _bonkAudio;
    [SerializeField] private AudioClip _runAudio;
    [SerializeField] private AudioClip _flyAudio;

    [SerializeField] private Settings _echapSettings;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_jumpRoutine && _jumpTimer >= 0.1f)
        {
            //Debug.Log("Jump");
            StartCoroutine(Jump());
            _jumpRoutine = true;
        }
        
        //Debug.Log(SpacePressed);
        Run();
        Animator();
        Flip();
    }

    private void Update()
    {
     if (_activeJumpTimer == true)   
        _jumpTimer += Time.deltaTime;
     
     //Debug.Log(_rb.linearVelocity.y);
     if (_rb.linearVelocity.y > 5)
         _rb.linearVelocityY = 5;
    }

    private IEnumerator Jump()
    {
        while (_spacePressed)
        {
            if (_spacePressed == false)
                break;
            
            yield return new WaitForSeconds(0.15f);
            
            if (_spacePressed == false)
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
        _spacePressed = value.isPressed;

        if (_spacePressed == true)
        {
            _activeJumpTimer = true;
        }
        else
        { 
            _activeJumpTimer = false;
            _jumpTimer = 0;
        }
           
        
        if (_spacePressed == true)
        {
            _animator.SetTrigger("Fly");
            _rb.AddForce(_Jumpstrength * Vector2.up);
            _source.PlayOneShot(_flyAudio, 0.5f);
        }
        else
        {
            StopCoroutine(Jump());
            _jumpRoutine = false;
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
        if  (_facingRight && _moveVector.x < 0 || !_facingRight && _moveVector.x > 0)
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            _facingRight = !_facingRight;

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
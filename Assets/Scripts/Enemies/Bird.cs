using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private Sequence _seq;
    
    private bool _facingRight = true;
    
    [SerializeField] private Transform _playerPos;
    [SerializeField] private float _speed;
    [SerializeField] private float _dashStrength;
    
    [SerializeField] private AudioClip _deathSound;
    private bool _soundPlayed;

    [SerializeField] private GameObject _scoreGameObject;
    [SerializeField] private int _scoreNumberAdd;
    private int _scoreAdded;
    public bool Death;
    
    private bool _isDashing;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        
        _seq = DOTween.Sequence();
        
        _playerPos = GameObject.Find("Player").transform;
    }
    
    private void Update()
    {
        if (Death)
            StartCoroutine(MobDeath());
        
        if (!_isDashing) 
            _rb.linearVelocityX = _speed;

        var aa = transform.position.x - _playerPos.localPosition.x;
        if ((aa > -3 && aa < -1 && transform.localScale.x > 0  ||  aa < 3 && aa > 1 &&  transform.localScale.x < 0) && _isDashing == false)
        {
            Debug.Log(aa);
            StartCoroutine(Dash());
        }

        var bb = transform.position.y - _playerPos.position.y;
        if (!_isDashing)
        {
            //Debug.Log("player" + _playerPos.position.y);
            //Debug.Log("distance" + bb);
            if (bb < 0 || bb > 0.25)
            {
                
                if (bb < 0)
                    _rb.linearVelocityY = 2;
                if (bb > 0.25)
                    _rb.linearVelocityY = -2;
            }
            else
                _rb.linearVelocityY = 0;
        }
        else
        {
            if (bb < 0 || bb > 0.25)
            {
                if (bb < 0.25) 
                    _rb.linearVelocityY = 1;
                if (bb > 0.5)
                    _rb.linearVelocityY = -1;
            }
            else
                _rb.linearVelocityY = 0;
        }
        
        Flip();
        Animation();
    }
    

    private IEnumerator Dash()
    {
        _isDashing = true;
        
        _seq.Append(transform.DOMoveX(transform.position.x - 1, 0.5f).SetEase(Ease.Linear));
        yield return new WaitForSeconds(0.5f);
        
        Debug.Log("dash");
        _rb.AddForceX(_dashStrength * transform.localScale.x, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        
        _isDashing = false;
    }

    public IEnumerator MobDeath()
    {
        
        while (_scoreAdded < _scoreNumberAdd)
        {
            _scoreGameObject = GameObject.Find("Score");
            _scoreAdded++;
            _scoreGameObject.GetComponent<Score>().ScoreNumber++;
            yield return new WaitForSeconds(0.1f);
        }

        if (_scoreAdded == _scoreNumberAdd)
        {
            if (_soundPlayed == false)
            {
                _soundPlayed = true;
                var _audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
                _audioSource.PlayOneShot(_deathSound);
            }

            Destroy(gameObject);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private void Animation()
    {
        _animator.SetBool("isDashing", _isDashing);
    }

    private void Flip()
    {
        if  (_facingRight && _rb.linearVelocityX < 0 || !_facingRight && _rb.linearVelocityX > 0)
        {
            _speed =  -_speed;
            
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            _facingRight = !_facingRight;
            
            Debug.Log("ici");
        }
    }
}

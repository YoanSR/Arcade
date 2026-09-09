using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTestSuperior : MonoBehaviour
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public BoxCollider2D _collider;
    [HideInInspector] public Rigidbody2D _rb;
    [HideInInspector] public SpriteRenderer _sr;
    
    public Transform target;
    
    [SerializeField] private Transform _side;
    [SerializeField] private Vector2 _sideRadius;
    
    [SerializeField] private LayerMask _otherLayer;
    
    public float _speed;
    public float _jumpStrength;
    
    public bool Death;
    private float _deathTimer;
    
    private bool _facingRight = true;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius;
    [SerializeField] private LayerMask _groundLayer;
    
    [SerializeField] private AudioClip _deathSound;
    private bool _soundPlayed;
    
    [SerializeField] private GameObject Egg;
    private bool _eggInvoked;
    
    private GameObject _scoreGameObject;
    [SerializeField] private int _scoreNumberAdd;
    private int scoreAdded = 0;

    public void FirstMovement()
    {
        int _firstMovement = 9;
        _firstMovement = Random.Range(1,2);
        
        Debug.Log(_firstMovement);
        
        if (_firstMovement == 1)
            _rb.linearVelocityX  = _speed * -1;
    }

    public void Damage()
    {
        Collider2D[] Damaged = Physics2D.OverlapBoxAll(_side.position, _sideRadius, 0,_otherLayer);
        for (int i = 0; i < Damaged.Length; i++)
            if (Damaged[i].GetComponent<Collider2D>() != _collider && Damaged[i].tag != "Monster" && Damaged[i].tag != "Water")
            {
                _speed *= -1f ;
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
                _facingRight = !_facingRight;
            }


        if (Death)
        {
            //Debug.Log("Mob Death");
            _sr.color = new Color(0.75f, 0.75f, 0.75f);
            _rb.simulated = false;
            _collider.enabled = false;
            _animator.SetBool("Death", true);
            _scoreGameObject = GameObject.Find("Score");

            if (_soundPlayed == false)
            {
                _soundPlayed = true;
               var _audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
                _audioSource.PlayOneShot(_deathSound); 
            }
            
            
            StartCoroutine(MobDeath());
        }
            
    }

    public IEnumerator MobDeath()
    {
        while (scoreAdded < _scoreNumberAdd)
        {
            scoreAdded++;
            _scoreGameObject.GetComponent<Score>()._scoreNumber++;
            yield return new WaitForSeconds(0.1f);
        }
        
        if (scoreAdded == _scoreNumberAdd && _eggInvoked == false)
        {
            _eggInvoked = true;
            //Instantiate(Egg, transform.position, transform.rotation);
            //var a = transform.parent;
            var eggSpawn = Instantiate(Egg, transform.parent);
            eggSpawn.transform.position = transform.position;
            Destroy(gameObject); 
            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(0.1f);
    }

    public void Animation()
    {
        _animator.SetBool("Mooving", _rb.linearVelocityX != 0 && IsGrounded() && !Death);
        _animator.SetBool("Sliding", !IsGrounded() && !Death);
        _animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x * 0.5f));
    }
    
    
    /*public void Flip()
    {
        if  (_facingRight && _rb.linearVelocityX < 0 || !_facingRight && _rb.linearVelocityX > 0)
        {
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            _facingRight = !_facingRight;
        }
    }*/
    
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer); 
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_side.position, _sideRadius);
    }
}

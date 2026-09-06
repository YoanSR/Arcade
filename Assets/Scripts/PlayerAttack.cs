using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPos;
    [SerializeField] private Vector2 _attackRadius;
    [SerializeField] private Transform _damagedPos;
    [SerializeField] private Vector2 _damagedRadius;
    [SerializeField] private LayerMask _EnemyLayer;
    
    public int _deathNumber;
    
    private Rigidbody2D _rb;
    [SerializeField] private float _kbStrength;
    [SerializeField] private ParticleSystem _killEffect;
    [SerializeField] private AudioClip _deathSound;

    [SerializeField] private RectTransform _healthImage;

    private Sequence _seq;

    public bool IsDying;
    [SerializeField] private Image _BG;
    [SerializeField] private GameObject _deathMenu;

    private void Awake()
    {
        _rb  = GetComponent<Rigidbody2D>();
        
        _seq = DOTween.Sequence();
    }

    private IEnumerator Death()
    {
        gameObject.GetComponent<Animator>().SetBool("Death", true);
        
        var deathsound = GameObject.Find("SFX").GetComponent<AudioSource>();
        deathsound.PlayOneShot(_deathSound);
        
        IsDying = true;
        Debug.Log("death");
        _deathNumber++ ;
        
        _rb.isKinematic = true;
        _rb.gravityScale = 0;
        _rb.linearVelocity = Vector2.zero;
        
        
        yield return new WaitForSeconds(1f);
        
        if (_deathNumber >= 4)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.isKinematic = true;
            gameObject.GetComponent<Light2D>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            _BG.DOFade(0.4f, 0.5f);
            _deathMenu.SetActive(true);
            _seq.Append(_deathMenu.transform.DOScaleY(1, 0.5f)).SetEase(Ease.InCubic);
            _seq.Append(_deathMenu.transform.DOScaleX(1, 0.25f)).SetEase(Ease.InCubic);

            if (GameObject.Find("Score").GetComponent<Score>()._scoreNumber < PlayerPrefs.GetInt("BestScore")); 
            PlayerPrefs.SetInt("BestScore", GameObject.Find("Score").GetComponent<Score>()._scoreNumber);
            Debug.Log(PlayerPrefs.GetInt("BestScore"));
          
            yield break;
        }
        
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool("Death", false);
        gameObject.GetComponent<Animator>().SetBool("Drowning", false);
        gameObject.GetComponent<Animator>().SetBool("Respawning", true);
        _seq.Append(_rb.DOMove(new Vector3(0, -3.35f, 0), 1.5f)).SetEase(Ease.InCubic);
        
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().SetBool("Respawning", false);
        gameObject.GetComponent<Collider2D>().enabled = true;
        _rb.gravityScale = 1;
        _rb.isKinematic = false;
        IsDying = false;
    }

    private void Update()
    {
        //lifebar
        var Health = 4 - _deathNumber;
        _healthImage.sizeDelta = new Vector2(Health * 100, 100);
        //Debug.Log( "Health"+ Health);
        
        Collider2D[] Damaged =  Physics2D.OverlapBoxAll(_damagedPos.position, _damagedRadius, 0 , _EnemyLayer);
        for (int i = 0; i < Damaged.Length; i++)
        {
            //Debug.Log(Damaged[i].transform.name);
            if (IsDying == false)
            {
                if (Damaged[i].tag == "TrueWater")
                    gameObject.GetComponent<Animator>().SetBool("Drowning", true);
                
                StartCoroutine(Death());
            }
            
        }
        
        Collider2D[] Damage =  Physics2D.OverlapBoxAll(_attackPos.position, _attackRadius, 0 , _EnemyLayer);
        for (int i = 0; i < Damage.Length; i++)
        {
            if (IsDying == false)
            {
               if (Damage[i].GetComponent<EnemyTestSuperior>() != null)
               { 
                   _killEffect.transform.position = _attackPos.position;
                   _killEffect.Play();
                   _rb.linearVelocity = Vector2.zero;
                   _rb.AddForce(_kbStrength * Vector2.up);
                   Debug.Log(Damage[i].transform.name);
                                           
                   Damage[i].GetComponent<EnemyTestSuperior>().Death = true;
               } 
               if (Damage[i].GetComponent<Pterodactyl>() != null)
               {
                   _killEffect.transform.position = _attackPos.position;
                   _killEffect.Play();
                   _rb.linearVelocity = Vector2.zero;
                   _rb.AddForce(_kbStrength * Vector2.up);
                   Debug.Log(Damage[i].transform.name);
                                           
                   Damage[i].GetComponent<Pterodactyl>()._death = true;
               }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPos.position, _attackRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_damagedPos.position, _damagedRadius);
    }
}

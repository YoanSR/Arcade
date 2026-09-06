using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private Rigidbody2D _rb;
    [SerializeField] private Transform _side;
    [SerializeField] private Vector2 _sideRadius;
    [SerializeField] private LayerMask _otherLayer;
    [SerializeField] private int _speed;

    [SerializeField] private Monster2 _monster2;

    public GameObject _invocator;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Grab());

        //_invocator = _monster2._otherGameObject;

        //StartCoroutine(Movement());
    }

    private void Update()
    {
        
        /*Debug.Log(_monster2._otherGameObject);
        if (_monster2._otherGameObject != null)
        {
            var bb = transform.position.x;
            bb = _monster2._otherGameObject.position.x;
            
            var Target = _monster2._otherGameObject.position;
            transform.position = Target - new Vector3(0, 2, 0);
        }*/
        transform.position = _invocator.transform.position;
        //_rb.velocity = Vector2.zero;
        
        //transform.position = _invocator.transform.position - new Vector3(0, 2, 0);
    }

    private IEnumerator Grab()
    {
        yield return new WaitForSeconds(0.6f);
        while (true && _invocator != null)
        {
            //Debug.Log("Grabbed");
            //_rb.linearVelocityY = -3;
            //_invocator.GetComponent<Rigidbody2D>().linearVelocityY =-3;
            
            _rb.AddForceY(-100);
            if (_invocator.tag == "Player")
                _invocator.GetComponent<Rigidbody2D>().AddForceY(-100);
            else
                _invocator.GetComponent<Rigidbody2D>().AddForceY(-25);
            
            yield return new WaitForSeconds(0.15f);
        }
    }
    
    public IEnumerator DeathMonster()
    {
        
        gameObject.GetComponent<Animator>().SetBool("Death", true);
        transform.Find("FrontSquid").gameObject.GetComponent<Animator>().SetBool("Death", true);
        yield return new WaitForSeconds(0.6f);
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    /*void Update()
    {
        Collider2D[] Damaged = Physics2D.OverlapBoxAll(_side.position, _sideRadius, 0,_otherLayer);
        for (int i = 0; i < Damaged.Length; i++)
            //if (Damaged[i].GetComponent<Collider2D>() != _collider)
            //{
        Debug.Log("oecaca");
            _speed *= -1f ;
            //}
    }*/
    /*IEnumerator Movement()
    {
             
        while (true)
        { 
            Collider2D[] Damaged = Physics2D.OverlapBoxAll(_side.position, _sideRadius, 0,_otherLayer);   
            for (int i = 0; i < Damaged.Length; i++)
            {
                Debug.Log("oecaca"); 
                _speed *= -1 ;
                _rb.linearVelocity = _speed * Vector2.right;
                
                yield return new WaitForSeconds(0.5f);
            }


            for (int i = Damaged.Length; i >= 0; i--)
            {
                _speed = Random.Range(-3,3);
                if (_speed != 0)
                    _rb.linearVelocity = _speed * Vector2.right;
                        
                        
                
                Debug.Log(_speed);
                
                            yield return new WaitForSeconds(0.5f);
            }
            
            
                //if (Damaged[i].GetComponent<Collider2D>() != _collider)
                //{
            
            
                yield return new WaitForSeconds(0.1f);
        }
    }*/
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_invocator != null)
            StartCoroutine(DeathMonster());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_side.position, _sideRadius);
    }
}

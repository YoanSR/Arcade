using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private float _eggTimer;
    
    [SerializeField] private LayerMask _playerMask;
        
    private GameObject _scoreGameObject;
    
    [SerializeField] private int _scoreNumberAdd;
    private int scoreAdded;
    private bool coroutine;
    [SerializeField] private GameObject _RedPenguin;
    
    private bool oneTime;

    private void Start()
    {
        StartCoroutine(Break());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        { 
            gameObject.GetComponent<Collider2D>().enabled = false; 
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            Debug.Log("add points");
                 
            _scoreGameObject = GameObject.Find("Score");
                 
            _scoreGameObject.GetComponent<Score>()._scoreNumber += _scoreNumberAdd;
            //StartCoroutine(Points());   
            Destroy(gameObject);
        }
        
    }

    private IEnumerator Points()
    {
        
        /*while (scoreAdded < _scoreNumberAdd)
        {
            Debug.Log(scoreAdded);
            Debug.Log(_scoreNumberAdd);
            scoreAdded++;
            _scoreGameObject.GetComponent<Score>()._scoreNumber++;
            yield return new WaitForSeconds(0.1f);
        }*/

        yield return new WaitForSeconds(1);
    }
    

    private IEnumerator Break()
    {
        yield return new WaitForSeconds(15);
        Debug.Log("oelateamoe");
        Instantiate(_RedPenguin, transform.position, transform.rotation);
        Destroy(gameObject);
        yield return new WaitForSeconds(15);
        
    }
}

using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private float _eggTimer;
    [SerializeField] private LayerMask _playerMask;
    
    private bool _coroutine;
    private int _scoreAdded;
    private GameObject _scoreGameObject;
    [SerializeField] private int _scoreNumberAdd;
    [SerializeField] private GameObject _RedPenguin;
    
    private bool _oneTime;

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
            _scoreGameObject.GetComponent<Score>().ScoreNumber += _scoreNumberAdd;

            Destroy(gameObject);
        }
        
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

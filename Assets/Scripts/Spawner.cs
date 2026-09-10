using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Sequence _seq;
    private LayerMask _enemyMask;
    
    [SerializeField] private GameObject _bluePenguin;
    [SerializeField] private GameObject _redPenguin;
    [SerializeField] private GameObject _bird;
    
    [SerializeField] private List<GameObject> _birdlist = new List<GameObject>();
    
    [SerializeField] private TextMeshProUGUI _text;

    private int _waves = 1;
    [SerializeField] private Transform spawnPos2;
    [SerializeField] private Transform spawnPos3;
    
    [SerializeField] private Transform _birdSpawn;

    [SerializeField] private GameObject SquidSpawn;
    [SerializeField] private DestructibleTilemap _destroyTilemap;

    [SerializeField] private TextMeshProUGUI _lastRoundText;
    
    private void Start()
    {
        //_text = GetComponent<TextMeshProUGUI>();
        _seq = DOTween.Sequence();
        
        StartCoroutine(Spawn());
        StartCoroutine(BirdSpawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (this.transform.childCount <= 2)
            {
                yield return new WaitForSeconds(0.5f);
                _text.text = "Wave " + _waves;
                Debug.Log( "Wave number : "+ _waves );
                
                _seq.Append(_text.transform.DOLocalMoveX(-1100, 0).SetEase(Ease.InCubic));
                _seq.Append(_text.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InCubic));
                _seq.Join(_text.transform.DOScale(new Vector2(3, 3), 0.5f).SetEase(Ease.Linear));
                
                _seq.Join(_text.transform.DOLocalMoveX(1100, 0.5f).SetDelay(1f).SetEase(Ease.InCubic)); 
                _seq.Join(_text.transform.DOScale(new Vector2(1, 1), 0.5f).SetEase(Ease.Linear).SetDelay(1));
                
                yield return new WaitForSeconds(1f);

                
                //var EnemyNumber = Random.RandomRange(1, 4);
                var EnemyNumber = Mathf.RoundToInt((1.3f * Mathf.Sqrt(_waves)) + 2);
                Debug.Log(EnemyNumber);
                for (int i = 0; i < EnemyNumber; i++)
                {
                   var RandomPos = Random.RandomRange(1, 4);
                   var RandomEnemy = Random.Range(1,3);
                   GameObject enemy;

                   if (RandomEnemy == 1 && _waves > 8)
                       enemy = _redPenguin;
                   else
                       enemy = _bluePenguin;
                   
                   if (RandomPos == 1)
                   { 
                       var LastPenguin = Instantiate(enemy, this.transform);
                       LastPenguin.transform.position = this.transform.position;
                   }
                   
                   if (RandomPos == 2)
                   { 
                       var LastPenguin = Instantiate(enemy, this.transform);
                       LastPenguin.transform.position = spawnPos2.transform.position;
                   }
                   
                   if (RandomPos == 3)
                   { 
                       var LastPenguin = Instantiate(enemy, this.transform);
                       LastPenguin.transform.position = spawnPos3.transform.position;
                   } 
                   
                   yield return new WaitForSeconds(1f);
                }
                if (_waves == 3 &&  _destroyTilemap.isActiveAndEnabled == false)
                    _destroyTilemap.enabled = true;

                if (_waves == 4 && SquidSpawn.activeInHierarchy == false)
                    SquidSpawn.SetActive(true);
                
                _lastRoundText.text = "game over\n wave " + _waves;
                _waves++;

            }
            
            yield return new WaitForSeconds(1f);  
        }
    }

    private IEnumerator BirdSpawn()
    {
        while (true)
        {
            while (this.transform.childCount <= 2)
            {
                foreach (var thing in _birdlist)
                   Destroy(thing); 

                yield return null;
            }

            if (this.transform.childCount > 2)
            {
                Debug.Log("birdspawn1234");
                yield return new WaitForSeconds(15f);
                var PosY = Random.Range(1.5f, 4);
                _birdlist.Add(Instantiate(_bird, new Vector2(_birdSpawn.position.x, PosY), Quaternion.identity));
                Debug.Log("birdspawn");
            }
            yield return new WaitForSeconds(0.5f);
            
        }
        
        Debug.Log("birdspaw23635745n");
        while (this.transform.childCount > 2)
        {
            Debug.Log("birdspawn1234");
            yield return new WaitForSeconds(15f);
            Instantiate(_bird);
            Debug.Log("birdspawn");
        }

    }
}

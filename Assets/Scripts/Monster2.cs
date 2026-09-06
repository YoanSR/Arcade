
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    private Collider2D _collider;

    [SerializeField] private GameObject _monsterSpawning;
    [SerializeField] private GameObject _front;

    public GameObject _otherGameObject;


    private Monster _monster;
    
    private GameObject _monsterSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Monster" )
        {
             Debug.Log(other.gameObject.name);
             _otherGameObject = other.gameObject;
            _monsterSpawn = Instantiate(_monsterSpawning, other.transform);
            //_monsterSpawn = Instantiate(_monsterSpawning, other.transform.position - new Vector3(0, 2), Quaternion.identity);
            _monsterSpawn.GetComponent<Monster>()._invocator = _otherGameObject;
            
        }
       
        //test
        //_monstersSpn.Add(Instantiate(_monsterSpawning, other.transform.position - new Vector3(0, 2), Quaternion.identity).GetComponent<Monster>()._invocator = _otherGameObject);
        //_monstersSpn[0].GetComponent<Monster>()._invocator = _otherGameObject;
    }
}

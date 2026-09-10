
using UnityEngine;

public class Squid : MonoBehaviour
{
    private Collider2D _collider;
    private SquidSpawner _squidSpawner;
    
    [SerializeField] private GameObject _monsterSpawning;
    [SerializeField] private GameObject _front;

    public GameObject OtherGameObject;
    private GameObject _monsterSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Monster" )
        {
             Debug.Log(other.gameObject.name);
             OtherGameObject = other.gameObject;
            _monsterSpawn = Instantiate(_monsterSpawning, other.transform);
            //_monsterSpawn = Instantiate(_monsterSpawning, other.transform.position - new Vector3(0, 2), Quaternion.identity);
            _monsterSpawn.GetComponent<SquidSpawner>().Invocator = OtherGameObject;
        }
       
        //test
        //_monstersSpn.Add(Instantiate(_monsterSpawning, other.transform.position - new Vector3(0, 2), Quaternion.identity).GetComponent<Monster>()._invocator = _otherGameObject);
        //_monstersSpn[0].GetComponent<Monster>()._invocator = _otherGameObject;
    }
}

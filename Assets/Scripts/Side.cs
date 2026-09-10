using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] private GameObject _sidePos;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
            other.transform.position = new Vector3(_sidePos.transform.position.x, other.transform.position.y, other.transform.position.z);
    }
}

using System;
using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] private GameObject sidePos;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
            other.transform.position = new Vector3(sidePos.transform.position.x, other.transform.position.y, other.transform.position.z);
    }
}

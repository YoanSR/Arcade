using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour,IPointerEnterHandler
{
    private Rigidbody2D _rb;
    
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _selectSound;
    

    void Start()
    {
        _source = GameObject.Find("SFX").GetComponent<AudioSource>();
        
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_selectSound);
        _rb.AddTorque(10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_rb.angularVelocity > 1000)
            _rb.angularVelocity = 1000;
    }
}

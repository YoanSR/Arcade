using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private Sequence seq;

    private Rigidbody2D _rb;
    
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _selectSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GameObject.Find("SFX").GetComponent<AudioSource>();
        
        seq = DOTween.Sequence();
        
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.PlayOneShot(_selectSound);
        
        _rb.AddTorque(10, ForceMode2D.Impulse);
        //seq.Append(transform.DOro(new Vector3(0, 0 ,-180), 0.2f).SetEase(Ease.InCirc));
        Debug.Log("OnPointerEnter");
        //seq.Append(transform.DORotate(new Vector3(0, 0 ,-180), 0.2f).SetEase(Ease.InCirc));
        //seq.Append(transform.DOScale(new Vector2(1.25f, 1.25f ), 0.2f).SetEase(Ease.InCirc));
    }

    private void Update()
    {
        if (_rb.angularVelocity > 1000)
            _rb.angularVelocity = 1000;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //seq.Append(transform.DORotate(new Vector3(0, 0 ,0 ), 0.2f).SetEase(Ease.InCirc));
        //seq.Append(transform.DOScale(new Vector2(1f, 1f ), 0.2f).SetEase(Ease.InCirc));
    }
}

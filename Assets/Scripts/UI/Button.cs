using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    
    private Sequence seq;

    [SerializeField] private Sprite _buttonImage;
    [SerializeField] private Sprite _pressedButtonImage;
    
    [SerializeField] private float _previewPos = 60;
    [SerializeField] float _mouseOnPos;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _selectSound;
    
    private void Start()
    {
        seq = DOTween.Sequence();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        seq.Append(transform.DOMoveX(_mouseOnPos, 0.1f).SetEase(Ease.InCubic));
        _source.PlayOneShot(_selectSound);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = _pressedButtonImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = _buttonImage;
        seq.Append(transform.DOMoveX(_previewPos, 0.1f).SetEase(Ease.InCubic));
        
    }
}

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BestScore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private Sequence seq;
    
    [SerializeField] private float _previewPos = 60;
    [SerializeField] float _mouseOnPos;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _selectSound;
    
    private void Start()
    {
        seq = DOTween.Sequence();
        
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("BestScore") + " <- best score";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        seq.Append(transform.DOMoveX(_mouseOnPos, 0.1f).SetEase(Ease.InCubic));
        _source.PlayOneShot(_selectSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        seq.Append(transform.DOMoveX(_previewPos, 0.1f).SetEase(Ease.InCubic));
    }
}
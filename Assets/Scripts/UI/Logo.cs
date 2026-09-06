using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using Object = System.Object;


public class Logo : MonoBehaviour
{
    private Sequence seq;
    
    [SerializeField] private RectTransform _bannerRectTransform;

    private bool isLogoNewPos;
    private bool _firstMenu = true;
    
    [SerializeField] private Transform _transition;

    [SerializeField] private string _scenename;

    [SerializeField] private Vector2 _bannerNewPos;
    [SerializeField] private Vector2 _logoNewPos;
    
    [SerializeField] private Transform _text;
    [SerializeField] private Transform _banner;

    [SerializeField] private Transform _Button1;
    [SerializeField] private Transform _Button2;
    [SerializeField] private Transform _Button3;
    
    [SerializeField] private Transform _bestScore;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seq = DOTween.Sequence();
        
        seq.Append(transform.DOScale(1.5f,0));
        seq.Append(transform.DOScale(1,1).SetEase(Ease.OutCubic));

    }

    // Update is called once per frame
    void Update()
    {
        //Pointer.current.position.ReadValue();
       
        
            /*var transformPosZ = new Vector2(transform.position.x, transform.position.y) ;
            //transform.localPosition = transformPosZ * (Pointer.current.position.ReadValue().magnitude);
            Debug.Log(Pointer.current.position.ReadValue());
            //transform.localPosition = Pointer.current.position.ReadValue();
            var mousePos = new Vector2(Pointer.current.position.ReadValue().x - 960, Pointer.current.position.ReadValue().y - 540);
            transform.position = mousePos * 0.025f;*/
        
        
        
        if (_firstMenu == true)
            InputSystem.onAnyButtonPress.CallOnce(currentAction =>
            {
                if (_firstMenu == true)
                {
                    _firstMenu = false;
                    Debug.Log("ca");
                    seq.Append(transform.DOLocalMove(_logoNewPos, 1).SetEase(Ease.InCubic));
                        //.OnComplete(() => { isLogoNewPos = true; }));

                    seq.Append(_bannerRectTransform.DOSizeDelta(new Vector2(1024, 0), 0.5f).SetEase(Ease.InCubic));
                    seq.Append(_text.DOScale(new Vector3(2 ,0 ), 0.5f).SetEase(Ease.InCubic)); 
                    
                    //seq.Append(_banner.DOLocalMove(_bannerNewPos, 1).SetEase(Ease.InCubic)); 
                    //seq.Append(_banner.DORotate(new Vector3(0, 0, -90), 1).SetEase(Ease.InCubic));
                    
                    seq.Append(_Button1.DOMoveX(60, 0.5f).SetEase(Ease.InCubic).SetDelay(1));
                    seq.Append(_Button2.DOMoveX(60, 0.5f).SetEase(Ease.InCubic).SetDelay(1.5f));
                    seq.Append(_Button3.DOMoveX(60, 0.5f).SetEase(Ease.InCubic).SetDelay(2));
                    seq.Append(_bestScore.DOMoveX(20, 0.5f).SetEase(Ease.InCubic).SetDelay(2.5f));
                }
            });
    }

    public void LaunchNewScene()
    {
        //Debug.Log(_scenename);
        //SceneManager.LoadScene(_scenename);
        seq.Append(_transition.DOScale(50, 1f).SetEase(Ease.InCubic).OnComplete(() => { SceneManager.LoadScene(_scenename);}));
        seq.Append(_transition.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetEase(Ease.InCubic));
    }
}

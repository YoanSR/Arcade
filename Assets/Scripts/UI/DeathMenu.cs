using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    private Sequence _seq;
    
    [SerializeField] private string _gameSceneName;
    [SerializeField] private string _MainMenuSceneName;

    [SerializeField] private Transform _transition;

    private void Start()
    {
        _transition.DOScale(20f, 0);
        _transition.DOScale(0f, 0.5f).SetEase(Ease.InCubic);
        
        _seq = DOTween.Sequence();
    }

    public void Retry()
    {
        _seq.Append(_transition.DOScale(50, 1f).SetEase(Ease.InCubic).OnComplete(() => { SceneManager.LoadScene(_gameSceneName);}));
        _seq.Append(_transition.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetEase(Ease.InCubic));
        //SceneManager.LoadScene(_gameSceneName);
    }

    public void MainMenu()
    {
        
        SceneManager.LoadScene(_MainMenuSceneName);
    }
}

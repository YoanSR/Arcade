using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private float _firstPos;

    [SerializeField] private Image _opacity;
    [SerializeField] private Transform _childScale;
    
    [SerializeField] AudioMixer _masterMixer;
    
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _SFXVolumeSlider;
    
    [HideInInspector] public bool _isActive;
    [HideInInspector] public bool _isPlaying;
    
    
    void Start()
    {
        _firstPos = gameObject.transform.localPosition.x;

        if (PlayerPrefs.HasKey("MasterVolume"))
            LoadVolume();
        else
        {
            MasterSound();
            Music();
            SFX();
        }
        
    }

    public void Menu()
    {
        
        Sequence seq = DOTween.Sequence().SetAutoKill(false).SetUpdate(true);
        
        _isPlaying = true;
        
        seq.SetUpdate(true);
        _isActive = true;
        _opacity.DOFade(0.4f, 0.5f).SetUpdate(true).OnComplete(() => { _isPlaying = false; });;
        _opacity.raycastTarget = true;
        seq.Join(_childScale.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.InCubic));
        seq.Join(transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InCubic));
        seq.Join(transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1600, 1200), 0.25f).SetEase(Ease.InCubic).SetDelay(0.25f));
        
        //seq.Append(_opacity.DOLocalMoveX(0, 1).SetEase(Ease.InCubic));
        //seq.Append(_opacity.DOScale(new Vector2(20, 20), 0.5f).SetEase(Ease.InCubic).SetDelay(1));
    }

    public void CloseMenu()
    {
        _isPlaying = true;
        
        Time.timeScale = 1;
        Sequence seq = DOTween.Sequence().SetAutoKill(false).SetUpdate(true);
        
        _isActive = false;
        _opacity.DOFade(0f, 0.5f).OnComplete(() => { _opacity.raycastTarget = false; _isPlaying = false; });
        //_opacity.raycastTarget = false;
        seq.Join(transform.DOLocalMoveX(_firstPos, 0.5f).SetEase(Ease.InCubic));
        seq.Join(_childScale.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic));
        seq.Join(transform.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1280, 92), 0.25f).SetEase(Ease.InCubic).SetDelay(0.25f));
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void MasterSound()
    {
        float volume = _masterVolumeSlider.value;
        _masterMixer.SetFloat("MasterVolume", volume * 16);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    public void Music()
    {
        float volume = _musicVolumeSlider.value;
        _masterMixer.SetFloat("MusicVolume", volume * 16);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void SFX()
    {
        float volume = _SFXVolumeSlider.value;
        _masterMixer.SetFloat("SFXVolume", volume * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MasterSound();

        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        Music();
        
        _SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SFX();
    }
}

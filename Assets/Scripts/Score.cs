using UnityEngine;

public class Score : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _score;

    public int _scoreNumber;

    [SerializeField] private int _totalScoreNumber;
    private int life;

    [SerializeField] private GameObject _Player;

    private void Start()
    {
        _score = GetComponent<TMPro.TextMeshProUGUI>();

        //if (PlayerPrefs.HasKey("BestScore"))        
            //_scoreNumber = PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    private void Update()
    {
        
         _totalScoreNumber = _scoreNumber - 10000*life;
         
         if (_totalScoreNumber >= 10000)
         {
             _Player.GetComponent<PlayerAttack>()._deathNumber--;
             life++;
         }
             
         _score.text = "" + _scoreNumber;


       
    }
}

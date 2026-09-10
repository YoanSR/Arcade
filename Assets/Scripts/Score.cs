using UnityEngine;

public class Score : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _score;

    public int ScoreNumber;

    [SerializeField] private int _totalScoreNumber;
    private int _life;

    [SerializeField] private GameObject _player;

    private void Start()
    {
        _score = GetComponent<TMPro.TextMeshProUGUI>();

        //if (PlayerPrefs.HasKey("BestScore"))        
            //_scoreNumber = PlayerPrefs.GetInt("BestScore");
    }


    private void Update()
    {
        
         _totalScoreNumber = ScoreNumber - 10000*_life;
         
         if (_totalScoreNumber >= 10000)
         {
             _player.GetComponent<PlayerAttack>().DeathNumber--;
             _life++;
         }
             
         _score.text = "" + ScoreNumber;


       
    }
}

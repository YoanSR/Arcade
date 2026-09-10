using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap _dt;
    
    [SerializeField] private RuleTile _breakingAnimation;
    [SerializeField] private RuleTile _breakingAnimation2;
    
    [SerializeField] private ParticleSystem _breakingParticles;
    [SerializeField] private ParticleSystem _breakingParticles2;
    
    private Sequence _seq;
    
    void Start()
    {
        _dt = GetComponent<Tilemap>();
        StartCoroutine(DestroyRoutine());
        
        _seq = DOTween.Sequence();
    }

    private IEnumerator DestroyRoutine()
    {
        _dt.CompressBounds();
        //foreach (Vector3Int position in _dt.cellBounds.allPositionsWithin)
        //{
            //var aa = _dt.GetTile(position);
            //Debug.Log(position);
            
            _seq.Append(_breakingParticles.transform.DOMoveX(-5,10).SetEase(Ease.Linear));
            _seq.Append(_breakingParticles2.transform.DOMoveX(5,10).SetEase(Ease.Linear));
            
        //}
        
        for (var i = -11; i < -4; i++)
        {
            int aa;
            aa = i * -1 - 1;

            //Debug.Log(i);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(i, -5, 0)), _breakingAnimation);
            _dt.SetTile(_dt.WorldToCell(new Vector3(aa, -5, 0)), _breakingAnimation2);
            _dt.CompressBounds();
               
            yield return new WaitForSeconds(2f);
            _dt.SetTile(_dt.WorldToCell(new Vector3(i, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(aa, -5, 0)), null);
        }
    }
}
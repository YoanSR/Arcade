using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTilemap : MonoBehaviour
{
    [SerializeField] private Tilemap _dt;
    
    [SerializeField] private new List<BoundsInt.PositionEnumerator> _tile = new();
    
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

    // Update is called once per frame
    void Update()
    {
        //_dt.SetTile(_dt.WorldToCell(transform.position), null);

        //var caca = _dt.WorldToCell(transform.position);
            //Debug.Log(caca);



        //foreach (Vector3Int position in _dt.cellBounds.allPositionsWithin)
        //{
          //  Debug.Log(position);
        //}
        

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
            //gameObject.GetComponent<CompositeCollider2D>().collid;
            
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(i, -5, 0)), _breakingAnimation);
            _dt.SetTile(_dt.WorldToCell(new Vector3(aa, -5, 0)), _breakingAnimation2);
            _dt.CompressBounds();
               
            yield return new WaitForSeconds(2f);
            _dt.SetTile(_dt.WorldToCell(new Vector3(i, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(aa, -5, 0)), null);
        }

        
        
        
        //_dt.CompressBounds();
        /*foreach (Vector3Int position in _dt.cellBounds.allPositionsWithin)
        {
            var aa = _dt.GetTile(position);
            Debug.Log(position);
            
            
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-10, -5, 0)), _breakingAnimation);
               
            yield return new WaitForSeconds(7);
            _dt.SetTile(_dt.WorldToCell(new Vector3(-10, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(9, -5, 0)), null);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-9, -5, 0)), _breakingAnimation);
            yield return new WaitForSeconds(7);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-9, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(8, -5, 0)), null);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-8, -5, 0)), _breakingAnimation);
            yield return new WaitForSeconds(7);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-8, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(7, -5, 0)), null);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-7, -5, 0)), _breakingAnimation);
            yield return new WaitForSeconds(7);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-7, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(6, -5, 0)), null);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-6, -5, 0)), _breakingAnimation);
            yield return new WaitForSeconds(7);
            
            _dt.SetTile(_dt.WorldToCell(new Vector3(-6, -5, 0)), null);
            _dt.SetTile(_dt.WorldToCell(new Vector3(5, -5, 0)), null);
            
            //_dt.SetTile(_dt.WorldToCell(new Vector3(-5, -5, 0)), _breakingAnimation);
            yield return new WaitForSeconds(4);
            //_dt.SetTile(_dt.WorldToCell(position), null);
            
        }
        
        //_dt.SetTile(_dt.WorldToCell(), null);
        //dt.SetTile(_dt.cell);
        yield return new WaitForSeconds(1);*/
    }

    


}

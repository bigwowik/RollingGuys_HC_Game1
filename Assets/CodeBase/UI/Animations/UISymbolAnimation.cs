using System.Collections;
using System.Collections.Generic;
using CodeBase.Helpers.Debug;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;

public class UISymbolAnimation : MonoBehaviour
{
    private readonly float _angle = -30f;

    // Start is called before the first frame update
    void Start()
    {
        
        WDebug.Log("Start UISymbolAnimation", WType.UI);
        transform.rotation= Quaternion.Euler(0, 0, -_angle);
        
        var endRot = new Vector3(0, 0, _angle);
        
        
        Sequence s = DOTween.Sequence();
        s.Append(transform.DORotate(endRot, 1f, RotateMode.Fast).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo));
        s.SetLoops(-1, LoopType.Yoyo);


        gameObject.OnDestroyAsObservable()
            .Take(1)
            .Subscribe(_ => s.Kill());
        
        //transform.DORotate(endRot, 2f, RotateMode.Fast);
    }
    
    // IEnumerator Start()
    // {
    //     // Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
    //     yield return new WaitForSeconds(1);
    //
    //     // Create a new Sequence.
    //     // We will set it so that the whole duration is 6
    //     Sequence s = DOTween.Sequence();
    //     // Add an horizontal relative move tween that will last the whole Sequence's duration
    //     s.Append(cube.DOMoveX(6, duration).SetRelative().SetEase(Ease.InOutQuad));
    //     // Insert a rotation tween which will last half the duration
    //     // and will loop forward and backward twice
    //     s.Insert(0, cube.DORotate(new Vector3(0, 45, 0), duration / 2).SetEase(Ease.InQuad).SetLoops(2, LoopType.Yoyo));
    //     // Add a color tween that will start at half the duration and last until the end
    //     s.Insert(duration / 2, cube.GetComponent<Renderer>().material.DOColor(Color.yellow, duration / 2));
    //     // Set the whole Sequence to loop infinitely forward and backwards
    //     s.SetLoops(-1, LoopType.Yoyo);
    // }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuControl : MonoBehaviour
{


    [SerializeField] private Text TapToPlay;
    void Start()
    {
        TapToPlay.transform.DOScale(1.1f, 0.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutFlash);
    }


}

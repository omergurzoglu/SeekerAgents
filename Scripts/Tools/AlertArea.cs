

using DG.Tweening;
using UnityEngine;

public class AlertArea : MonoBehaviour
{
   

    private void Start()
    {
        transform.DOScale(Vector3.one * 6f, 1.4f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
    }


   
}

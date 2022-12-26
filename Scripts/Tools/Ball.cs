


using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;


public class Ball :MonoBehaviour


{
    [SerializeField] private GameObject alertArea;
    private float _ballHealth=1;
    
    private void OnCollisionEnter()
    {
        _ballHealth--;
        if (_ballHealth <= 0)
        {
            Instantiate(alertArea,transform.position,alertArea.transform.rotation);
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBounce).OnComplete(()
                => gameObject.SetActive(false));
        }
    }
}

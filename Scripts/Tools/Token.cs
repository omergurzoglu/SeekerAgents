
using DG.Tweening;
using UnityEngine;

public class Token : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private void Start() => transform.DOLocalRotate(new Vector3(0, 360, 0), 5f,RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out _)) return;
        _boxCollider.isTrigger = false;
        StatusCheck.Instance.tokenCount++;
        StatusCheck.Instance.UpdateScore();
        transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBounce).OnComplete((() => gameObject.SetActive(false)));
    }
}

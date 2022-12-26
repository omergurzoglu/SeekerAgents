

using UnityEngine;

public class Panel : MonoBehaviour
{
    void Start() => transform.LeanScale(Vector3.one, 1f).setEase(LeanTweenType.easeInBounce);
}

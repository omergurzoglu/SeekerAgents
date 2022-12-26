using UnityEngine;

public class SpringRaycastController : MonoBehaviour
{
    public Transform playerTransform;
    public float raycastDistance = 1.0f;
    public float upwardForce = 1.5f;
    public float maxUpwardVelocity = 5.0f;
    private float _springVelocity;
    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = playerTransform.GetComponent<Rigidbody>();

    void Update()
    {
        var ray = new Ray(playerTransform.position, -playerTransform.up);
       
        if (Physics.Raycast(ray, out _, raycastDistance))
        {
            _springVelocity = Mathf.Clamp(_springVelocity, -maxUpwardVelocity, maxUpwardVelocity);
            playerTransform.position += playerTransform.up * (_springVelocity * Time.deltaTime);
            _rigidbody.AddForce(playerTransform.up * upwardForce, ForceMode.Acceleration);
            _rigidbody.useGravity = false;
        }
        else
        {
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(Vector3.down*5,ForceMode.Acceleration);
        }
    }
}


using UnityEngine;



public class BallThrower : MonoBehaviour
{
    public new Camera camera;
    [SerializeField] private Transform ballLaunchPos;
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int linePoints = 25;
    [SerializeField] private float timeBetweenPoints = 0.1f;
    
    private void Update()
    {
        PredictLine();
        ThrowBall();
    }


    private void ThrowBall()
    {
        if (Input.GetMouseButtonUp(1))
        {
            lineRenderer.enabled = false;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var aimPoint = hit.point;
            var throwDirectionVector = aimPoint - ballLaunchPos.position;
            var newBall=Instantiate(ballRigidbody, ballLaunchPos.position, Quaternion.identity);
            newBall.AddForce(throwDirectionVector.normalized*40f,ForceMode.Impulse);
        }
    }

    private void PredictLine()
    {
        if (Input.GetMouseButton(1))
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit);
            var mousePos = hit.point;
            var lineDirection = (mousePos - ballLaunchPos.position).normalized;
            var startVelocity =  lineDirection / ballRigidbody.mass;
            var i = 0;
            for (float time = 0; time < linePoints; time+=timeBetweenPoints)
            {
                i++;
                var point = ballLaunchPos.position + time * startVelocity;
                point.y = 1f;
                lineRenderer.SetPosition(i,point);
                
            }

        }
    }


    
}

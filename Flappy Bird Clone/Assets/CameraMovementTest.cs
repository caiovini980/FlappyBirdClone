using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rate;
    [SerializeField] private float xOffset;
    
    // Update is called once per frame
    private void Update()
    {
        // Only enable when countdown ends
        Vector3 cameraPosition = gameObject.transform.position;
        Vector3 newPosition = new Vector3(target.position.x + xOffset, cameraPosition.y, cameraPosition.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, rate * Time.deltaTime);
    }
}

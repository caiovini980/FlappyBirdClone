using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(.5f, 0, 0) * (/*.5f*/ 1 * Time.deltaTime);
    }
}

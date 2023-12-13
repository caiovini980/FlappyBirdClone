using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * (2 * Time.deltaTime);
    }
}

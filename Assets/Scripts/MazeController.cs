using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(-Input.acceleration.x * 35 * Time.deltaTime, 0, Input.acceleration.z * 35 * Time.deltaTime);
    }
}

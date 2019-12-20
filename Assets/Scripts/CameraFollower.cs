using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject follower;
    public float scaling;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position - Vector3.forward * 10, follower.transform.position, scaling);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_O : MonoBehaviour {
    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;

    void LateUpdate()
    {
        var y = Input.GetAxis("Mouse Y") * 3f;
       // var x = Input.GetAxis("Mouse X") * 3f;
        this.GetComponent<Camera>().transform.Rotate(-y, 0, 0);
    }
}

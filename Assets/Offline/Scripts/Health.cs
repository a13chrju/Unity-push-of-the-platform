using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int team; //1 red or 2 blue
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 115, ForceMode.Force);
    }

    void LateUpdate()
    {
        Vector3 offset = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        Quaternion deltaRotation = Quaternion.Euler(offset * Time.fixedDeltaTime * 200);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}

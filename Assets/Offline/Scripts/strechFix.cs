using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strechFix : MonoBehaviour
{
    private Vector3 startPosition;
    void Start() {
        startPosition = transform.localPosition;
    }
    void LateUpdate() {
        transform.localPosition = startPosition;
    }
}

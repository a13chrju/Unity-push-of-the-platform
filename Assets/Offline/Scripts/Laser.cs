using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	private LineRenderer line;
	// Use this for initialization
	public bool right = true;
	public Material lineMaterial;
	void Start () {
        line = gameObject.AddComponent<LineRenderer>();
		line.material = lineMaterial;
		// Debug.DrawLine(transform.position, transform.forward, Color.red);
	}

	// Update is called once per frame
	void Update () {
		if (!line) return;
        if (right)
        {
			transform.Rotate(Vector3.up * Time.deltaTime * 70f);
		} else
        {
			transform.Rotate(Vector3.up * Time.deltaTime * -70f);
		}
		line.SetPosition(0, transform.position);
		line.SetPosition(1, Vector3.forward);

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 360f))
		{
			line.enabled = true; line.SetPosition(1, hit.point);
            if (hit.collider.tag == "Player")
            {
				hit.collider.GetComponent<Death_O>().killPlayer();
            }
		} else
        {
			line.SetPosition(1, transform.position); line.enabled = false;
		}
	}
}

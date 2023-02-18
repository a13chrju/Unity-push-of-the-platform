using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2 : MonoBehaviour {

	private LineRenderer line;
	// Use this for initialization
	public bool right = true;
	void Start()
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, transform.position);
	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 360f))
		{
			line.enabled = true; line.SetPosition(1, hit.point);
		}
		else
		{
			line.SetPosition(1, transform.position); line.enabled = false;
		}
	}
}

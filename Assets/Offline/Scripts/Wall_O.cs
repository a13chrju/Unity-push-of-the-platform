using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_O : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			Debug.Log("fall");
			collision.collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}

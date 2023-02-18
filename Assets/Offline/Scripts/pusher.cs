using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusher : MonoBehaviour {
	// Use this for initialization
	public float force = 78;
	void Start () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "Player"){
			collision.collider.gameObject.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * force, ForceMode.Impulse);
		}
	}
}

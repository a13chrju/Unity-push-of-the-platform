using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour {

	// Use this for initialization
	public GameObject[] enemies;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			
			foreach (GameObject objG in enemies)
			{
				objG.GetComponent<Enemy_O>().enabled = true;
			}

		}
	}

	private void OnTriggerExit(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{

			foreach (GameObject objG in enemies)
			{
				objG.GetComponent<Enemy_O>().enabled = false;
			}

		}
	}
}

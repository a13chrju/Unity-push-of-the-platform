using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
	void Start()
	{
		StartCoroutine(remove());
	}

	// Update is called once per frame

	IEnumerator remove()
	{
		yield return new WaitForSeconds(4);
		Destroy(this.gameObject);
	}
}

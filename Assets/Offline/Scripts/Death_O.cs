using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_O : MonoBehaviour {
	public Camera deadCam;
	public Camera playerCam;
	public GameObject spawnPosition, lookPosition;
	private Rigidbody rb;
	private Vector3 startPosition;
	private Animator anim;
	// Use this for initialization
	public bool isDead = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		startPosition = this.transform.position;
		anim = GetComponent<Animator>();
	}

	IEnumerator changeCam()
	{
		isDead = true;
		GetComponent<MoveRB_O>().enabled = false;
		stopAnim();
		GetComponent<Fire_O>().enabled = false;
		yield return new WaitForSeconds(1);
		//playerCam.enabled = false;
		//deadCam.enabled = true;
		//deadCam.GetComponent<Animator>().enabled = true;

		yield return new WaitForSeconds(5);
		//playerCam.enabled = true;
		//deadCam.enabled = false;
		//deadCam.GetComponent<Animator>().enabled = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		resetPosition();
		deadCam.GetComponent<Animator>().enabled = false;
		yield return new WaitForSeconds(3);
		isDead = false;
		GetComponent<MoveRB_O>().enabled = true;
		GetComponent<Fire_O>().enabled = true;
		/*Vector3 direction = lookPosition.transform.position - spawnPosition.transform.position;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(direction));*/

	}

	public void killPlayer()
    {
		GetComponent<Animator>().SetTrigger("Die");
		GetComponent<MoveRB_O>().enabled = false;
		GetComponent<Fire_O>().enabled = false;
	}

	public void resetPosition()
	{
		rb.MovePosition(startPosition);
	}

	public void stopAnim()
	{
		if (anim)
		{
			anim.SetFloat("Vertical", 0);
			anim.SetFloat("Horizontal", 0);
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Lava")
        {
			StartCoroutine(changeCam());
		}
    }
}

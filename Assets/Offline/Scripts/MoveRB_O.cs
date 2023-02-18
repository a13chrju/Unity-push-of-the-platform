using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRB_O : MonoBehaviour {

	Rigidbody rb;
	public float speed;
	public float speedMultiplier;
	private Vector3 inputVector;
	private Animator anim;
	private bool isFalling = false;

	ParticleSystem lineBehind;
	 void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		lineBehind = GetComponentInChildren<ParticleSystem>();
		speed = 31f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	 void Update () {
		if (!IsGroundedTwo())
        {
			isFalling = true;
			lineBehind.emissionRate = 1;
			//anim.SetBool("isfalling", true);
           // Debug.DrawLine(transform.position, transform.forward, Color.red);
        }
		if (IsCloseGroundedTwo() && isFalling)
		{
			lineBehind.emissionRate = 0;
			isFalling = false;
			//anim.SetBool("isfalling", false);
		}

		if (Input.GetKeyDown("space") && IsGroundedTwo())
		{
			anim.SetTrigger("jump");
			rb.AddForce(Vector3.up * 33, ForceMode.VelocityChange);
		}
	}

	void FixedUpdate()
    {
		float moveHorizontalTemp = 0, moveVerticalTemp = 0;

		if (Input.GetKey(KeyCode.D)) { moveHorizontalTemp = (2 * -1f) * speed; }
		if (Input.GetKey(KeyCode.A)) { moveHorizontalTemp = (-2 * -1f) * speed; }
		if (Input.GetKey(KeyCode.W)) { moveVerticalTemp = (-2 * -1f) * speed; }
		if (Input.GetKey(KeyCode.S)) { moveVerticalTemp = (2 * -1f) * speed; }


		Vector3 movHorizontal = (transform.right * moveHorizontalTemp) * -1f;
		Vector3 movVertical = ((transform.forward * moveVerticalTemp) * 1f);

		Animating(moveHorizontalTemp, moveVerticalTemp);
		Vector3 Velocity = ((movHorizontal + movVertical).normalized) * speedMultiplier;
		rb.AddForce(Velocity);
	}
	public bool IsGroundedTwo()
	{
		return Physics.Raycast(transform.position, -Vector3.up, 1);
	}

	public bool IsCloseGroundedTwo()
	{
		return Physics.Raycast(transform.position, -Vector3.up, 6);
	}

	public void Animating(float valone, float valtwo)
	{
		bool walking = valone != 0f || valtwo != 0f;
		anim.SetFloat("Vertical", valtwo * 1);
		anim.SetFloat("Horizontal", valone * -1);
	}
}

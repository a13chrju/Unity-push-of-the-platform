using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCon_O : MonoBehaviour {

    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;
    public Animator anim; 
    public float jumpHeight = 3;

    Vector3 velocity;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

        void Update()
    {

        if (IsGroundedTwo() && velocity.y < 0)

        {

            velocity.y = -2f;

        }

        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeed = speed * Input.GetAxis("Vertical");
        float curSpeed2 = speed * Input.GetAxis("Horizontal");
        controller.SimpleMove(forward * curSpeed);
        controller.SimpleMove(right * curSpeed2);

        Animating(curSpeed2, curSpeed);


        if (Input.GetKeyDown("space") && IsGroundedTwo())
        {
            Debug.Log("JUMP2");

            // source.PlayOneShot(jumpSound, 1f);
         //   velocity.y = Mathf.Sqrt(jumpHeight * -2 * -9);
            // ragdollify();

        }
       // velocity.y -= 23 * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);

    }
    public bool IsGroundedTwo()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 3);
    }

    public void Animating(float valone, float valtwo)
    {
        bool walking = valone != 0f || valtwo != 0f;

        // anim.SetBool("isfalling", false);

        anim.SetFloat("Vertical", valtwo * 1);
        anim.SetFloat("Horizontal", valone );
        //anim.SetBool("isrunning", walking);


    }
}

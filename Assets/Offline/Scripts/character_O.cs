using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_O : MonoBehaviour {

    public CharacterController controller;

    public Transform cam;



    public float speed = 6;

    public float gravity = -9.81f;

    public float jumpHeight = 3;

    Vector3 velocity;


    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;



    float turnSmoothVelocity;

    public float turnSmoothTime = 0.1f;

    private Animator anim;

    private Rigidbody rb;

    private bool isStunned = false;

    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void stunned ()
    {
        isStunned = true;
    }

    void Update()

    {

        //jump

        if (isStunned) return;

        if (isGrounded() && velocity.y < 0)

        {

          //  velocity.y = -2f;

        }
        Debug.Log(Quaternion.Euler(0f, Input.GetAxis("Mouse X"), 0f));
        Vector3 offset = new Vector3(0, Input.GetAxis("Mouse X") * 33, 0);


        //var rotation = new Vector3(0, Input.GetAxis("Mouse X") * 33 * Time.deltaTime, 0);

        //var x = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * ( 100 * Time.deltaTime));

        Vector3 move = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1);
        controller.Move(move * Time.deltaTime * 33);

        if (move != Vector3.zero)
        {
           // gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * 4);
        }

        velocity.y -= 22 * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
     
        /*

        if (Input.GetButtonDown("Jump") && isGrounded())

        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        //gravity

        velocity.y += gravity * Time.deltaTime;
        //rb.AddForce(Vector3.down * -gravity, ForceMode.Force);


        controller.Move(velocity * Time.deltaTime);

      

        //walk

        float horizontal = Input.GetAxisRaw("Horizontal");

        float horizontalM = Input.GetAxis("Mouse X");

        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;



        if (direction.magnitude >= 0.1f)

        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
           
        }
        Animating(0, direction.magnitude);*/

    }


    public void Animating(float valone, float valtwo)
    {
        bool walking = valone != 0f || valtwo != 0f;

        // anim.SetBool("isfalling", false);

       // anim.SetFloat("Vertical", valtwo  );
       // anim.SetFloat("Horizontal", valone );
        //anim.SetBool("isrunning", walking);


    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 5f);
    }
}

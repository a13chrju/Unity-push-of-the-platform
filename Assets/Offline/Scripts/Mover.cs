using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    public Rigidbody rb;
    private Collider colliMain;
    public float MoveSpeed;
    public float gravityscale;
    public float jumpforce;
    public CharacterController chara;
    public float jumptimer;
    public float speedeffect;
    public float power = 0;
    public float fall;

    public float lastClickTime;
    public GameObject particles;
    public Animator anim;
    public float distToGround;
    public Vector3 moveDirection;
    public bool isjumping = false;
    public float jumpdelay;
    public Vector3 getpushed;
    public float fireballimpactspeed;
    public GameObject innerbone;
    public float lookspeed;

    public GameObject water;

    public AudioClip jumpSound;
    private AudioSource source;
    private float timedelayjump;
    private float canjump = 0f;
    private float moveHorizontal, moveVertical;
    private Vector3 movHorizontal, movVertical;

    public float maxVel = 10f;

    private bool btnJump = false;
    // Use this for initialization
    // Use this for initialization
    private bool transitionJump = false;
    private float transitionJumpTime = 0f;
    public Rigidbody[] bodies;
    private Collider[] colliders;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        colliMain = GetComponent<Collider>();

        rb = this.GetComponent<Rigidbody>();
            chara = GetComponent<CharacterController>();
            jumptimer = Time.time + 2f; Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.lockState = CursorLockMode.Confined;
            lastClickTime = Time.time;
        // Joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<bl_Joystick>();
     
        // colliders = GetComponentsInChildren<Collider>();

        // rb.isKinematic = true;
       /* foreach (Rigidbody rb in bodies)
        {
           rb.isKinematic = true;
        }
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        rb.isKinematic = false;
        colliMain.enabled = true;*/


        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    public float rotateSpeed = 3.0F;
 
    public void ragdollify()
    {
        anim.enabled = false;
        //rb.isKinematic = true;
        foreach (Rigidbody rbs in bodies)
        {
           if(rbs != rb) rbs.isKinematic = false;
        }
        /*foreach (Collider col in colliders)
        {
            col.enabled = true;
        }*/

        // rb.isKinematic = true;
       // colliMain.enabled = false;
       // GetComponent<Mover>().enabled = false;
    }

    void Update ()
    {
        if (Input.GetKeyDown("space") && IsGroundedTwo())
        {
            Debug.Log("JUMP2");
            canjump = Time.time + 0.5f;
            // source.PlayOneShot(jumpSound, 1f);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpforce, ForceMode.Force);
            // ragdollify();
            jumpdelay = Time.time + 0.1f;
            isjumping = true;
        }

        if(Time.time > transitionJumpTime)
        {
            transitionJump = true;
        }

        if (!IsGrounded() /*&& transitionJump*/)
        {
            transitionJump = false;
            //  Debug.Log(rb.velocity.y);
            anim.SetBool("isfalling", true);
            Debug.DrawLine(transform.position, transform.forward, Color.red);
        }
        else if(IsGrounded())
        {
            anim.SetBool("isfalling", false);
        }
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude >= maxVel) return;
        //moveHorizontal = Input.GetAxis("Horizontal") * -1f; //Joystick.Horizontal
        //moveVertical = Input.GetAxis("Vertical") * -1f; // Joystick.Vertical
        float moveHorizontalTemp = 0, moveVerticalTemp = 0;

        if (Input.GetKey(KeyCode.D)) { moveHorizontalTemp = (2 * -1f) * MoveSpeed; }
        if (Input.GetKey(KeyCode.A)) { moveHorizontalTemp = (-2 * -1f) * MoveSpeed; }
        if (Input.GetKey(KeyCode.W)) { moveVerticalTemp = (-2 * -1f) * MoveSpeed; }
        if (Input.GetKey(KeyCode.S)) { moveVerticalTemp = (2 * -1f) * MoveSpeed; }


        movHorizontal = (transform.right * moveHorizontalTemp) * -1f;
        movVertical = ((transform.forward * moveVerticalTemp) * 1f);

        rb.AddForce(Vector3.down * gravityscale, ForceMode.Force);


        Vector3 Velocity = ((movHorizontal + movVertical).normalized) * MoveSpeed;
         rb.MovePosition(rb.position + Velocity * Time.deltaTime);
       // rb.velocity = new Vector3(moveVerticalTemp, 0, moveHorizontalTemp); // rb.position + Velocity * Time.deltaTime;
        Animating(moveHorizontalTemp, moveVerticalTemp);
  
        //for andriod use mouse X: Joystick.Horizontal
        Vector3 offset = new Vector3(0, Input.GetAxis("Mouse X") * lookspeed, 0);


  
         var x = Input.GetAxis("Mouse X") * 3f;
       // rb.MoveRotation(Quaternion.Euler(offset));
        rb.MoveRotation(rb.rotation * Quaternion.Euler(offset));
        //transform.Rotate(offset);
    }

    public void Animating(float valone, float valtwo)
    {
        bool walking = valone != 0f || valtwo != 0f;

        // anim.SetBool("isfalling", false);

        anim.SetFloat("Vertical", valtwo * 1);
        anim.SetFloat("Horizontal", valone * -1);
        //anim.SetBool("isrunning", walking);
      
       
    }

    public void jump()
    {
        transitionJumpTime = Time.time + 0.3f;
    }


    public bool IsGroundedTwo()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

  
    void SetKinematic(bool newValue)
    {
        GetComponent<Animator>().enabled = false;
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        rb.isKinematic = false;
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
        }
    }
    
    

    public void Hitforce(Vector3 force)
    {
            getpushed = force;
            this.gameObject.GetComponent<Rigidbody>().AddForce(force * 5);
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -rb.transform.up, distToGround);
    }
}

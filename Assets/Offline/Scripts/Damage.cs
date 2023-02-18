using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Animator anim;
    public float cooldown;
    public Rigidbody rb;
    public GameObject DazedCanvas, DazeCanvasFill;
    public AudioClip stunSound;
    private AudioSource source;
    public Image DazedCanvasFill;

    public float cooltime = 2f, lifetime;
    public bool isStunned = false;
    private bool isInFlagArea = false;

    void Start()
    {
        DazedCanvas = GameObject.FindGameObjectWithTag("dazed");
        DazeCanvasFill = GameObject.FindGameObjectWithTag("dazedFill");
        DazedCanvasFill = GameObject.FindGameObjectWithTag("dazedFill").GetComponentInChildren<Image>();
        DazedCanvas.SetActive(false);
        DazeCanvasFill.SetActive(false);

        lifetime = Time.time + 12f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.time < cooldown && isStunned == true)
        {
            DazedCanvasFill.fillAmount += 1 / cooltime * Time.deltaTime;
        }
        else if (isStunned == true && Time.time > cooldown)
        {
            if (!GetComponent<Death_O>().isDead)
            {
                GetComponent<MoveRB_O>().enabled = true;
                GetComponent<Fire_O>().enabled = true;
            }

            isStunned = false;
            this.GetComponent<Rigidbody>().drag = 6; // to do, kolla om under flaggan, sätt inte till 6 då
            DazedCanvasFill.fillAmount = 0;
            DazedCanvas.SetActive(false);
            DazeCanvasFill.SetActive(false);
            anim.SetTrigger("NotDazed");
            Debug.Log("NotDazed");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            stunPlayer();
        }
    }

    public bool IsGroundedTwo()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1);
    }

    void stunPlayer ()
    {
        cooldown = Time.time + cooltime;
        isStunned = true;
        DazedCanvas.SetActive(true);
        DazeCanvasFill.SetActive(true);
        GetComponent<MoveRB_O>().enabled = false;
        GetComponent<Fire_O>().enabled = false;
        this.GetComponent<Rigidbody>().drag = 1;
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 120, ForceMode.Impulse);
        source.PlayOneShot(stunSound, 1f);
        anim.SetTrigger("dazed");
        Debug.Log("dazed");
    }
    public void setWithFlagArea(bool isWithin)
    {
        isInFlagArea = isWithin;
    }
}

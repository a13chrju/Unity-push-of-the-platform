using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire_O : MonoBehaviour {


    // Use this for initialization
    public GameObject fireball_prefab;
    public Transform transforms;
    public Animator anim;
    public float shotdelay, fireballspeed;

    public float cooldown;
    private bool active = false;

    public bool canshoot = false;
    public Rigidbody rb;
    public GameObject aim_reference;
    public AudioClip FireballSound;
    private AudioSource source;
    private Camera myCamera;
    public bool isgliding = false;
    public Button fireButton;
    //non local
    // public Image fire_icon;

    //local
    public Image localfire_icon;

    public float cooltime = 2f, lifetime;
    public GameObject fireball;

    public bool canfire = true;

    void Start()
    {
        localfire_icon = GameObject.Find("localfireimage").GetComponent<Image>();

        lifetime = Time.time + 12f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
       // this.GetComponentInChildren<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Time.time < cooldown && canfire == false)
        {
            localfire_icon.fillAmount += 1 / 2f * Time.deltaTime;
        }
        else if (canfire == false && Time.time > cooldown)
        {
            canfire = true;
            localfire_icon.fillAmount = 0;
        }

        if (Input.GetMouseButtonDown(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("fireball") && canfire == true)
        {
            Debug.Log("SHOT");
            source.PlayOneShot(FireballSound, 1f);
            cooldown = Time.time + 2f;

            Cmdshot();
            anim.SetTrigger("fireball");
            canfire = false;

        }
    }

    public void Cmdshot()
    {
        Vector3 playerpos = transform.position;
        playerpos.y = playerpos.y + 2f;

        Vector3 playerDir2 = this.transform.forward;
        Quaternion playerRot = transform.transform.rotation;
        Vector3 posfire = playerpos + playerDir2 * 10;

        fireball = (GameObject)Instantiate(fireball_prefab, posfire, transform.rotation) as GameObject;

        //fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * fireballspeed;
       // fireball.GetComponent<Bullet_O>().direction = rotation;
       // fireball.transform.rotation = myCamera.transform.rotation;
    }
}

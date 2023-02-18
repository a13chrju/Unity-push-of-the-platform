using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoost_O : MonoBehaviour
{
    public bool isgliding = false;
    public Image SpeedBoost_icon;
    public float cooltime = 10f;
    public GameObject Particles_prefab;
    public AudioClip SpeedBoostSound;
    private AudioSource source;
    public Animator anim;

    private float cooldown;

    public bool canfire = true;

    private Camera myCamera;
    private float ZoomTime;
    private bool zoomCamera = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        myCamera = this.GetComponentInChildren<Camera>();
        SpeedBoost_icon.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (isgliding == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                isgliding = false;
            }
        }

    }

    void LateUpdate()
    {

        if (Time.time < cooldown && canfire == false)
        {
            SpeedBoost_icon.fillAmount += 1 / cooltime * Time.deltaTime;

        }
        else if (canfire == false && Time.time > cooldown)
        {
            canfire = true;
            SpeedBoost_icon.fillAmount = 0;
        }

        if (Input.GetKeyDown(KeyCode.C) && canfire == true)
        {
            source.PlayOneShot(SpeedBoostSound, 0.3f);
            this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 200, ForceMode.Impulse);
            this.isgliding = true;
            Debug.Log("SHOT");
            cooldown = Time.time + cooltime;
            ZoomTime = Time.time + 2;
            Vector3 playerpos = transform.position;
            Particles_prefab = (GameObject)Instantiate(Particles_prefab, playerpos, transform.rotation) as GameObject;
            canfire = false;
            zoomCamera = true;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder_O : MonoBehaviour
{
    public GameObject placehodler;
    public GameObject wall;
    public Camera camera;
    public AudioClip ForceFieldSound;
    private AudioSource source;
    public float builderTime;
    public float cooldown;
    private Animator Animator;
    public bool switcher = false, canbuild = false;
    public Image builder_icon;
    // Use this for initialization
    void Start()
    {
        Animator = GetComponent<Animator>();
        camera = this.GetComponentInChildren<Camera>();
        cooldown = Time.time;
        source = GetComponent<AudioSource>();
        builder_icon.fillAmount = 0;
    }

    void Update()
    {
        if (Time.time < cooldown && canbuild == false && builder_icon.fillAmount < 1)
        {
            builder_icon.fillAmount += 1 / builderTime * Time.deltaTime;
        }
        else if (canbuild == false && Time.time > cooldown)
        {
            canbuild = true;
            builder_icon.fillAmount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            if (canbuild == true)
            {
                source.PlayOneShot(ForceFieldSound, 0.5f);
                Animator.SetTrigger("fireball");
                cooldown = Time.time + builderTime;
                Cmdbuild();
                canbuild = false;
            }
        }
    }

    public void Cmdbuild()
    {
        //Apply it to all other clients
        // RpcBuildwall();
        Vector3 playerpos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
        Vector3 playerDir = transform.transform.forward;

        //transform.rotation = Quaternion.LookRotation(camera.velocity);
        Vector3 v = transform.rotation.eulerAngles;
        Quaternion dsaotation = Quaternion.Euler(0, v.y - 0, v.z);

        Vector3 posfire = playerpos + playerDir * 10;
        GameObject Dwall = (GameObject)Instantiate(wall, posfire, dsaotation) as GameObject;
        // NetworkServer.Spawn(Dwall, this.gameObject);
    }
}

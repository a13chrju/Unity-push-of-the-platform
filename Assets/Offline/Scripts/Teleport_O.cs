using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport_O : MonoBehaviour
{
    public bool isgliding = false;
    private Rigidbody rb;
    public Image Tele_icon;
    public float cooltime = 10f;
    public GameObject PlaceholderModel_prefab, Teleparticles_prefab;
    private Vector3 PlaceHolderTransform;
    private AudioSource source;
    public AudioClip TeleSound;
    public Animator anim;
    private float cooldown = 0;
    public bool canfire = true;
    public bool canTeleport = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();

        canfire = true;
        canTeleport = false;
        Tele_icon.fillAmount = 0;
    }

    void LateUpdate()
    {

        if (Time.time < cooldown && canfire == false)
        {
            Tele_icon.fillAmount += 1 / cooltime * Time.deltaTime;
        }
        else if (canfire == false && Time.time > cooldown)
        {
            canfire = true;
            canTeleport = false;
            Tele_icon.fillAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.E) && canTeleport && !canfire)
        {
            // TELEPORT PLAYER TO STORED POSITION
            CmdSpawnTeleparticles();
            source.PlayOneShot(TeleSound, 0.4f);
            Debug.Log("weeeeeeeeeow");
            // source.PlayOneShot(SpeedBoostSound, 0.3f);
            canTeleport = false;
            rb.MovePosition(PlaceHolderTransform);
        }

        if (Input.GetKeyDown(KeyCode.E) && canfire)
        {
            // STORE PLAYER POSITION
            canTeleport = true;
            this.isgliding = true;
            PlaceHolderTransform = rb.position;
            Debug.Log("SHOT");
            cooldown = Time.time + cooltime;
            Cmdshot(transform.position);
            canfire = false;
        }
    }

    public void CmdSpawnTeleparticles()
    {
        Vector3 playerpos = transform.position;
        Teleparticles_prefab = (GameObject)Instantiate(Teleparticles_prefab, playerpos, transform.rotation) as GameObject;
    }

    public void Cmdshot(Vector3 position)
    {
        GameObject PlaceholderModel_prefab_new = (GameObject)Instantiate(PlaceholderModel_prefab, position, transform.rotation) as GameObject;
    }
}

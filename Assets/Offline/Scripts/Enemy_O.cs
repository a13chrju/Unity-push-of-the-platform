using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_O : MonoBehaviour
{
    // Use this for initialization
    public GameObject player;
    //public LineRenderer lr;
    public float timeshoot;
    private float shootDelay = 0;
    public float timeshootend;
    public float timedelay;
    public Vector3 position0;
    public Vector3 position1;
    public float turnspeed;
    public GameObject bullet;
    public Quaternion fireat;

    public bool followTarget;
    public float bulletSpeed;

    public bool enabled = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //  lr.enabled = false;
        position0 = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        position1 = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;
        if (followTarget) position1 = player.transform.position;
        //position1 = player.transform.position;
        Vector3 relativePos = new Vector3(position1.x, position1.y + 1f, position1.z) - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, toRotation, Time.deltaTime * turnspeed);
        //transform.LookAt(player.transform.position);




        // transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * turnspeed);
        if (Time.time > timedelay) // save the position of where the player is.
        {
            timedelay = Time.time + 3f;
            position1 = player.transform.position;
        }
        if (Time.time > shootDelay)
        {

            //   lr.enabled = true;
            shootDelay = Time.time + timeshoot;
            // lr.SetPosition(0, this.transform.position);
            // lr.SetPosition(1, position1);

            GameObject newg = Instantiate(bullet, transform.position, transform.rotation);

            if (newg != null && bulletSpeed > 0) newg.GetComponent<Bullet_O>().speed = bulletSpeed;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_O : MonoBehaviour {

    public float speed;
    public GameObject collidedwith, impact;
    public AudioClip explodeSound, hitplayerSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        // fiskar = gameObject.GetComponent<CharacterController>();

        /* if (isLocalPlayer)
         {
             Destroy(this);
             return;
         }*/
        this.GetComponent<Rigidbody>().velocity = (this.transform.forward) * speed;
        StartCoroutine(destroydelay());
    }


    private void OnCollisionEnter(Collision collision)
    {
        source.PlayOneShot(explodeSound, 1f);
        if (collision.collider.tag == "friendly")
        {
            GameObject balstimpact = (GameObject)Instantiate(impact, collision.collider.gameObject.transform.position, this.transform.rotation) as GameObject;
        }

        if (collision.collider.tag == "Player")
        {
            Debug.Log("DIE FOOL");
           // collision.collider.GetComponent<Mover>().ragdollify();
        }

        if (collision.collider.tag != "forcefield")
        {
            hitExplode();
        }
    }

    public void hitExplode()
    {
        Vector3 playerpos = transform.position;
        // GameObject fireball = (GameObject)Instantiate(fireball_prefab, posfire, this.GetComponentInChildren<Camera>().transform.rotation) as GameObject;
        GameObject balstimpact = (GameObject)Instantiate(impact, playerpos, this.transform.rotation) as GameObject;
        //StartCoroutine(destroydelay());
        Destroy(this.gameObject);
    }

    IEnumerator destroydelay()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(hitplayerSound, 0.6f);
            }
        }
    }
}

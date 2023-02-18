using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private bool canCollide = true;
    private float time;
    public GameObject barr;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Time.time > time)
        {
            canCollide = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Debug.Log("run");
            if (canCollide)
            {
                anim.SetTrigger("hit");
                time = Time.time + 2f;
                canCollide = !canCollide;
                Instantiate(barr, this.transform.position, Quaternion.identity);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    // Start is called before the first frame update
    public int team = 0; //0: neutral, 1: red, 2: blue
    public float value = 0;
    public GameObject flagImage;
    public Sprite blue, red;
    private Image flag;
    public int countDownTime;
    public List<Collider> onPoint = new List<Collider>();
    public List<Collider> GetColliders() { return onPoint; }

    public int teamRoundWinner = 0;
    public GameObject capturingParticles;

    private bool capturing = false;

    private List<string> tags = new List<string>() { "string1", "hello", "world" };
    void Start()
    {
        flag = flagImage.GetComponent<Image>();
        StartCoroutine(setNotCapturing());

        string apamo = "hello";

        if (tags.Contains(this.gameObject.tag))
        {
            Debug.Log("yes, its there");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (capturing)
        {
           // Debug.Log(onPoint.Count); 
            if (onPoint.Count > 0 && onPoint.TrueForAll(i => i.GetComponent<Health>().team.Equals(team))) {
                flag.fillAmount += 1f / countDownTime * Time.deltaTime;

                if (flag.fillAmount == 1) // score
                {
                    teamRoundWinner = team;
                }

                capturingParticles.GetComponent<ParticleSystem>().emissionRate = 0.53f;
            }
        } else
        {
            capturingParticles.GetComponent<ParticleSystem>().emissionRate = 0f;
        }


        if (!capturing && flag.fillAmount > 0) flag.fillAmount -= 1f / (countDownTime / 2) * Time.deltaTime;
    }

    IEnumerator setNotCapturing()
    {
        yield return new WaitForSeconds(2);
        if (onPoint.TrueForAll(i => !i.GetComponent<Health>().team.Equals(team)))
        {
            capturing = false;
        }
        StartCoroutine(setNotCapturing());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!onPoint.Contains(other))
            {
                onPoint.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPoint.Remove(other);
        }
    }

    public void reset()
    {
        teamRoundWinner = 0;
        team = 0;
        flag.fillAmount = 0;
        capturing = false;
    }

    public void listAllOnPoint ()
    {
        foreach (var item in onPoint)
        {
            Debug.Log("item");
            Debug.Log(item);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (flag.fillAmount <= 0 && other.gameObject.tag == "Player")
        {
            team = other.gameObject.GetComponent<Health>().team;
            if (team == 1) flag.GetComponent<Image>().sprite = red;
            else flag.GetComponent<Image>().sprite = blue;
            capturing = true;
        }
    }
}

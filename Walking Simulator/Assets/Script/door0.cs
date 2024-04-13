using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Text;
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public GameObject TrackOpen;
    public AudioSource DoorOpenSound;
    public bool Action = false;

    void Start()
    {
        Text.SetActive(false);
        ThisTrigger.GetComponent<BoxCollider>().enabled = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            Text.SetActive(true);
            Action = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Text.SetActive(false);
        Action = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Text.SetActive(false);
                AnimeObject.GetComponent<Animator>().Play("DoorOpen");
                ThisTrigger.GetComponent<BoxCollider>().enabled = false;
                DoorOpenSound.Play();
                Action = false;
            }
        }
    }
     
}

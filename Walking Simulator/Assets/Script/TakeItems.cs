using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItems : MonoBehaviour
{
    public GameObject Items;
    public Transform ItemsParent;
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        Text.SetActive(false);
        Items.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))  // Corrected from Getkey to GetKey
        {
            Drop();
            Text.SetActive(false);
        }
    }

    void Drop()
    {
        ItemsParent.DetachChildren();
        Items.transform.eulerAngles = new Vector3(0, 0, 0);  // Assuming you want to reset rotation
        Items.GetComponent<Rigidbody>().isKinematic = false;
        Items.GetComponent<MeshCollider>().enabled = true;
    }

    void Take()
    {   
        
        Items.GetComponent<Rigidbody>().isKinematic = true;

        Items.transform.position = ItemsParent.transform.position;
        Items.transform.rotation = ItemsParent.transform.rotation;

        Items.GetComponent<MeshCollider>().enabled = false;

        Items.transform.SetParent(ItemsParent);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))  // Corrected from Getkey to GetKey
            {   
                Text.SetActive(true);
                Take();
            } 
        }
    }
}

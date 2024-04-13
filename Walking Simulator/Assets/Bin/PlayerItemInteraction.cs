using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    public GameObject Items;
    public Transform ItemsParent;

    void Start()
    {
        Items.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Drop();
        }
    }

    void Drop()
    {
        ItemsParent.DetachChildren();
        Items.transform.eulerAngles = new Vector3(0, 0, 0);
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
            if (Input.GetKey(KeyCode.E))
            {
                Take();
            }
        }
    }
}

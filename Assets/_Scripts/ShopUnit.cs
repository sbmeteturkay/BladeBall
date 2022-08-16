using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUnit : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "NPCTriggerCollider")
            shopPanel.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name== "NPCTriggerCollider")
            shopPanel.SetActive(false);
    }
}

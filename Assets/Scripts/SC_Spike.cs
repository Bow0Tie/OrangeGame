using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Spike : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    
    void OnTriggerEnter2D(Collider2D wasEntered)
    {
        if (wasEntered.gameObject.CompareTag("Player"))
        {
            wasEntered.gameObject.GetComponent<SC_LiveControl>().End("lose");
        }
    }
}

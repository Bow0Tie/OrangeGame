using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Coin : MonoBehaviour
{
    public static int CoinsCount = 0; 

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D wasEntered)
    {
        if (wasEntered.gameObject.CompareTag("Player"))
        {
            CoinsCount++;
            Destroy(gameObject);
        }
    }
}

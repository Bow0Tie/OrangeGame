using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CoinManager : MonoBehaviour
{
    private static Text _counterText;
    
    void Start()
    {
        _counterText = GetComponent<Text>();
    }
    
    void Update()
    {
        if(_counterText.text != SC_Coin.CoinsCount.ToString())
        {
            _counterText.text = SC_Coin.CoinsCount.ToString();
        }
    }
}
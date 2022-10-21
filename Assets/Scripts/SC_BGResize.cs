using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BGResize : MonoBehaviour
{
    private int _height;
    private int _weight;
    
    void Start()
    {
        _height = Screen.height;
        _weight = Screen.width;
        gameObject.transform.localScale = new Vector3(_weight, _height, 0f);
    }
}

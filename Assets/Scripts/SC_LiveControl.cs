using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_LiveControl : MonoBehaviour
{
    [SerializeField] private Canvas defeatWidget;
    [SerializeField] private Canvas winWidget;
    private Rigidbody2D _rigidbody2D;
    private Button _restartButtonLose;
    private Button _restartButtonWin;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _restartButtonLose = defeatWidget.GetComponentInChildren<Button>();
        _restartButtonWin = winWidget.GetComponentInChildren<Button>();
        _restartButtonLose.onClick.AddListener(Restart);
        _restartButtonWin.onClick.AddListener(Restart);
    }

    public void End(string type)
    {
        switch (type)
        {
            case "win":
                winWidget.gameObject.SetActive(true);
                break;
            case "lose":
                defeatWidget.gameObject.SetActive(true);
                break;
        }
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SC_Coin.CoinsCount = 0;
    }
}

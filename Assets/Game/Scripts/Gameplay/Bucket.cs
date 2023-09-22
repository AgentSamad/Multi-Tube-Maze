using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private GameObject bucketFx;

    private int currentBalls, requiredBalls;
    private bool isFinish;

    void Start()
    {
        currentBalls = 0;
        requiredBalls = LevelManager.instance.GetLevelTarget();
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetText()
    {
        _textMeshPro.text = currentBalls + "/" + requiredBalls;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            currentBalls++;
            SetText();
            bucketFx.SetActive(true);
            CheckLevelComplete();
        }
    }

    void CheckLevelComplete()
    {
        if (currentBalls >= requiredBalls && !isFinish)
        {
            isFinish = true;
            GameManager.changeGameState.Invoke(GameState.Win);
        }
    }
}
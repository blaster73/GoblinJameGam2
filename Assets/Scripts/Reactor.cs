using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reactor : MonoBehaviour
{

    public Manager manager;
    public int roundsRequired = 10;
    private int currentRounds = 0;
    public bool inGameplay = false;
    public float timerMax;
    public float timerMin;
    public int reactMax;
    public int reactMin;

    public int incomingLevel;
    public float timer;
    public TMP_Text timerText;
    public TMP_Text repairsLeftText;


    void Start() {
        GenerateNew();
    }

    void Update()
    {
        if(inGameplay)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                GenerateNew();
            }
        }

        timerText.text = timer.ToString("F0");
    }

    private void GenerateNew()
    {
        currentRounds++;
        manager.speed = manager.speed + 0.15f;
        if(currentRounds == roundsRequired)
        {
            manager.Win();
        }

        manager.reactorLevel = incomingLevel;
        incomingLevel = Random.Range(reactMin, reactMax + 1);
        repairsLeftText.text = (roundsRequired - currentRounds).ToString();
        timer = Random.Range(timerMin, timerMax);        
    }
}

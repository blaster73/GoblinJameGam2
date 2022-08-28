using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using DG;

public class Slot : MonoBehaviour
{

    public bool inCurrent = false;
    private bool previousCurrent = false;
    public TMP_Text powText;
    public Image currentLine;
    public Image border;
    public Color notColor;
    public Color currentColor;
    public RectTransform gameScreen;
    public Camera mainCamera;

    public Manager manager;

    public AudioSource audioSource;
    public AudioSource click;

    [Header("Power Settings")]
    public int maxPow = 3;
    public int minPow = -3;
    public int startPow = 0;
    public int pow = 1;

    private void Start() {
        previousCurrent = inCurrent;
        SetCurrent(inCurrent);
        pow = startPow;
        powText.text = pow.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetCurrent(bool newCurrent)
    {
        if(newCurrent != inCurrent)
        {
            SwapCurrent(newCurrent);
        }
    }

    public void SwapCurrent(bool newInCurrent)
    {

        audioSource.Play();

        if(!inCurrent)
        {
            inCurrent = true;
            pow = (pow * -1);
            powText.text = pow.ToString();
            border.color = currentColor;
            currentLine.color = currentColor;
        }
        else
        {
            inCurrent = false;
            currentLine.color = notColor;
            border.color = notColor;
        }

        gameScreen.DOShakePosition(0.25f, 6, 30);
        //mainCamera.DOShakePosition(0.4f, 0.1f, 8);
        StartCoroutine(manager.DoPause());
        
    }

    public void AddPow()
    {
        if(pow < maxPow)
        {
            pow++;
            if(pow == 0)
            {
                pow = 1;
            }
            click.Play();
            powText.text = pow.ToString();
        }
    }

    public void MinPow()
    {
        if(pow > minPow)
        {
            pow--;
            if(pow == 0)
            {
                pow = -1;
            }
            click.Play();
            powText.text = pow.ToString();
        }
    }

}

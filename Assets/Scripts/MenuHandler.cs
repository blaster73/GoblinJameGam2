using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

    public GameObject mainGame;
    public GameObject menu;
    public GameObject startScreen;
    public GameObject howToPlay;

    public void StartGame()
    {
        menu.SetActive(false);
        mainGame.SetActive(true);
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        startScreen.SetActive(false);
    }

    public void BackToMenu()
    {
        howToPlay.SetActive(false);
        startScreen.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{

    public GameObject[] screens;
    public int index = 0;

    public void NextScreen()
    {
        index++;
        for(int i = 0; i < screens.Length; i++)
        {
            if(index == i)
            {
                screens[i].SetActive(true);
            }
            else{
                screens[i].SetActive(false);
            }
        }
    }



}

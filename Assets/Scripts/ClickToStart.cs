using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour
{

    public GameObject thingToDisable;
    public GameObject thingToEnable;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            thingToEnable.SetActive(true);
            thingToDisable.SetActive(false);
        }
    }
}

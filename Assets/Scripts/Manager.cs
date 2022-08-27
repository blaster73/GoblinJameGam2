using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum OvalState {Shrink, Enlarge, Pause};

    public RectTransform oval;
    private Vector2 startingSize;
    public float speed = 1;
    public OvalState direction;
    private OvalState prevDirection;
    public Vector2[] slotActivations;
    public float[] slotCheckpoints;
    
    
    private Vector2 targetSize;
    private Vector2 velocity = Vector2.zero;
    private float time = 0;


    // Start is called before the first frame update
    void Start()
    {
        startingSize.x = oval.rect.width;
        startingSize.y = oval.rect.height;
    }

    // Update is called once per frame
    void Update()
    {

        switch(direction)
        {
            case OvalState.Shrink:
                if(prevDirection != direction)
                {
                    targetSize = new Vector2(0, 0);
                    startingSize = oval.rect.size;
                    prevDirection = direction;
                    time = 0;
                }
                break;
            case OvalState.Enlarge:
                if(prevDirection != direction)
                {
                    targetSize= new Vector2(1024, 610);
                    startingSize = oval.rect.size;
                    prevDirection = direction;
                    time = 0;
                }
                break;
            case OvalState.Pause:
                if(prevDirection != direction)
                {
                    startingSize = oval.rect.size;
                    targetSize = startingSize;
                    prevDirection = direction;
                    time = 0;
                }
                break;
        }


        oval.sizeDelta = Vector2.Lerp(startingSize, targetSize, time / speed);
        time += Time.deltaTime;
    }
}

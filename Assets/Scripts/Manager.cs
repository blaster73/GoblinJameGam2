using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum OvalState {Shrink, Enlarge, Pause};

    public RectTransform oval;
    public Vector2 startingSize;
    public float speed = 1;
    public OvalState direction;
    private OvalState prevDirection;
    
    private Vector2 targetSize;
    private Vector2 velocity = Vector2.zero;


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
                targetSize = new Vector2(0, 0);
                startingSize = oval.rect.size;
                break;
            case OvalState.Enlarge:
                targetSize= new Vector2(1024, 610);
                startingSize = oval.rect.size;
                break;
            case OvalState.Pause:
                startingSize = oval.rect.size;
                targetSize = startingSize;
                break;
        }

        oval.sizeDelta = Vector2.Lerp(startingSize, targetSize, speed);
    }
}

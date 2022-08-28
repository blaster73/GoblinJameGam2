using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public enum OvalState {Shrink, Enlarge, Pause};

    public RectTransform oval;
    public float speed = 1;
    public float hitPauseTime = 0.25f;
    public OvalState direction;
    public float[] slotCheckpoints;
    public Slot[] slots;

    [Header("Flow")]
    public int incoming;
    public TMP_Text incomingText;
    public int outgoing;
    public TMP_Text outgoingText;

    private Vector2 startingSize;
    private OvalState prevDirection;
    private Vector2 targetSize;
    private Vector2 velocity = Vector2.zero;
    private float time = 0;

    private bool crossedCheckpoint;
    private int currentCheckpoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        startingSize.x = oval.rect.width;
        startingSize.y = oval.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        // Direction Handling
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

        oval.sizeDelta = Vector2.Lerp(startingSize, targetSize, time / 5);
        time += (Time.deltaTime * speed);

        // Update current checkpoint
        if(oval.rect.width >= slotCheckpoints[1]){
            slots[0].SetCurrent(true);
        } else {
            slots[0].SetCurrent(false);
        }

        if(oval.rect.width >= slotCheckpoints[2]){
            slots[1].SetCurrent(true);
        } else {
            slots[1].SetCurrent(false);
        }

        if(oval.rect.width >= slotCheckpoints[3]){
            slots[2].SetCurrent(true);
        } else {
            slots[2].SetCurrent(false);
        }

        if(oval.rect.width >= slotCheckpoints[4]){
            slots[3].SetCurrent(true);
        } else {
            slots[3].SetCurrent(false);
        }

        if(oval.rect.width >= slotCheckpoints[5]){
            slots[4].SetCurrent(true);
        } else {
            slots[4].SetCurrent(false);
        }

        int newOutgoing = 0;
        foreach(Slot s in slots)
        {
            if(s.inCurrent)
            {
                newOutgoing += s.pow;
            }
        }
        outgoing = newOutgoing + 1;
        outgoingText.text = outgoing.ToString();

        if(incoming > outgoing)
        {
            direction = OvalState.Shrink;
        }
        if(incoming < outgoing)
        {
            direction = OvalState.Enlarge;
        }
        if(incoming == outgoing)
        {
            direction = OvalState.Pause;
        }


    }

    public IEnumerator DoPause()
    {
        OvalState old = direction;
        float duration = hitPauseTime;
        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            direction = OvalState.Pause;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        direction = old;
    }

}

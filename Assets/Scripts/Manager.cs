using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int reactorLevel;
    public TMP_Text reactorText;
    public int coolingLevel;
    public TMP_Text coolingText;

    private Vector2 startingSize;
    private OvalState prevDirection;
    private Vector2 targetSize;
    private Vector2 velocity = Vector2.zero;
    private float time = 0;

    [Header("Game States")]
    public bool ignoreWinLose = false;
    public GameObject gameScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject music;


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

        // Check for losing
        if(!ignoreWinLose)
        {
            if(oval.rect.width <= slotCheckpoints[0] || oval.rect.width >= slotCheckpoints[6])
            {
                loseScreen.SetActive(true);
                StartCoroutine(ResetGame());
                music.SetActive(false);
            }
        }

        int newOutgoing = 0;
        foreach(Slot s in slots)
        {
            /*if(s.inCurrent)
            {
                newOutgoing += s.pow;
            }*/
            newOutgoing += s.pow;
        }
        coolingLevel = newOutgoing + 1;

        coolingText.text = coolingLevel.ToString();
        reactorText.text = reactorLevel.ToString();

        if(reactorLevel > coolingLevel)
        {
            direction = OvalState.Shrink;
        }
        if(reactorLevel < coolingLevel)
        {
            direction = OvalState.Enlarge;
        }
        if(reactorLevel == coolingLevel)
        {
            direction = OvalState.Pause;
        }

    }

    public void Win()
    {
        if(!ignoreWinLose)
        {
            gameScreen.SetActive(false);
            winScreen.SetActive(true);
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

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    public void ResetGameButton()
    {
        SceneManager.LoadScene(0);
    }

}

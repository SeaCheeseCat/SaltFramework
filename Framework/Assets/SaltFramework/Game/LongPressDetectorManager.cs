using System;
using UnityEngine;

public class LongPressDetectorManager : SingleMono<LongPressDetectorManager>
{
    private bool runOpen = false;
    private KeyCode currentKey;
    private float pressDuration = 1f;
    private float pressTimer;
    private bool isPressed;
    private Action currentAction;
    private Action<float> updateAction;
    private Action destoryAction;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!runOpen)
            return;
        if (Input.GetKey(currentKey))
        {
            if (!isPressed)
            {
                isPressed = true;
                pressTimer = 0f;
            }
            else
            {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressDuration)
                {
                    ExecuteAction();
                }
                updateAction?.Invoke(pressTimer);
            }
        }
        else
        {
            if (isPressed)
            {
                isPressed = false;
                destoryAction?.Invoke();
            }
        }
    }

    private void ExecuteAction()
    {
        currentAction?.Invoke();
        currentAction = null;
    }




    public void CheckLongPress(KeyCode key, float pressDuration, Action triggerAction , Action<float> updateAction, Action destroyAction)
    {
        if (Input.GetKeyDown(key))
        {
            runOpen = true;
            currentKey = key;
            currentAction = triggerAction;
            this.updateAction = updateAction;
            this.destoryAction = destroyAction;
            this.pressDuration = pressDuration;
        }  
    }

    public void Recycle()
    {
        runOpen = false; 
        isPressed = false;
        currentAction = null;
        this.updateAction = null;
        this.destoryAction = null;
    }
    
}

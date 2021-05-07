using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public class MonoBehaviorHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }

    private Action action;
    private float timer;
    private bool isDestroyed;

    public Timer(Action action, float timer)
    {
        this.action = action;
        this.timer = timer;
        isDestroyed = false;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                action();
                StopTimer();
            }
        }
    }

    private void StopTimer()
    {
        isDestroyed = true;
    }
}

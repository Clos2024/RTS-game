using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UtilTimer
{
    public static GameObject Create(Action action, float timer)
    {
        GameObject gameObject = new GameObject("UtilTimer", typeof(MonoBehaviorHook));
        UtilTimer utilTimer = new UtilTimer(action, timer, gameObject);
        gameObject.GetComponent<MonoBehaviorHook>().onUpdate = utilTimer.Update;

        return gameObject;
    }

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
    private string timerName;
    private GameObject gameObject;
    private bool isDestroyed;
    public bool start;

    private UtilTimer(Action action, float timer, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
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
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
    }
}

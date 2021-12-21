using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnGameWin = new UnityEvent(); 
    public static UnityEvent OnGameLose = new UnityEvent();
    public static UnityEvent OnLevelStarted = new UnityEvent();
    public static UnityEvent OnLevelFinished = new UnityEvent();
    public static UnityEvent OnBasketScore = new UnityEvent();
}

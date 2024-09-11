using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleMono<GameManager>
{
    public GameScreen gameScreen;

    /// <summary>
    /// Base:Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnGameStart(int chapter, int level)
    {
    }
}

public enum GameScreen
{ 
    Start,
    Game
}


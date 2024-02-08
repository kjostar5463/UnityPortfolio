using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int score;
    public bool isDead = false;

    private void Awake()
    {
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isDead);
        if(isDead)
            OnPlayerDead();
    }

    void OnPauseGame()
    {

    }

    void OnPlayerDead()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnClearGame()
    {

    }
}

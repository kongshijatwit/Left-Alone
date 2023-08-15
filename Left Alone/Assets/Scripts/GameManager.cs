using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnGameOver = delegate { };

    private void OnEnable() 
    {
        Monster.OnCaughtPlayer += OnGameOver;
    }

    private void OnDisable() 
    {
        Monster.OnCaughtPlayer -= OnGameOver;
    }
}

﻿using UnityEngine;
using System.Collections;

public class CarryOverInfo : MonoBehaviour {

    public static int LevelNumber;
    public static bool DemonControlRound;
    public static int StartLives;
    public static float GameTimerFloat;

    void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
}

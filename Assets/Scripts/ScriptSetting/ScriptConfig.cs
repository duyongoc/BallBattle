﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CONFIG", menuName = "GAME/CONFIG")]
public class ScriptConfig : ScriptableObject
{
    [Header("Set limit time")]
    public float timeLimit = 140;
    
    [Header("Ball Speed")]
    public float ballSpeed = 1.5f;

}

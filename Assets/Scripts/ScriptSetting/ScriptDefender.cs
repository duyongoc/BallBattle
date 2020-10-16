using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defender", menuName = "GAME/Defender")]
public class ScriptDefender: ScriptableObject
{
    [Header("Energy regeneration")]
    public float energyRegen = 0.5f;

    [Header("Energy cost")]
    public float energyCost = 2;

    [Header("Spawn time")]
    public float spawnTime = 0.5f;

    [Header("Reactivate Time")]
    public float reactivateTime = 4f;
    
    [Header("Normal Speed")]
    public float normalSpeed = 1.5f;

    [Header("Carrying Speed")]
    public float carryingSpeed = 0.75f;

    [Header("Return Speed")]
    public float returnSpeed = 2f;

    [Header("Detection Range")]
    public float detectionRange = 35f;
    


}

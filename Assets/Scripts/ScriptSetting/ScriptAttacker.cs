using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacker", menuName = "GAME/Attacker")]
public class ScriptAttacker: ScriptableObject
{

    [Header("Energy regeneration")]
    public float energyRegen = 0.5f;

    [Header("Energy cost")]
    public float energyAttackerCost = 2;

    [Header("Spawn time")]
    public float spawnTime = 0.5f;

    [Header("Reactivate Time")]
    public float reactivateTime = 4f;
    
    [Header("Normal Speed")]
    public float normalSpeed = 1.5f;

    [Header("Carrying Speed")]
    public float carryingSpeed = 0.75f;

}

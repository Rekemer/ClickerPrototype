using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DifficultyEnemySettings")]
//allow us to tweak parameters of enemies to change difficulty as time passes
public class DifficultyEnemySettings : ScriptableObject
{
    public AnimationCurve difficultyHealthCurve;
    public AnimationCurve difficultySpeedCurve;
    public float timeOfWaitingAfterReachingNewPos ;
}

[CreateAssetMenu(menuName = "ScriptableObjects/DifficultySpawnSettings")]
//allow us to change speed pf spawning
public class DifficultySpawnSettings : ScriptableObject
{
    public AnimationCurve difficultySpawnCurve;
}


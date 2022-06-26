using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/DifficultySpawnSettings")]
//allow us to change speed pf spawning
public class DifficultySpawnSettings : ScriptableObject
{
    public AnimationCurve difficultySpawnCurve;
}
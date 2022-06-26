using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WaveDifficultySpawnSettings")]
public class WaveDifficultySpawnSettings : ScriptableObject
{
    public List<Difficulty> difficulties;
    public float timeBetweenSpawns;
    public float amountOfEnemies;
}
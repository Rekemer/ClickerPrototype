using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WaveDifficultySpawnSettings")]
    public class WaveDifficultySpawnSettings : ScriptableObject
    {
        public List<Difficulty> difficulties;
        public float timeBetweenSpawns;
        public List<int> amountOfEnemiesPerWave;
    }
}
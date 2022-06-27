using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DefaultDifficultySpawnSettings")]
    public class DefaultDifficultySpawnSettings : ScriptableObject
    {
        public List<Difficulty> difficulties;
        public float timeBetweenSpawns;
        [Range(0.7f, 1f)] public float timeBetweenSpawnsMultiplier;
    }
}
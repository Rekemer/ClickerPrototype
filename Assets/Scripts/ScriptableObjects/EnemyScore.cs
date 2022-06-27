using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemyScore")]
    public class EnemyScore : ScriptableObject
    {
        public int score;
    }
}
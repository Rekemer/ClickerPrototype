using System;

namespace ScriptableObjects
{
    [Serializable]
    public struct Difficulty
    {
        public int time;
        public int enemyHealth;
        public int enemySpeed;
        public float timeOfWaitingAfretReachingNewPos;
    }
}
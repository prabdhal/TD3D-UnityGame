using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "EnemyLogInfo")]
    public class EnemyLogInfo : ScriptableObject
    {
        public Sprite icon;
        public string name;
        public EnemySpeed enemySpeed;
        public Immunities immunity;
        public int health;
        public int armour;
        public int magicResist;
        public int goldValue;
    }
}
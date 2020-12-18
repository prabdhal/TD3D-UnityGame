using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Towers")]
    public class TowerData : ScriptableObject
    {
        [Header("Tower Info")]
        public GameObject towerPrefab;
        public Sprite icon;
        public string towerName;
        public int cost;
        public int sellValue;

        [Header("Combat Stats")]
        public Immunities towerType;
        public DamageType damageType;
        public float minDamage;
        public float maxDamage;
        public float fireRate;
        public float range;
        [TextArea(1,2)]
        public string description;
        [Header("Special Effect")]
        public List<Effect> effects = new List<Effect>(4);
        [Space]
        public TowerData upgrade01;
        public TowerData upgrade02;
    }
}

using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Effects/Effect")]
    public class Effect : ScriptableObject
    {
        public EffectType effectType;
        public DamageType damageType;
        public float effectValue;
        public float effectDuration;
    }
}
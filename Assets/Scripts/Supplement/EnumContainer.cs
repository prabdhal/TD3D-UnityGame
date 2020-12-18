using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class EnumContainer : MonoBehaviour { }

    public enum EffectType { None, DamageOverTime, Slow, ArmorPenetration, MagicPenetration, CriticalHitDamage, CriticalHitChance, SlowOverDuration }

    public enum EnemyType { Goblin, Wolf, Bee, Spiderling, Bat, Treant, Magma, Cobra, Golem, Dragon}

    public enum DamageType { Physical, Magical, True }

    public enum Immunities { None, Poison, Fire, Cannon, Bolt }

    public enum EnemySpeed { Slow, Normal, Fast };
    
}

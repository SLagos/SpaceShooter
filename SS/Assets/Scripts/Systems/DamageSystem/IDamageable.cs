using System.Collections;
using UnityEngine;

namespace Systems.DamageSystem
{
    public interface IDamageable
    {
        void ReceiveDamage(DamageInfo info);
    }
    public struct DamageInfo
    {
        public int Damage;       
    }
}
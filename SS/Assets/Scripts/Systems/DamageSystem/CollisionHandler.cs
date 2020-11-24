using UnityEngine;

namespace Systems.DamageSystem
{
    /// <summary>
    /// This class is responsible for notify the owner that a Collision occurs and also fire an event when a collision occurs.
    /// So this will communicate both parts of the collision event.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionHandler : MonoBehaviour
    {
        public delegate void CollisionEvent(Collision collisionData);
        public event CollisionEvent OnCollision;

        private IDamageable _owner;

        [SerializeField]
        private bool _isPlayer;
        public bool IsPlayer => _isPlayer;

        public void Init(IDamageable owner)
        {
            _owner = owner;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(OnCollision!= null)
            {
                OnCollision(collision);
            }
        }

        public void NotifyCollision(DamageInfo damageInfo)
        {
            if(_owner != null)
                _owner.ReceiveDamage(damageInfo);
        }
    }
}
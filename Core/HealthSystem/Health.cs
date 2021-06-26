using System;
using UnityEngine;

namespace Core.HealthSystem
{
    public class Health : MonoBehaviour
    {
        public event Action<Health> OnDeath;
        public event Action<Health> OnDamaged;
        public event Action<Health> OnHealed;

        [SerializeField] private int maxHp = 100;

        public int MaxHp
        {
            get => maxHp;
            set
            {
                maxHp = value;
                CurrentHp = maxHp;
                OnHealed?.Invoke(this);
            }
        }

        public int CurrentHp { get; private set; }

        public bool IsDead => CurrentHp <= 0;


        private void Awake()
        {
            CurrentHp = maxHp;
        }


        public void Damage(int damage)
        {
            if (IsDead)
            {
                return;
            }

            CurrentHp -= damage;
            CurrentHp = Mathf.Clamp(CurrentHp, 0, CurrentHp);

            if (IsDead)
            {
                OnDeath?.Invoke(this);
            }
            else
            {
                OnDamaged?.Invoke(this);
            }
        }

        public void Heal(int damage)
        {
            CurrentHp += damage;
            CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
            OnHealed?.Invoke(this);
        }
    }
}
using System;
using UnityEngine;
using Devdog.InventoryPro;

namespace NeoFPS.InvProIntegration
{
    [RequireComponent(typeof(InventoryPlayer))]
    public class NeoInventoryHealthManager : MonoBehaviour, IHealthManager
    {
        [SerializeField]
        private string m_StatCategory = "Character";

        [SerializeField]
        private string m_StatName = "Health";

        public event HealthDelegates.OnHealthChanged onHealthChanged;
        public event HealthDelegates.OnHealthMaxChanged onHealthMaxChanged;
        public event HealthDelegates.OnIsAliveChanged onIsAliveChanged;

        private InventoryPlayer m_InventoryPlayer;
        private IStat m_HealthStat;
        private float m_TrackedHealth;
        private bool m_Critical;
        private IDamageSource m_Source;

        void Start()
        {
            // Get player
            m_InventoryPlayer = GetComponent<InventoryPlayer>();

            // Get and initialise health stat
            m_HealthStat = m_InventoryPlayer.stats.Get(m_StatCategory, m_StatName);
            if (m_HealthStat == null)
                Debug.LogError(string.Format("Player health stat not found: {0}.{1}", m_StatCategory, m_StatName));
            else
            {
                m_TrackedHealth = m_HealthStat.currentValue;
                m_HealthStat.OnValueChanged += OnStatValueChanged;
            }
        }

        void OnStatValueChanged(IStat stat)
        {
            if (onHealthChanged != null)
                onHealthChanged(m_TrackedHealth, stat.currentValue, m_Critical, m_Source);

            // Update tracked health
            m_TrackedHealth = stat.currentValue;

            // Check if dead
            if (isAlive && stat.currentValue <= 0f)
                isAlive = false;

            // Reset values
            m_Critical = false;
            m_Source = null;
        }

        private bool m_IsAlive = true;
        public bool isAlive
        {
            get { return m_IsAlive; }
            private set
            {
                if (m_IsAlive != value)
                {
                    m_IsAlive = value;
                    if (onIsAliveChanged != null)
                        onIsAliveChanged(m_IsAlive);

                    // Tell the player to drop all items
                    if (!m_IsAlive)
                        m_InventoryPlayer.NotifyPlayerDied(true);
                }
            }
        }

        public float health
        {
            get
            {
                if (m_HealthStat == null)
                    return 100f;
                else
                    return m_HealthStat.currentValue;
            }
            set
            {
                if (value < 0f)
                    value = 0f;
                SetHealth(value, false, null);
            }
        }

        void SetHealth(float to, bool crit, IDamageSource source)
        {
            if (to < 0f)
                to = 0f;

            m_Critical = crit;
            m_Source = source;
            m_HealthStat.SetCurrentValueRaw(to);
        }

        public float healthMax
        {
            get { return m_HealthStat.currentMaxValue; }
            set
            {
                float old = m_HealthStat.currentMaxValue;
                if (old != value)
                {
                    // Set value
                    m_HealthStat.SetMaxValueRaw(Mathf.Max(0f, value), false);
                    float newValue = m_HealthStat.currentMaxValue;
                    // Fire event
                    if (onHealthMaxChanged != null)
                        onHealthMaxChanged(old, newValue);
                    // Check health is still valid
                    if (health > newValue)
                        health = newValue;
                }
            }
        }

        public float normalisedHealth
        {
            get { return health / healthMax; }
            set { health = healthMax * value; }
        }

        public void AddDamage(float damage)
        {
            AddDamage(damage, false, null);
        }

        public void AddDamage(float damage, bool critical)
        {
            AddDamage(damage, critical, null);
        }

        public void AddDamage(float damage, IDamageSource source)
        {
            AddDamage(damage, false, source);
        }

        public void AddDamage(float damage, bool critical, IDamageSource source)
        {
            SetHealth(health - damage, critical, source);
        }

        public void AddHealth(float h)
        {
            AddHealth(h, null);
        }

        public void AddHealth(float h, IDamageSource source)
        {
            SetHealth(health + h, false, source);
        }
    }
}

using Devdog.InventoryPro;
using NeoFPS.CharacterMotion;
using NeoFPS.CharacterMotion.MotionData;
using System;
using System.Collections;
using UnityEngine;

namespace NeoFPS.InvProIntegration
{
    [RequireComponent(typeof(MotionController))]
    [RequireComponent(typeof(InventoryPlayer))]
    public class NeoInventoryStatsMotionDataOverride : MonoBehaviour, IMotionGraphDataOverride
    {
        [Header("Speed")]

        [SerializeField, Tooltip("The category of the speed stat in the Inventory Pro player.")]
        private string m_SpeedStatCategory = "Character";
        [SerializeField, Tooltip("The name of the speed stat in the Inventory Pro player.")]
        private string m_SpeedStatName = "Speed";

        [SerializeField, Tooltip("All the data names that are affected by the speed stat. Their value will be multiplied by the stat percentage (value / 100).")]
        private string[] m_SpeedData = new string[]
            {
                "moveSpeedWalking",
                "moveSpeedWalkAiming",
                "moveSpeedSprinting",
                "moveSpeedSprintAiming",
                "moveSpeedCrouching",
                "ladderClimb",
                "ladderClimbFast"
            };

        [Header("Strength")]

        [SerializeField, Tooltip("The category of the strength stat in the Inventory Pro player.")]
        private string m_StrengthStatCategory = "Character";
        [SerializeField, Tooltip("The name of the strength stat in the Inventory Pro player.")]
        private string m_StrengthStatName = "Speed";

        [SerializeField, Tooltip("All the data names that are affected by the speed stat. Their value will be multiplied by the stat percentage (value / 100).")]
        private string[] m_StrengthData = new string[]
            {
                "minJumpHeight",
                "maxJumpHeight",
                "dodgeVertical",
                "dodgeHorizontal",
                "ladderJumpOff"
            };

        MotionController m_MotionController = null;
        InventoryPlayer m_InventoryPlayer = null;
        IStat m_SpeedStat;
        IStat m_StrengthStat;
        DataOverride[] speedOverrides;
        DataOverride[] strengthOverrides;

        struct DataOverride
        {
            public string name;
            public float defaultValue;
            public IStat stat;

            public float GetValue()
            {
                return defaultValue * (stat.currentValue / 100f);
            }
        }

        void Awake()
        {
            m_MotionController = GetComponent<MotionController>();
            m_InventoryPlayer = GetComponent<InventoryPlayer>();

            // Initialise speed overrides
            speedOverrides = new DataOverride[m_SpeedData.Length];
            for (int i = 0; i < speedOverrides.Length; ++i)
                speedOverrides[i] = new DataOverride { name = m_SpeedData[i] };

            // Initialise strength overrides
            strengthOverrides = new DataOverride[m_StrengthData.Length];
            for (int i = 0; i < strengthOverrides.Length; ++i)
                strengthOverrides[i] = new DataOverride { name = m_StrengthData[i] };
        }

        IEnumerator Start()
        {
            yield return null;
            m_SpeedStat = m_InventoryPlayer.stats.Get(m_SpeedStatCategory, m_SpeedStatName);
            m_StrengthStat = m_InventoryPlayer.stats.Get(m_StrengthStatCategory, m_StrengthStatName);
            m_MotionController.motionGraph.ApplyDataOverrides(this);
        }

        public Func<float> GetFloatOverride(FloatData data)
        {
            // Check if it's a registered speed data entry
            for (int i = 0; i < speedOverrides.Length; ++i)
            {
                if (speedOverrides[i].name == data.name)
                {
                    speedOverrides[i].defaultValue = data.value;
                    speedOverrides[i].stat = m_SpeedStat;
                    return speedOverrides[i].GetValue;
                }
            }

            // Check if it's a registered strength data entry
            for (int i = 0; i < strengthOverrides.Length; ++i)
            {
                if (strengthOverrides[i].name == data.name)
                {
                    strengthOverrides[i].defaultValue = data.value;
                    strengthOverrides[i].stat = m_StrengthStat;
                    return strengthOverrides[i].GetValue;
                }
            }

            return null;
        }

        public Func<bool> GetBoolOverride(BoolData data)
        {
            return null;
        }

        public Func<int> GetIntOverride(IntData data)
        {
            return null;
        }
    }
}

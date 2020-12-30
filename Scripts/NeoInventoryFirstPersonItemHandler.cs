using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryFirstPersonItemHandler : MonoBehaviour
    {
        [SerializeField, Tooltip("The item to use when none have been equipped through Inventory Pro (usually hands).")]
        private NeoInventoryFirstPersonItem m_FallbackPrototype = null;

        [SerializeField, Tooltip("The root transform to set as the parent for new items.")]
        private Transform m_ItemRoot = null;

        private List<NeoInventoryFirstPersonItem> m_Items = new List<NeoInventoryFirstPersonItem>(8);
        private NeoInventoryFirstPersonItem m_Fallback = null;

        private NeoInventoryFirstPersonItem m_Equipped;
        public NeoInventoryFirstPersonItem equipped
        {
            get { return m_Equipped; }
            set
            {
                if (m_Equipped != null)
                    m_Equipped.Unequip();

                if (value == null)
                    m_Equipped = m_Fallback;
                else
                    m_Equipped = value;

                if (m_Equipped != null)
                    m_Equipped.Equip();
            }
        }

        public Transform itemRoot
        {
            get
            {
                if (m_ItemRoot != null)
                    return m_ItemRoot;
                else
                    return transform;
            }
        }

        private void Start()
        {
            if (m_FallbackPrototype != null)
            {
                m_Fallback = Instantiate(m_FallbackPrototype, Vector3.zero, Quaternion.identity, itemRoot);
                m_Fallback.Attach(this, null);
                equipped = m_Fallback;
            }
        }

        public void EquipItem(NeoInventoryEquippableItem item)
        {
            // Do nothing if already equipped
            if (equipped != null && equipped.inventoryItem == item)
                return;

            if (item == null)
            {
                equipped = m_Fallback;
                return;
            }

            for (int i = 0; i < m_Items.Count; ++i)
            {
                if (m_Items[i] != null && m_Items[i].inventoryItem == item)
                {
                    equipped = m_Items[i];
                    return;
                }
            }

            if (item.firstPersonPrototype != null)
            {
                var instance = Instantiate(item.firstPersonPrototype, Vector3.zero, Quaternion.identity, itemRoot);
                instance.Attach(this, item);
                m_Items.Add(instance);
                equipped = instance;
            }
        }

        public void UnequipItem(NeoInventoryEquippableItem item)
        {
            equipped = null;
        }
    }
}
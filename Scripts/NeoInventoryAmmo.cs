using Devdog.InventoryPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeoFPS.ModularFirearms;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryAmmo : BaseAmmoBehaviour
    {
        [SerializeField, Tooltip("The ammo type to use.")]
        private string m_DisplayName = "9mm";

        [SerializeField, Tooltip("The effect of the ammo when it hits something.")]
        private BaseAmmoEffect m_Effect = null;

        private NeoInventoryFirstPersonItem m_FirstPersonItem;

        public override string printableName
        {
            get { return m_DisplayName; }
        }

        public override IAmmoEffect effect
        {
            get { return m_Effect; }
        }

        public override int currentAmmo
        {
            get
            {
                return 1;
            }
        }

        public override int maxAmmo
        {
            get { return 999; }
        }

        private InventoryPlayer m_InventoryPlayer;
        public InventoryPlayer inventoryPlayer
        {
            get { return m_InventoryPlayer; }
            set
            {
                if (m_InventoryPlayer != null && m_InventoryPlayer.inventoryCollections.Length > 0)
                {
                    m_InventoryPlayer.inventoryCollections[0].OnAddedItem -= AddedItem;
                    m_InventoryPlayer.inventoryCollections[0].OnRemovedItem -= RemovedItem;
                }

                m_InventoryPlayer = value;

                if (m_InventoryPlayer != null && m_InventoryPlayer.inventoryCollections.Length > 0)
                {
                    m_InventoryPlayer.inventoryCollections[0].OnAddedItem += AddedItem;
                    m_InventoryPlayer.inventoryCollections[0].OnRemovedItem += RemovedItem;
                }
            }
        }

        void Start()
        {
            m_FirstPersonItem = firearm.GetComponent<NeoInventoryFirstPersonItem>();
            m_FirstPersonItem.onAddedToCharacter += OnItemAddedToCharacter;
            m_FirstPersonItem.onRemovedFromCharacter += OnItemRemovedFromCharacter;
        }

        public override void IncrementAmmo(int amount)
        {
            //m_InventoryPlayer.inventoryCollections[0].GetItemCount(m_ID);
        }

        public override void DecrementAmmo(int amount)
        {
            //m_InventoryPlayer.inventoryCollections[0].RemoveItem(m_ID, amount);
        }

        void AddedItem(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {

        }

        void RemovedItem(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {

        }

        int GetAmmoCount()
        {
            if (m_InventoryPlayer == null || m_InventoryPlayer.inventoryCollections.Length == 0)
                return 0;

            return 1;// m_InventoryPlayer.inventoryCollections[0].GetItemCount(m_ID);
        }

        // TODO: How to track when added to inventory
        void OnItemAddedToCharacter(InventoryPlayer ip)
        {
            inventoryPlayer = ip;
            Debug.Log("Item added to character. Get ammo count");
        }

        void OnItemRemovedFromCharacter(InventoryPlayer ip)
        {
            m_InventoryPlayer = null;
            Debug.Log("Item removed from character. Get ammo count");
        }
    }
}
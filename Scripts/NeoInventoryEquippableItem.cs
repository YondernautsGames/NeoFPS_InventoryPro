using UnityEngine;
using Devdog.General;
using Devdog.General.UI;
using Devdog.InventoryPro;
using NeoFPS.Constants;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryEquippableItem : EquippableInventoryItem
    {
        [SerializeField, Tooltip("The prefab for the first person item that represents this inventory item.")]
        private NeoInventoryFirstPersonItem m_FirstPersonPrototype = null;

        private NeoInventoryFirstPersonItemHandler m_Handler;

        public NeoInventoryFirstPersonItem firstPersonPrototype
        {
            get { return m_FirstPersonPrototype; }
        }

        public override bool PickupItem()
        {
            //Debug.Log("Pick up item: " + name);
            return base.PickupItem();
        }

        public override void NotifyItemPickedUp()
        {
            base.NotifyItemPickedUp();

            var col = itemCollection as ICharacterCollection;
            if (col != null)
                Debug.Log("Item picked up by character: " + name);
            else
                Debug.Log("Item added to container: " + name);
        }

        public override GameObject Drop()
        {
            //Debug.Log("Drop item: " + name);
            return base.Drop();
        }

        public override GameObject Drop(Vector3 position, Quaternion rotation)
        {
            Debug.Log("Drop item (with transform): " + name);
            return base.Drop(position, rotation);
        }

        public override bool CanEquip(ICharacterCollection equipTo)
        {
            var result = base.CanEquip(equipTo);
            //Debug.Log(string.Format("Can equip item: {0}, {1}", name, result));
            return result;
        }

        public override bool CanUnEquip(bool addToInventory)
        {
            var result = base.CanUnEquip(addToInventory);
            //Debug.Log(string.Format("Can unequip item: {0}, {1}", name, result));
            return result;
        }

        public override EquippableSlot GetBestEquipSlot(ICharacterCollection equipCollection)
        {
            var result = base.GetBestEquipSlot(equipCollection);
            //Debug.Log(string.Format("Best equipment slot for item: {0}, {1}", name, result));
            return result;
        }

        public override int Use()
        {
            var result = base.Use();
            //Debug.Log(string.Format("Used item: {0}, {1}", name, result));
            return result;
        }

        public override bool CanUse()
        {
            //var result = !m_MustEquip || isEquipped;
            //result &= base.CanUse();
            //Debug.Log(string.Format("Can use item: {0}, {1}", name, result));
            //return result;
            return base.CanUse();
        }

        public override bool CanPickupItem()
        {
            var result = base.CanPickupItem();
            //Debug.Log(string.Format("Can pick up item: {0}, {1}", name, result));
            return result;
        }

        public override void NotifyItemEquipped(EquippableSlot equipSlot, uint amountEquipped)
        {
            m_Handler = (equipSlot.characterCollection.character as MonoBehaviour).GetComponent<NeoInventoryFirstPersonItemHandler>();
            if (m_Handler != null)
                m_Handler.EquipItem(this);

            base.NotifyItemEquipped(equipSlot, amountEquipped);
        }

        public override void NotifyItemUnEquipped(ICharacterCollection equipTo, uint amountUnEquipped)
        {
            if (m_Handler != null)
                m_Handler.UnequipItem(this);

            base.NotifyItemUnEquipped(equipTo, amountUnEquipped);
        }
    }
}

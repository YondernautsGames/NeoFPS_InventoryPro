using Devdog.InventoryPro;
using System;
using UnityEngine;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryFirstPersonItem : MonoBehaviour
    {
        public event Action<InventoryPlayer> onAddedToCharacter;
        public event Action<InventoryPlayer> onRemovedFromCharacter;

        public NeoInventoryFirstPersonItemHandler handler
        {
            get;
            private set;
        }

        public NeoInventoryEquippableItem inventoryItem
        {
            get;
            private set;
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        public void Unequip()
        {
            gameObject.SetActive(false);
        }

        public void Attach(NeoInventoryFirstPersonItemHandler h, NeoInventoryEquippableItem i)
        {
            handler = h;
            inventoryItem = i;

            var t = transform;
            t.SetParent(handler.itemRoot);
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public void Detach()
        {
            handler = null;
            inventoryItem = null;
            gameObject.SetActive(false);
        }
    }
}
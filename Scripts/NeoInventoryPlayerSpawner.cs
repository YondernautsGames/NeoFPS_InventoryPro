using Devdog.InventoryPro;
using NeoFPS.SinglePlayer;
using UnityEngine;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryPlayerSpawner : FpsSoloGameMinimal
    {
        [SerializeField]
        private bool m_OverridePlayerCollections = false;

        [SerializeField]
        private CharacterUI m_CharacterUI = null;

        [SerializeField]
        private ItemCollectionBase[] m_InventoryCollections = new ItemCollectionBase[0];

        [SerializeField]
        private SkillbarUI m_SkillbarCollection = null;

        protected override void OnPlayerCharacterChanged(ICharacter character)
        {
            base.OnPlayerCharacterChanged(character);
            if (character == null)
                return;

            var inventoryPlayer = character.GetComponent<InventoryPlayer>();

            if (m_OverridePlayerCollections)
            {
                inventoryPlayer.characterUI = m_CharacterUI;
                inventoryPlayer.inventoryCollections = m_InventoryCollections;
                inventoryPlayer.skillbarCollection = m_SkillbarCollection;
            }

            if (!inventoryPlayer.isInitialized)
                inventoryPlayer.Init();
        }
    }
}
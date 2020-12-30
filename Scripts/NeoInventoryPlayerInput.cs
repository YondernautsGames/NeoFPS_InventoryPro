using UnityEngine;
using Devdog.General;
using Devdog.General.UI;

namespace NeoFPS.InvProIntegration
{
    public class NeoInventoryPlayerInput : MonoBehaviour, IPlayerInputCallbacks
    {
        [SerializeField, Tooltip("Should the Inventory Pro windows be closed if the player clicks outside of them.")]
        private bool m_CloseWindowsWhenClickWorld = true;

        bool m_InputActive = true;
        bool m_HideWindows = false;

        public virtual void SetInputActive(bool set)
        {
            m_InputActive = set;
            if (m_InputActive)
                NeoFpsInputManager.captureMouseCursor = true;
            else
                NeoFpsInputManager.captureMouseCursor = false;
        }

        void Update()
        {
            if (m_InputActive)
                return;

            if (m_HideWindows)
            {
                m_HideWindows = false;
                foreach (var window in UIWindowUtility.GetAllWindowsWithInput())
                    window.Hide();
            }
            else
            {
                if (m_CloseWindowsWhenClickWorld &&
                    Input.GetMouseButtonDown(0) &&
                    UIUtility.isHoveringUIElement == false)
                {
                    m_HideWindows = true;
                }
            }
        }
    }
}
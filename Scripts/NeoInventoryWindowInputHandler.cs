using NeoFPS;
using NeoFPS.Constants;
using UnityEngine;

namespace Devdog.General.UI
{
    [RequireComponent(typeof(UIWindow))]
    public partial class NeoInventoryWindowInputHandler : FpsInput
    {
        [SerializeField]
        private FpsInputButton m_Button;

        private UIWindow m_Window = null;
        private InputMenu m_InputMenu = null;

        public override FpsInputContext inputContext
        {
            get { return FpsInputContext.None; }
        }

        protected override void OnAwake()
        {
            m_Window = GetComponent<UIWindow>();

            m_InputMenu = GetComponent<InputMenu>();
            if (m_InputMenu == null)
                m_InputMenu = gameObject.AddComponent<InputMenu>();
            m_InputMenu.enabled = false;

            m_Window.OnShow += OnShowWindow;
            m_Window.OnHide += OnHideWindow;
        }

        void OnShowWindow ()
        {
            m_InputMenu.enabled = true;
            NeoFpsInputManagerBase.PushEscapeHandler(EscapeHandler);
        }

        void OnHideWindow()
        {
            m_InputMenu.enabled = false;
            NeoFpsInputManagerBase.PopEscapeHandler(EscapeHandler);
        }

        void EscapeHandler()
        {
            m_Window.Hide();
        }

        protected override void UpdateInput()
        {
            if (m_Button != FpsInputButton.None && FpsSettings.keyBindings.GetButtonDown(m_Button))
            {
                m_Window.Toggle();
            }
        }
    }
}

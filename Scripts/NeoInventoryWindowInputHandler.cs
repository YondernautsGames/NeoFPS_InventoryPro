using NeoFPS;
using NeoFPS.Constants;
using UnityEngine;

namespace Devdog.General.UI
{
    [RequireComponent(typeof(UIWindow))]
    public partial class NeoInventoryWindowInputHandler : FpsInput, IUIWindowInputHandler
    {
        [SerializeField]
        private FpsInputButton m_Button;

        protected UIWindow window;

        public override FpsInputContext inputContext
        {
            get { return FpsInputContext.Menu; }
        }

        protected override void OnAwake()
        {
            window = GetComponent<UIWindow>();
            window.OnShow += PushContext;
            window.OnHide += PopContext;
        }

        protected override void OnEnable()
        {
            // Remove base OnEnable to prevent pushing context
        }

        //protected virtual void Update()
        //{
        //}

        protected override void UpdateInput()
        {
            if (m_Button != FpsInputButton.None && FpsSettings.keyBindings.GetButtonDown(m_Button))
            {
                window.Toggle();
            }
        }
    }
}

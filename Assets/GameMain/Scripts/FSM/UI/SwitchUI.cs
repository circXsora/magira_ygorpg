using BBYGO;
using NodeCanvas.Framework;


namespace NodeCanvas.Tasks.Actions
{

    public class SwitchUI : ActionTask
    {

        public enum UIAction
        {
            Open,
            Close,
        }

        public UIType uiType;
        public UIAction action;
        public bool waitUIComplete;

        protected override string info
        {
            get { return string.Format("{0} UI {1}", action.ToString(), uiType.ToString()); }
        }

        protected override async void OnExecute()
        {
            if (waitUIComplete)
            {

                switch (action)
                {
                    case UIAction.Open:
                        await GameEntry.UI.Open(uiType);
                        break;
                    case UIAction.Close:
                        await GameEntry.UI.Close(uiType);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (action)
                {
                    case UIAction.Open:
                        _ = GameEntry.UI.Open(uiType);
                        break;
                    case UIAction.Close:
                        _ = GameEntry.UI.Close(uiType);
                        break;
                    default:
                        break;
                }
            }
            EndAction(true);
        }
    }
}
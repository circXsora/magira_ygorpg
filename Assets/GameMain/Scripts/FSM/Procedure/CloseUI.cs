using BBYGO;
using GameFramework.Event;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	public class CloseUI : ActionTask{

		public UIType uiType;

		protected override string info
		{
			get { return string.Format("Close UI {0}", uiType.ToString()); }
		}

		protected override void OnExecute(){
			_ = GameEntry.UI.Close(uiType);
		}
	}
}
using BBYGO;
using GameFramework.Event;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	public class OpenUI : ActionTask{

		public UIType uiType;

		protected override string info
		{
			get { return string.Format("Open UI {0}", uiType.ToString()); }
		}

		protected override void OnExecute(){
			_ = GameEntry.UI.Open(uiType);
		}
	}
}
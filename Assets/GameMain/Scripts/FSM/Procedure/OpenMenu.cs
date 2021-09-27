using BBYGO;
using GameFramework.Event;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	public class OpenMenu : ActionTask{

		public int testValue;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
            _ = GameEntry.UI.Open(UIType.MenuForm);
			//GameEntry.UI.RegisterButtonClickEvent(UIType.MenuForm, ()=> EndAction(true));
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate(){
			
		}

		//Called when the task is disabled.
		protected override void OnStop(){
			_ = GameEntry.UI.Close(UIType.MenuForm);
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}
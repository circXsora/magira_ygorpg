using UnityEditor;

namespace SBG.SpeedScript
{
	public class CustomContextMenuItems
	{
		#region TEMPLATES
		
		//MENU: UIForm
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "U I Form", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateUIForm(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_UIForm", "NewUIForm");
		}
		//END: UIForm
																																																																																																																																										
		#endregion

		#region EDITOR TEMPLATES

		#endregion
	}
}
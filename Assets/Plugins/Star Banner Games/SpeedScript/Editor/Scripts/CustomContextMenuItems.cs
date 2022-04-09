using UnityEditor;

namespace SBG.SpeedScript
{
	public class CustomContextMenuItems
	{
		#region TEMPLATES
		
		//MENU: Work
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "Work", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateWork(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_Work", "NewWork");
		}
		//END: Work
		
		//MENU: EventArgs
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "Event Args", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateEventArgs(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_EventArgs", "NewEventArgs");
		}
		//END: EventArgs
		
		//MENU: EntityData
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "Entity Data", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateEntityData(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_EntityData", "NewEntityData");
		}
		//END: EntityData
		
		//MENU: EntityLogic
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "Entity Logic", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateEntityLogic(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_EntityLogic", "NewEntityLogic");
		}
		//END: EntityLogic
		
		//MENU: uiForm
		[MenuItem(GlobalPaths.TEMPLATE_MENUPATH + "ui Form", false, ScriptBuilder.TEMPLATE_CUSTOM_MENU_PRIORITY)]
		public static void CreateuiForm(MenuCommand cmd)
		{
			ScriptBuilder.CreateScriptFromCustomTemplate("Template_uiForm", "NewuiForm");
		}
		//END: uiForm
																																																																																																																																																		
		#endregion

		#region EDITOR TEMPLATES

		#endregion
	}
}
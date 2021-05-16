using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SBG.SpeedScript
{
    [InitializeOnLoad]
    public sealed class ScriptBuilder : UnityEditor.AssetModificationProcessor
    {
        #region CONSTANTS

        public const int TEMPLATE_NORMAL_MENU_PRIORITY = 81; //Picked so it appears right before the default "C# Scripts" menu item.
        public const int TEMPLATE_CUSTOM_MENU_PRIORITY = 100;
        public const int TEMPLATE_EDITOR_MENU_PRIORITY = 120;
        public const int TEMPLATE_CUSTOMEDITOR_MENU_PRIORITY = 140;

        #endregion

        private static string _defaultNamespace = "CoolCompany.CoolGame";
        private static string _creatorName = "你的名字";
        private static string _companyName = "你的公司名";
        private static string _projectName = "你的项目名";
        private static string _email = "你的邮箱";
        private static string _ignoreFolder = "Scripts";
        private static bool _useFolderNames = false;
        private static bool _usePlayerSettings = true;


        static ScriptBuilder()
        {
            if (SessionState.GetBool("SpeedScriptLoaded", false) == false)
            {
                SessionState.SetBool("SpeedScriptLoaded", true);
                TemplateSettingsEditorWindow.RefreshCustomTemplates();
            }
        }


        #region DEBUG MESSAGES

        ///Not pretty to put these in here, but it doesn't feel worth its own class

        public static void Log(string text)
        {
            Debug.Log($"SPEED SCRIPT: {text}");
        }

        public static void LogWarning(string text)
        {
            Debug.LogWarning($"SPEED SCRIPT: {text}");
        }

        public static void LogError(string text)
        {
            Debug.Log($"SPEED SCRIPT: {text}");
        }

        #endregion

        #region SCRIPT CREATION

        /// <summary>
        /// Looks for a Template in the Data Folder and creates a script from it.
        /// </summary>
        public static void CreateScriptFromTemplate(string templateName, string defaultScriptName)
        {
            string templatePath = Path.Combine(GlobalPaths.RelativeTemplateFolderPath, $"{templateName}.cs.txt");

            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, $"{defaultScriptName}.cs");
        }

        /// <summary>
        /// Looks for a Template in the external UserData Folder and creates a script from it.
        /// </summary>
        public static void CreateScriptFromCustomTemplate(string templateName, string defaultScriptName)
        {
            string templatePath = Path.Combine(GlobalPaths.UserTemplatesFolderPath, $"{templateName}.cs.txt");

            try
            {
                ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, $"{defaultScriptName}.cs");
            }
            catch
            {
                ScriptBuilder.LogWarning("Template not found! Reimporting Custom Templates...");
                TemplateSettingsEditorWindow.RefreshCustomTemplates();
            }
        }

        public static void OnWillCreateAsset(string path)
        {
            ///THIS FUNCTION IS CALLED FROM "UnityEditor.AssetModificationProcessor"
            ///The Path passed to the function starts with "Assets/..."
            ///and leads to the .meta file of the created asset.
            ///We need to:
            ///1. REMOVE the .meta
            ///2. Check if it is a .cs file (meta files go "name.ending.meta" => "scriptname.cs.meta")
            ///3. Get the full system path.
            ///4. Tinker with the file
            ///5. Refresh the Assets so Unity displays the changes

            //1
            path = path.Replace(".meta", string.Empty);

            //2
            if (!path.EndsWith(".cs"))
            {
                return;
            }

            //3
            string pathNoAssets = path;
            string assetString = "Assets/";
            if (path.StartsWith(assetString)) pathNoAssets = path.Remove(0, assetString.Length);
            string systemPath = $"{Application.dataPath}/{pathNoAssets}";

            //4
            ReplaceKeywords(systemPath, path);

            //5
            AssetDatabase.Refresh();
        }

        private static void ReplaceKeywords(string systemPath, string projectPath)
        {
            LoadConfig();

            string fileData = File.ReadAllText(systemPath);

            if (fileData.Contains("#NAMESPACE#"))
            {
                string fullNamespace = GetNamespace(projectPath);
                fileData = fileData.Replace("#NAMESPACE#", fullNamespace);
            }

            if (fileData.Contains("#SCRIPTNAME!EDITOR#"))
            {
                string fileNameNoEditor = GetFilenameNoEditor(projectPath);
                fileData = fileData.Replace("#SCRIPTNAME!EDITOR#", fileNameNoEditor);
            }

            if (fileData.Contains("#CREATORNAME#"))
            {
                fileData = fileData.Replace("#CREATORNAME#", _creatorName);
            }

            if (fileData.Contains("#COMPANYNAME#"))
            {
                fileData = fileData.Replace("#COMPANYNAME#", _companyName);
            }

            if (fileData.Contains("#EMAIL#"))
            {
                fileData = fileData.Replace("#EMAIL#", _email);
            }

            if (fileData.Contains("#PROJECTNAME#"))
            {
                fileData = fileData.Replace("#PROJECTNAME#", _projectName);
            }
            if (fileData.Contains("#CREATETIME#"))
            {
                fileData = fileData.Replace("#CREATETIME#", DateTime.Now.ToString());
            }
            
            File.WriteAllText(systemPath, fileData);
        }

        #endregion

        #region LOAD DATA

        private static void LoadConfig()
        {
            string path = GlobalPaths.ConfigPath;

            if (File.Exists(path))
            {
                string[] content = File.ReadAllLines(path);

                if (content.Length >= 4) //BEWARE
                {
                    _defaultNamespace = content[0];
                    _ignoreFolder = content[1];
                    _useFolderNames = content[2] == "1";
                    _usePlayerSettings = content[3] == "1";
                    if (content.Length >= 5)
                        _creatorName = content[4];
                    if (content.Length >= 6)
                        _companyName = content[5];
                    if (content.Length >= 7)
                        _projectName = content[6];
                    if (content.Length >= 8)
                        _email = content[7];
                }

                if (_usePlayerSettings)
                {
                    _defaultNamespace = $"{PlayerSettings.companyName}.{PlayerSettings.productName}";
                }
            }
        }

        private static string GetNamespace(string projectPath)
        {
            string fullNamespace;

            if (_useFolderNames)
            {
                string slashIgnore = $"/{_ignoreFolder}/";

                //Limit namespace Hierarchy to any folder after "Scripts"
                int ignoreIndex = projectPath.IndexOf(slashIgnore);
                if (ignoreIndex > -1)
                {
                    fullNamespace = projectPath.Substring(ignoreIndex + slashIgnore.Length);
                }
                else
                {
                    string assetString = "Assets/";
                    ignoreIndex = projectPath.IndexOf(assetString);
                    fullNamespace = projectPath.Substring(ignoreIndex + assetString.Length);
                }

                //Remove Filename to only get directory path
                if (fullNamespace.Contains("/"))
                {
                    fullNamespace = fullNamespace.Remove(fullNamespace.LastIndexOf('/'));
                    //Replace Slashes with dots for Namespace Syntax
                    fullNamespace = fullNamespace.Replace('/', '.');
                    //Add Default Namespace to start of Folder Namespace
                    fullNamespace = $"{_defaultNamespace}.{fullNamespace}";
                }
                else
                {
                    fullNamespace = _defaultNamespace;
                }

                if (string.IsNullOrEmpty(fullNamespace))
                {
                    fullNamespace = _defaultNamespace;
                }
            }
            else
            {
                fullNamespace = _defaultNamespace;
            }

            fullNamespace = fullNamespace.Replace(" ", string.Empty);

            return fullNamespace;
        }

        private static string GetFilenameNoEditor(string projectPath)
        {
            string fileName = GlobalPaths.GetFolderNameFromPath(projectPath);

            int cutOffIndex = fileName.LastIndexOf("Editor.cs");
            if (cutOffIndex > -1)
            {
                return fileName.Remove(cutOffIndex);
            }

            cutOffIndex = fileName.LastIndexOf("Inspector.cs");
            if (cutOffIndex > -1)
            {
                return fileName.Remove(cutOffIndex);
            }

            cutOffIndex = fileName.LastIndexOf(".cs");
            fileName = "YourClass";

            return fileName;
        }

        #endregion
    }
}
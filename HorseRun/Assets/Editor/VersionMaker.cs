using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class VersionMaker : EditorWindow
{
    private Vector2 pos = Vector2.zero;

    [MenuItem("Version/版本制作")]
    public static void ProductVersion()
    {
        UpdateBuildSetting();
        EditorWindow.GetWindow(typeof(VersionMaker));
    }

    [MenuItem("Version/调试")]
    public static void Debug()
    {
        UpdateBuildSetting();
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene("Assets/Scenes/Start.unity");
        EditorApplication.isPlaying = true;
    }

    /// <summary>
    /// 添加场景
    /// </summary>
    public static void UpdateBuildSetting()
    {
        List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
        scenes.Add(new EditorBuildSettingsScene("Assets/Scenes/Start.unity",true));
        //scenes.Add(new EditorBuildSettingsScene("Assets/Scenes/Loading.unity", true));
        scenes.Add(new EditorBuildSettingsScene("Assets/Scenes/Main.unity", true));

        EditorBuildSettings.scenes = scenes.ToArray();
    }

    private void OnGUI()
    {
        pos = GUILayout.BeginScrollView(pos);
        if (GUILayout.Button("制作XX版本"))
        {
            BuildNormal();
        }
        GUILayout.EndScrollView();
    }

    public static void BuildNormal()
    {
        string path = "G:/NiuNiu/";
        string versionName = "动物狂欢";
        PlayerSettings.companyName = "XXX";
        PlayerSettings.productName = "XXX";
        //Texture2D[] tex = new Texture2D[7];
        //for (int index = 0; index < 7; index++)
        //{
        //    tex[index] = (Texture2D)Resources.Load("Icon/xcb");
        //}
        //PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, tex);
        SetSymbols("NiuNiu");
        BuildPipeline.BuildPlayer(new string[]
        {
            "Assets/Scenes/Start.unity",
            "Assets/Scenes/Main.unity",
        }, path + versionName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        //BuildVersion(versionName, path);
    }

    /// <summary>
    /// 定义标签
    /// </summary>
    /// <param name="s"></param>
    public static void SetSymbols(string s)
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, s);
    }

    public static void BuildVersion(string path, string name, string versionName)
    {
        
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ImportFBXField
{
    public static string path;

    public static void DrawUI(string fieldName)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(fieldName, GUILayout.Width(145));
            if (GUILayout.Button("Select FBX", GUILayout.Width(100)))
            {
                path = EditorUtility.OpenFilePanel(fieldName, "", "fbx");
            }
        }
    }

    public static void ImportFBXModel()
    {
        if (!string.IsNullOrEmpty(path))
        {
            FileUtil.CopyFileOrDirectory(path, $"Assets/1_Graphics/Models/{Path.GetFileName(path)}");
            AssetDatabase.Refresh();
        }
    }
}

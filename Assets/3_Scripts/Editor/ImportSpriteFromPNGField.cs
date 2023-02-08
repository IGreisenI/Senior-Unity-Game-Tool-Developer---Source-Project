using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ImportSpriteFromPNGField
{
    public static string path;

    public static void DrawUI(string fieldName)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(fieldName, GUILayout.Width(145));
            if (GUILayout.Button("Select PNG", GUILayout.Width(100)))
            {
                path = EditorUtility.OpenFilePanel(fieldName, "", "png");
            }
        }
    }

    public static void ImportAsSprite()
    {
        if (!string.IsNullOrEmpty(path))
        {
            FileUtil.CopyFileOrDirectory(path, $"Assets/1_Graphics/Store/{Path.GetFileName(path)}");
            AssetDatabase.Refresh();
        }
    }
}

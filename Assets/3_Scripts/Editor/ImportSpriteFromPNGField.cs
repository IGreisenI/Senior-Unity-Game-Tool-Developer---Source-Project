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

    public static Sprite ImportAsSprite()
    {
        if (!string.IsNullOrEmpty(path))
        {
            string destinationPath = $"Assets/1_Graphics/Store/{Path.GetFileName(path)}";

            FileUtil.CopyFileOrDirectory(path, destinationPath);
            AssetDatabase.Refresh();

            TextureImporter importer = AssetImporter.GetAtPath(destinationPath) as TextureImporter;

            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;

                // Save the changes to the texture asset
                importer.SaveAndReimport();
            }
            else
            {
                Debug.LogError($"Texture not found at path: {destinationPath}");
            }



            return (Sprite)AssetDatabase.LoadAssetAtPath(destinationPath, typeof(Sprite));
        }

        Debug.LogError("Path provided is empty");
        return null;
    }
}

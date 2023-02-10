using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ImportTextureField
{
    public static string path;

    /// <summary>
    /// Draws import button with a label for the button and field to show selected files name
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="textureFileExt"></param>
    public static void DrawUI(string fieldName, string textureFileExt)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(fieldName, GUILayout.Width(145));
            if (GUILayout.Button($"Select {textureFileExt.ToUpper()}", GUILayout.Width(100)))
            {
                path = EditorUtility.OpenFilePanel(fieldName, "", textureFileExt);
            }
            GUILayout.Label(Path.GetFileName(path), GUILayout.ExpandWidth(true));
        }
    }

    public static Sprite ImportAsSprite()
    {
        // If no path was selected return null
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Path provided is empty");
            return null;
        }

        // Copy the selected file if it doesn't exist already
        string destinationPath = $"Assets/1_Graphics/Store/{Path.GetFileName(path)}";
        if (!File.Exists(destinationPath))
        {
            FileUtil.CopyFileOrDirectory(path, destinationPath);
        }
        else
        {
            Debug.LogWarning($"File {destinationPath} already imported");
        }
        AssetDatabase.Refresh();

        // Import texture as a sprite and optimise for mobile devices
        TextureImporter importer = AssetImporter.GetAtPath(destinationPath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;

            OptimiseTetureForMobile(importer);
                
            importer.SaveAndReimport();
        }
        else
        {
            Debug.LogError($"Texture not found at path: {destinationPath}");
        }

        // Return the imported Texture as a Sprite
        return (Sprite)AssetDatabase.LoadAssetAtPath(destinationPath, typeof(Sprite));
    }

    public static TextureImporter OptimiseTetureForMobile(TextureImporter importer)
    {
        importer.maxTextureSize = 512;
        importer.textureCompression = TextureImporterCompression.Compressed;
        importer.SaveAndReimport();
        return importer;
    }
}

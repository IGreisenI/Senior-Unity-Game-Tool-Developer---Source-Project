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

    public static Object ImportFBXModel()
    {
        if (!string.IsNullOrEmpty(path))
        {
            string destinationPath = $"Assets/1_Graphics/Models/{Path.GetFileName(path)}";

            FileUtil.CopyFileOrDirectory(path, destinationPath);
            AssetDatabase.Refresh();

            ModelImporter importer = AssetImporter.GetAtPath(destinationPath) as ModelImporter;

            if (importer != null)
            {
                importer.importVisibility = false;
                importer.importCameras = false;
                importer.importLights = false;

                importer.importNormals = ModelImporterNormals.Import;
                importer.indexFormat = ModelImporterIndexFormat.UInt16;
                importer.normalCalculationMode = ModelImporterNormalCalculationMode.Unweighted_Legacy;

                importer.animationType = ModelImporterAnimationType.Human;
                importer.importAnimation = false;

                // Save the changes and reimport
                importer.SaveAndReimport();
            }
            else
            {
                Debug.LogError($"Texture not found at path: {destinationPath}");
            }

            return AssetDatabase.LoadAssetAtPath(destinationPath, typeof(Object));
        }

        Debug.LogError("Path provided is empty");
        return null;
    }
}

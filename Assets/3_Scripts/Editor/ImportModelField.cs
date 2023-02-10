using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ImportModelField
{
    public static string path;

    public static void DrawUI(string fieldName, string modelFileExt)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(fieldName, GUILayout.Width(145));
            if (GUILayout.Button($"Select {modelFileExt.ToUpper()}", GUILayout.Width(100)))
            {
                path = EditorUtility.OpenFilePanel(fieldName, "", modelFileExt);
            }
            GUILayout.Label(Path.GetFileName(path), GUILayout.ExpandWidth(true));
        }
    }

    public static Object ImportFBXCharacterModel()
    {
        if (!string.IsNullOrEmpty(path))
        {
            string destinationPath = $"Assets/1_Graphics/Models/{Path.GetFileName(path)}";
             
            if (!File.Exists(destinationPath))
            {
                FileUtil.CopyFileOrDirectory(path, destinationPath);
            }
            else
            {
                Debug.LogWarning($"File {destinationPath} already imported");
            }
            AssetDatabase.Refresh();

            ModelImporter importer = AssetImporter.GetAtPath(destinationPath) as ModelImporter;

            if (importer != null)
            {
                OptimiseModelForMobile(importer);
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

    private static ModelImporter OptimiseModelForMobile(ModelImporter importer)
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

        return importer;
    }
}

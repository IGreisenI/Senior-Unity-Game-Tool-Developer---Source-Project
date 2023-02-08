using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CharacterImportWindow : EditorWindow
{
    private string characterName;
    private int characterPrice;
    private GameObject characterModel;
    private Sprite characterSprite;

    [MenuItem("Tools/Character Import")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterImportWindow));
    }

    private void DrawUI()
    {
        characterName = EditorGUILayout.TextField("Character Name", characterName);
        characterPrice = EditorGUILayout.IntField("Character Speed", characterPrice);
        ImportFBXField.DrawUI("Character Model");
        ImportSpriteFromPNGField.DrawUI("Character Sprite");
    }

    private void OnGUI()
    {
        DrawUI();
        if (GUILayout.Button("Import"))
        {
            ImportCharacter();
        }
    }

    private void ImportCharacter()
    {
        ImportSpriteFromPNGField.ImportAsSprite();
    }
}

public class ImportFBXField
{
    public static string path;
    public static Object fbxFile;

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
    }
}

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
        if(path.Length > 0)
        {
            FileUtil.CopyFileOrDirectory(path, $"Assets/1_Graphics/Store/{Path.GetFileName(path)}");
            AssetDatabase.Refresh();
        }
    }
}
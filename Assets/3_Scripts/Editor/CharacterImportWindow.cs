using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterImportWindow : EditorWindow
{
    private string characterName;
    private int characterPrice;
    private GameObject characterModel;
    private Sprite characterSprite;

    [MenuItem("Window/CharacterImport")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterImportWindow));
    }

    private void DrawUI()
    {
        characterName = EditorGUILayout.TextField("Character Name", characterName);
        characterPrice = EditorGUILayout.IntField("Character Speed", characterPrice);
        characterModel = (GameObject)EditorGUILayout.ObjectField("Character Model", characterModel, typeof(GameObject), false);
        characterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite", characterSprite, typeof(Sprite), false);
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

    }
}
 
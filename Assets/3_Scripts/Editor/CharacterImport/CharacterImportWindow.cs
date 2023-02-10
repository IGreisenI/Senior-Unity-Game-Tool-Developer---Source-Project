using UnityEngine;
using UnityEditor;

public class CharacterImportWindow : EditorWindow
{
    [SerializeField] private ImportCharacter importCharacterSettings;

    private string characterName;
    private int characterPrice;

    private bool foldout;

    [MenuItem("Tools/Character/Character Import")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterImportWindow));
    }

    private void DrawUI()
    {
        characterName = EditorGUILayout.TextField("Character Name", characterName);
        characterPrice = EditorGUILayout.IntField("Character Price", characterPrice);
        ImportModelField.DrawUI("Character Model", "fbx");
        ImportTextureField.DrawUI("Character Sprite", "png");

        foldout = EditorGUILayout.Foldout(foldout, "Additional Import Settings");
        if (foldout)
        {
            Editor.CreateEditor(importCharacterSettings).OnInspectorGUI();
        }
    }

    private void OnGUI()
    {
        DrawUI();
        if (GUILayout.Button("Import"))
        {
            importCharacterSettings.CreateStoreItem(characterName, characterPrice, ImportModelField.ImportFBXCharacterModel(), ImportTextureField.ImportAsSprite());
        }
    }
}
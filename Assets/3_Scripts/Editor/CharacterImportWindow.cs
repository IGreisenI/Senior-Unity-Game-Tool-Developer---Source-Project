using UnityEngine;
using UnityEditor;

public class CharacterImportWindow : EditorWindow
{
    [SerializeField] private ImportCharacter importCharacterSettings;

    private string characterName;
    private int characterPrice;

    [MenuItem("Tools/Character Import")]
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
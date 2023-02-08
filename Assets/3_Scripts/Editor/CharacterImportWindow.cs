using UnityEngine;
using UnityEditor;

public class CharacterImportWindow : EditorWindow
{
    private StoreItem storeItem;

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
        ImportFBXField.ImportFBXModel();
        ImportSpriteFromPNGField.ImportAsSprite();
    }
}
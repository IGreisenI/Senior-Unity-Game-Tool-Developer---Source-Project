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
        characterPrice = EditorGUILayout.IntField("Character Price", characterPrice);
        ImportModelField.DrawUI("Character Model", "fbx");
        ImportTextureField.DrawUI("Character Sprite", "png");
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
        StoreItem storeItem = ScriptableObject.CreateInstance<StoreItem>();

        storeItem.Name = characterName;
        storeItem.Price = characterPrice;

        storeItem.Icon = ImportTextureField.ImportAsSprite();

        Object model = ImportModelField.ImportFBXCharacterModel();
        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(model);

        CapsuleCollider collider = prefab.AddComponent<CapsuleCollider>();

        PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

        storeItem.Prefab = PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/2_Prefabs/{prefab.name}.prefab");
        DestroyImmediate(prefab.gameObject, false);

        AssetDatabase.CreateAsset(storeItem, $"Assets/4_ScriptableObject/{characterName}SO.asset");
        AssetDatabase.SaveAssets();
    }
}
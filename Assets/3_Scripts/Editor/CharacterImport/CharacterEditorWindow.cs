using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StoreItemUI
{
    public Editor storeItemEditor;
    public bool foldout;
    public bool reimportSprite;
    public bool reimportModel;

    public StoreItemUI(Editor editor)
    {
        this.storeItemEditor = editor;
        foldout = false;
        reimportSprite = false;
        reimportModel = false;
    }
}

public class CharacterEditorWindow : EditorWindow
{
    [SerializeField] private ImportCharacter importCharacter;
    private List<StoreItemUI> itemUIs = new List<StoreItemUI>();   

    [MenuItem("Tools/Character/Character Editor")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterEditorWindow));
    }

    private void DrawUI()
    {
        // Get the GUIDs of all StoreItem assets in the project
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(StoreItem).Name);
        StoreItem[] storeItems = new StoreItem[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            // Load the current StoreItem asset
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            storeItems[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path);

            // If there's not yet a StoreItemUI object for the current StoreItem, create one
            if (itemUIs.Count <= i)
            {
                itemUIs.Add(new StoreItemUI(Editor.CreateEditor(storeItems[i])));
            }

            itemUIs[i].foldout = EditorGUILayout.Foldout(itemUIs[i].foldout, storeItems[i].Name);
            if (itemUIs[i].foldout)
            {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.ExpandWidth(true)))
                {
                    Foldout(itemUIs[i], storeItems[i]);
                }
            }
        }
    }

    private void Foldout(StoreItemUI itemUI, StoreItem storeItem)
    {
        // Show the inspector for the StoreItem
        itemUI.storeItemEditor.OnInspectorGUI();

        // If toggle is toggled on, show UI for importing a model
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Reimport Char Model", GUILayout.Width(145));
            itemUI.reimportModel = GUILayout.Toggle(itemUI.reimportModel, "");
        }
        if (itemUI.reimportModel)
        {
            ImportModelField.DrawUI("Import Model", "fbx");
        }

        // If toggle is toggled on, show UI for importing a texture
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("Reimport Char Sprite", GUILayout.Width(145));
            itemUI.reimportSprite = GUILayout.Toggle(itemUI.reimportSprite, "");
        }
        if (itemUI.reimportSprite)
        {
            ImportTextureField.DrawUI("Import Sprite", "png");
        }

        // Buttons for saving and removing the character
        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Save"))
            {
                if (itemUI.reimportModel)
                    importCharacter.ReimportModel(storeItem, ImportModelField.ImportFBXCharacterModel());
                if (itemUI.reimportSprite)
                    importCharacter.ReimportTexture(storeItem, ImportTextureField.ImportAsSprite());
            }
            if (GUILayout.Button("Remove"))
            {
                importCharacter.RemoveCharacter(storeItem);
            }
        }
    }

    private void OnGUI()
    {
        DrawUI();
    }
}

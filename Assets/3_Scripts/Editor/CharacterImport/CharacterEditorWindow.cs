using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StoreItemUI
{
    public Editor editor;
    public bool foldout;
    public bool reimportSprite;
    public bool reimportModel;

    public StoreItemUI(Editor editor)
    {
        this.editor = editor;
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
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(StoreItem).Name);
        StoreItem[] storeItems = new StoreItem[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            storeItems[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path);

            if (itemUIs.Count <= i)
            {
                itemUIs.Add(new StoreItemUI(Editor.CreateEditor(storeItems[i])));
            }

            itemUIs[i].foldout = EditorGUILayout.Foldout(itemUIs[i].foldout, storeItems[i].Name);
            if (itemUIs[i].foldout)
            {
                itemUIs[i].editor.OnInspectorGUI();

                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Reimport Char Model", GUILayout.Width(145));
                    itemUIs[i].reimportModel = GUILayout.Toggle(itemUIs[i].reimportModel, "");
                }
                if (itemUIs[i].reimportModel)
                {
                    ImportModelField.DrawUI("Import Model", "fbx");
                }

                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Reimport Char Sprite", GUILayout.Width(145));
                    itemUIs[i].reimportSprite = GUILayout.Toggle(itemUIs[i].reimportSprite, "");
                }
                if (itemUIs[i].reimportSprite)
                {
                    ImportTextureField.DrawUI("Import Sprite", "png");
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Save"))
                    {
                        if (itemUIs[i].reimportModel)
                            importCharacter.ReimportModel(storeItems[i], ImportModelField.ImportFBXCharacterModel());
                        if (itemUIs[i].reimportSprite)
                            importCharacter.ReimportTexture(storeItems[i], ImportTextureField.ImportAsSprite());
                    }
                    if (GUILayout.Button("Remove"))
                    {
                        importCharacter.RemoveCharacter(storeItems[i]);
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        DrawUI();
    }

}

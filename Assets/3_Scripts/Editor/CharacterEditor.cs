using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow
{
    [MenuItem("Tools/Character Editor")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterEditor));

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(StoreItem).Name);
        StoreItem[] a = new StoreItem[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path);
        }

    }

    private void DrawUI()
    {
        using (new GUILayout.VerticalScope())
        {
            using (new GUILayout.HorizontalScope())
            {
                string[] guids = AssetDatabase.FindAssets("t:" + typeof(StoreItem).Name);
                StoreItem[] a = new StoreItem[guids.Length];
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    a[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path);
                }
            }
        }
    }

    private void OnGUI()
    {
        DrawUI();
        if (GUILayout.Button("Edit"))
        {

        }
    }
}

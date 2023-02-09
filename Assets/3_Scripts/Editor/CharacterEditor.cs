using UnityEngine;
using UnityEditor;

public class CharacterEditor : EditorWindow
{
    public bool showPosition = true;
    public string status = "Select a GameObject"; 
    
    private string characterName;
    private int characterPrice;

    [MenuItem("Tools/Character Editor")]
    static void ShowWindow()
    {
        GetWindow(typeof(CharacterEditor));
    }

    private void DrawUI()
    {
        
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(StoreItem).Name);
        StoreItem[] a = new StoreItem[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string[] path = { AssetDatabase.GUIDToAssetPath(guids[i]) };
            a[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path[0]);
            showPosition = EditorGUI.Foldout(new Rect(3, 3, position.width - 6, 15), showPosition, a[i].Name);
            if (showPosition)
            {
                Editor.CreateEditor(a[i]).OnInspectorGUI();
            }
        }
    }

    private void OnGUI()
    {
        DrawUI();
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}

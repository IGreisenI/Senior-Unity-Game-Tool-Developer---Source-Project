using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CharacterEditor : EditorWindow
{
    public bool showPosition = true;
    public string status = "Select a GameObject"; 

    private bool foldout;
    private List<Editor> editors = new();


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
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<StoreItem>(path);

            foldout = EditorGUILayout.Foldout(foldout, a[i].Name);
            if (foldout)
            {
                if (editors[i] == null)
                {
                    editors.Add(Editor.CreateEditor(a[i]));
                }
                editors[i].OnInspectorGUI();
            }
        }
    }

    private void OnGUI()
    {
        DrawUI();
    }

}

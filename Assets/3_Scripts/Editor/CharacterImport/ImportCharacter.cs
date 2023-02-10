using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "ImportCharacterSettings", menuName = "ImportSettings/CharacterImport")]
public class ImportCharacter : ScriptableObject
{
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private float colliderRadius;
    [SerializeField] private float colliderHeight;

    public void CreateStoreItem(string characterName, int characterPrice, Object model, Sprite icon)
    {
        StoreItem storeItem = ScriptableObject.CreateInstance<StoreItem>();
        storeItem.Id = AssetDatabase.FindAssets("t:" + typeof(StoreItem)).Length;
        storeItem.Name = characterName;
        storeItem.Price = characterPrice;
        storeItem.Icon = icon;

        AssetDatabase.CreateAsset(storeItem, $"Assets/4_ScriptableObject/{characterName}SO.asset");
        AssetDatabase.SaveAssets();

        CreatePrefabForCharacter(storeItem, model);

        Store.Instance.StoreItems.Add(storeItem);
    }

    private void CreatePrefabForCharacter(StoreItem storeItem, Object model)
    {
        if (storeItem.Prefab)
        {
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(storeItem.Prefab));
            storeItem.Prefab = null;
        }

        GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(model);

        CapsuleCollider collider = prefab.AddComponent<CapsuleCollider>();
        collider.radius = colliderRadius;
        collider.height = colliderHeight;

        Animator animator = prefab.GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
        animator.applyRootMotion = false;
        animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;

        PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);

        storeItem.Prefab = PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/2_Prefabs/{prefab.name}.prefab");

        DestroyImmediate(prefab.gameObject, false);
    }

    public void ReimportModel(StoreItem storeItem, Object model)
    {
        CreatePrefabForCharacter(storeItem, model);
    }

    public void ReimportTexture(StoreItem storeItem, Sprite icon)
    {
        storeItem.Icon = icon;
    }

    public void RemoveCharacter(StoreItem storeItem)
    {
        Store.Instance.StoreItems.Remove(storeItem);
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(storeItem));
    }
}
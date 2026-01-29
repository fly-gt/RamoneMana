using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UtilityAdressables {
    private static HashSet<string> addressIndex = new();
    private static bool indexInited = false;

    public static void InitializeCache() {
        if (!indexInited) {
            indexInited = true;

            foreach (var locator in Addressables.ResourceLocators) {
                foreach (var key in locator.Keys) {
                    if (key is string s) {
                        addressIndex.Add(s);
                    }
                }
            }
        }
    }

    public static async UniTask<T> InitializeObject<T>() where T : Component {
        var name = typeof(T).Name;

        if (!AddressableIdExist(name)) {
            return null;
        }

        var handle = Addressables.InstantiateAsync(name);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded) {
            Debug.LogError($"Failed to instantiate {name}");
            return null;
        }

        var newObj = handle.Result;

        if (!newObj.TryGetComponent(out T comp)) {
            Debug.LogError($"Failed to 'Get Component' {name}");
            return null;
        }

        return comp;
    }

    public static async UniTask<T> InitializeObject<T>(AssetReference assetReference, Transform parent = null) where T : Component {
        var name = typeof(T).Name;

        //if (!AddressableIdExist(name)) {
        //    return null;
        //}

        var handle = Addressables.InstantiateAsync(assetReference, parent: parent);
        await handle.Task;

        if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded) {
            Debug.LogError($"Failed to instantiate {assetReference.RuntimeKey}");
            return null;
        }

        GameObject newObj = handle.Result;

        if (!newObj.TryGetComponent(out T comp)) {
            Debug.LogError($"Failed to 'Get Component' {assetReference.RuntimeKey}");
            return null;
        }

        return comp;
    }

    public static bool AddressableIdExist(string index) {
        if (!addressIndex.Contains(index)) {
            Debug.LogError($"{index} none");
            return false;
        }

        return true;

        //foreach (var locator in Addressables.ResourceLocators) {
        //    if (locator.Locate(index, typeof(object), out IList<IResourceLocation> locations)) {
        //        return locations != null && locations.Count > 0;
        //    }
        //}

        //return false;
    }
}

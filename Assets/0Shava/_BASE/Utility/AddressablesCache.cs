using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesCache {
    private HashSet<string> addressIndex = new HashSet<string>();
    private bool indexInited = false;

    public void Initialize() {
        if (!indexInited) {
            indexInited = true;

            foreach (var locator in Addressables.ResourceLocators) {
                foreach (var key in locator.Keys) {
                    if (key is string s)
                        addressIndex.Add(s);
                }
            }
        }
    }

    public bool AddressableExist(string index) {
        if (!addressIndex.Contains(index)) {
            Debug.Log($"{index} none");
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

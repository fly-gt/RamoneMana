using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectPool : MonoBehaviour {
    //public GameObject prefab;
    public AssetReference prefab;
    public int initSize = 10;

    public LinkedList<GameObject> pool = new();

    private void Awake() {
        for (int i = 0; i < initSize; i++) {
            CreateObject();
        }
    }

    private void OnDestroy() {
        foreach (var p in pool) {
            Addressables.Release(p);
        }

        pool.Clear();
    }

    private async UniTask<GameObject> CreateObject() {
        //GameObject obj = Instantiate(prefab, transform);
        GameObject obj = (await UtilityAdressables.InitializeObject<Transform>(prefab, transform)).gameObject;
        obj.SetActive(false);
        pool.AddLast(obj);
        return obj;
    }

    public async UniTask<GameObject> Get() {
        if (pool.Count == 0) {
            await CreateObject();
        }

        LinkedListNode<GameObject> node = pool.First;
        GameObject obj = node.Value;
        pool.Remove(node);

        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj) {
        obj.SetActive(false);
        pool.AddLast(obj);
    }
}

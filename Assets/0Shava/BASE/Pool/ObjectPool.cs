using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public GameObject prefab;
    public int initSize;

    public LinkedList<GameObject> pool = new();

    private void Awake() {
        for (int i = 0; i < initSize; i++) {
            CreateObject();
        }
    }

    private GameObject CreateObject() {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.AddLast(obj);
        return obj;
    }

    public GameObject Get() {
        if (pool.Count == 0) {
            CreateObject();
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

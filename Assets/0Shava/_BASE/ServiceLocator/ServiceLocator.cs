using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : Singletone<ServiceLocator> {
    private Dictionary<Type, object> services = new();

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public static void Register<T>(T service) {
        if (Instance == null) {
            return;
        }

        var type = typeof(T);

        if (!Instance.services.ContainsKey(type)) {
            Instance.services[type] = service;
        }
    }

    public static T Get<T>() {
        if (Instance == null) {
            return default;
        }

        var type = typeof(T);

        if (Instance.services.ContainsKey(type)) {
            return (T)Instance.services[type];
        }

        //if (services.TryGetValue(type, out var service)) {
        //    return (T)service;
        //}

        Debug.LogError($"Service {type} not found");
        return default;
    }
}

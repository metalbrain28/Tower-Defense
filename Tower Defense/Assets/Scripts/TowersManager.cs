using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    public static TowersManager Instance { get; private set; }

    public IList<Tower> Towers { get; protected set; }

    protected void Awake()
    {
        if (Instance == null) {
            Debug.Log("TowerManger created");
            Instance = this;
            Towers = new List<Tower>();
        } else {
            Debug.LogWarning("There is already one manger in the scene. Removing this one.");
            DestroyImmediate(this);
        }
    }
}

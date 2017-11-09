using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Setup(Point gridPosition, Vector3 worldPosition, Transform parent)
    {
        this.GridPosition = gridPosition;
        transform.position = worldPosition;
        transform.SetParent(parent);
        DefensiveManager.Instance.Tiles.Add(gridPosition, this);
    }
}

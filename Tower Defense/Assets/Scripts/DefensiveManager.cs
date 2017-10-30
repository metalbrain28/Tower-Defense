using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveManager : MonoBehaviour {

    [SerializeField]
    private GameObject defensiveContainer;

    [SerializeField]
    private GameObject stateContainer;

    [SerializeField]
    private GameObject scoreContainer;

    public float WidthDefensiveContainer
    {
        get { return defensiveContainer.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }


    // Use this for initialization
    void Start () {
        CreateScoreContainer();
        CreateStateContainer();
        CreateDefensiveContainers();
	}

    // Update is called once per frame
    void Update() {

    }

    private void CreateStateContainer()
    {
        //Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4, Screen.height));
        GameObject newTile = Instantiate(stateContainer);
        //newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
    }

    private void CreateScoreContainer()
    {
        //Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/20, Screen.height));
        GameObject newTile = Instantiate(scoreContainer);
        //newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
    }

    private void CreateDefensiveContainers()
    {

        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4 + Screen.width/6, Screen.height/2 - Screen.height/4));

        for (int i = 0; i < 5; i++)
        {
            PlaceDefensiveContainers(i, startPosition);
        }
    }

    public void PlaceDefensiveContainers(int i, Vector3 startPosition)
    {
        GameObject newTile = Instantiate(defensiveContainer);
        newTile.transform.position = new Vector3(startPosition.x + WidthDefensiveContainer * i + (float)i/2 + (float)i/4, startPosition.y, float.Parse("-0.01"));
    }
}

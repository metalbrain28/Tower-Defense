using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefensiveManager : MonoBehaviour {

    [SerializeField]
    private GameObject defensiveContainer;

    [SerializeField]
    private GameObject stateContainer;

    [SerializeField]
    private GameObject scoreContainer;

    [SerializeField]
    private GameObject backgroundContainer;

    [SerializeField]
    private CameraMovement cameraMovement;

    public Button startGameButton;

    public GameOverManager gameOverManager;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    public float WidthDefensiveContainer
    {
        get { return defensiveContainer.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public float WidthBackgroundContainer
    {
        get { return backgroundContainer.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public float HeigthBackgroundContainer
    {
        get { return backgroundContainer.GetComponent<SpriteRenderer>().sprite.bounds.size.y; }
    }


    // Use this for initialization
    void Start () {

        Tiles = new Dictionary<Point, TileScript>();

        CreateBackgroundContainer();
        CreateScoreContainer();
        CreateStateContainer();
        CreateDefensiveContainers();

        // Start button click action binding
        Button btn = startGameButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Here, we should start a new enemy wave
        Debug.Log("Start level");
        // Then hide the button
        startGameButton.gameObject.SetActive(false);

        gameOverManager.ShowGameOver(123);
    }

    // Update is called once per frame
    void Update() {

    }

    private void CreateBackgroundContainer()
    {
        
        Vector3 maxTile = Vector3.zero;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        GameObject newTile = Instantiate(backgroundContainer);
        maxTile = newTile.transform.position = new Vector3(startPosition.x, startPosition.y, 0);
        cameraMovement.SetLimits(new Vector3(maxTile.x + WidthBackgroundContainer, maxTile.y - HeigthBackgroundContainer));

        newTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    private void CreateStateContainer()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4 + Screen.width/12, Screen.height - Screen.height / 22));
        GameObject newTile = Instantiate(stateContainer);
        newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
    }

    private void CreateScoreContainer()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/24, Screen.height - Screen.height/30));
        GameObject newTile = Instantiate(scoreContainer);
        newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
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
        //GameObject newTile = Instantiate(defensiveContainer);
        //newTile.transform.position = new Vector3(startPosition.x + WidthDefensiveContainer * i + (float)i / 2 + (float)i / 4, startPosition.y, float.Parse("-0.01"));
        //select prefab and click add component to add TypeScript
        TileScript newTile = Instantiate(defensiveContainer).GetComponent<TileScript>();
        newTile.Setup(new Point(i, 0), new Vector3(startPosition.x + WidthDefensiveContainer * i + (float)i / 2 + (float)i / 4, startPosition.y, float.Parse("-0.01")));
        Tiles.Add(new Point(i, 0), newTile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefensiveManager : Singleton<DefensiveManager>
{

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

    [SerializeField]
    private Transform mapObjects;

    [SerializeField]
    private Text currencyText;

    [SerializeField]
    private Text gameLevelText;

    [SerializeField]
    private Text escapedEnemiesText;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private GameOverManager gameOverManager;

    [SerializeField]
    private TowersManager towersManager;

    private int currency;

    private int gameLevel;

    private int escapedEnemies;

    private int maxNumberOfEscapedEnemies;

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

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyText.text = value.ToString() + " $";
        }
    }

    public int GameLevel
    {
        get
        {
            return gameLevel;
        }

        set
        {
            this.gameLevel = value;
            this.gameLevelText.text = "L " + value.ToString();
        }
    }

    public int EscapedEnemies
    {
        get
        {
            return escapedEnemies;
        }

        set
        {
            this.escapedEnemies = value;
            this.escapedEnemiesText.text = "Escaped " + value.ToString() + "/" + MaxNumberOfEscapedEnemies.ToString();
        }
    }

    public int MaxNumberOfEscapedEnemies
    {
        get
        {
            return maxNumberOfEscapedEnemies;
        }

        set
        {
            this.maxNumberOfEscapedEnemies = value;
        }
    }


    // Use this for initialization
    public void Start () {

        Tiles = new Dictionary<Point, TileScript>();

        CreateBackgroundContainer();
        CreateScoreContainer();
        CreateStateContainer();
        CreateDefensiveContainers();

        Currency = 50;
        GameLevel = 1;
        MaxNumberOfEscapedEnemies = 10;
        EscapedEnemies = 0;

        // Start button click action binding
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartGame()
    {
        // Here, we should start a new enemy wave
        Debug.Log("Start level");
        // Then hide the button
        startButton.gameObject.SetActive(false);

        WaveSpawner.Instance.StartEnemyWave();
        
    }

    public void EndGame()
    {
        gameOverManager.ShowGameOver(123);
    }

    private void CreateBackgroundContainer()
    {
        Vector3 maxTile = Vector3.zero;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        GameObject newTile = Instantiate(backgroundContainer);
        maxTile = newTile.transform.position = new Vector3(startPosition.x, startPosition.y, 0);
        newTile.transform.SetParent(mapObjects);
        cameraMovement.SetLimits(new Vector3(maxTile.x + WidthBackgroundContainer, maxTile.y - HeigthBackgroundContainer));
        newTile.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }

    private void CreateStateContainer()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/4 + Screen.width/12, Screen.height - Screen.height / 22));
        GameObject newTile = Instantiate(stateContainer);
        newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
        newTile.transform.SetParent(mapObjects);
    }

    private void CreateScoreContainer()
    {
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/24, Screen.height - Screen.height/30));
        GameObject newTile = Instantiate(scoreContainer);
        newTile.transform.position = new Vector3(startPosition.x, startPosition.y, float.Parse("-0.01"));
        newTile.transform.SetParent(mapObjects);
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
        newTile.Setup(new Point(i, 0), new Vector3(startPosition.x + WidthDefensiveContainer * i + (float)i / 2 + (float)i / 4, startPosition.y, float.Parse("-0.01")), mapObjects);
    }


    public void towerDragged(Tower towerButton)
    {
        Currency -= towerButton.CostToBuy;
    }

    public void towerSale(Tower towerButton)
    {
        Currency += towerButton.CostToBuy*(towerButton.SpriteIndex + 1);
    }

    public bool checkCurrencyBuy(Tower tower)
    {
        return tower.CostToBuy <= Currency;
    }

    public bool checkCurrencyUpgrade(Tower tower)
    {
        return tower.CostToUpgrade <= Currency;
    }

    public void enemiesEscaped()
    {

        if (EscapedEnemies >= MaxNumberOfEscapedEnemies)
        {
            EndGame();
            for (int i = 0; i < WaveSpawner.Instance.objects.Length; i++)
            {
                Destroy(WaveSpawner.Instance.objects[i]);
            }

            int total = towersManager.Towers.Count;
            while(total > 0) {
                towersManager.Towers[0].Sell();
            }

            //Destroy(gameObject);
            //WaveSpawner.Instance.OnEnemyDeath();
            EscapedEnemies = 10;
            return;
        }

        EscapedEnemies += 1;

    }
}

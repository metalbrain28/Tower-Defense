using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour {
    [SerializeField] private GameObject upgradeMenuPrefab = null;
    [SerializeField] private Sprite[] upgradeStates = null;
    private int spriteIndex = 0;
    [SerializeField]
    private int speed = 0;
    [SerializeField]
    private int range = 0;
    [SerializeField]
    private int costToBuy = 0;
    [SerializeField]
    private int costToUpgrade = 0;

    public int CostToBuy
    {
        get
        {
            return costToBuy;
        }

        set
        {
            costToBuy = value;
        }
    }

    public int CostToUpgrade
    {
        get
        {
            return costToUpgrade;
        }

        set
        {
            costToUpgrade = value;
        }
    }

    public int SpriteIndex
    {
        get
        {
            return spriteIndex;
        }

        set
        {
            spriteIndex = value;
        }
    }


    // Use this for initialization
    //private BoxCollider2D boxCollider;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Buy() {

    }
    

    public void Upgrde() {
        if (upgradeStates != null && upgradeStates.Length > SpriteIndex && DefensiveManager.Instance.checkCurrencyUpgrade(this)) {
            DefensiveManager.Instance.towerDragged(this);
            GetComponent<SpriteRenderer>().sprite = upgradeStates[SpriteIndex];
            SpriteIndex++;
        }
    }
    public void Sale() {
        DefensiveManager.Instance.towerSale(this);
        TowersManager.Instance.Towers.Remove(this);
        Destroy(gameObject);    
    }
    public void OnMouseDown() {
        if (upgradeMenuPrefab != null) {
            GameObject go = Instantiate<GameObject>(upgradeMenuPrefab);

            UpgradeMenu upgradeMenu = go.GetComponentInChildren<UpgradeMenu>();
            upgradeMenu.Initiate(this);
            upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
 
}

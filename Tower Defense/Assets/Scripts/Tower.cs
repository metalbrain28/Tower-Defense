using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour {
    [SerializeField] private GameObject upgradeMenuPrefab = null;
    [SerializeField] private Sprite[] upgradeStates = null;
    private int spriteIndex = 0;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float range = 0f;
    [SerializeField]
    private float costToBuy = 0f;
    [SerializeField]
    private float costToUpgrade = 0f;


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
        if (upgradeStates != null && upgradeStates.Length > spriteIndex) {
            GetComponent<SpriteRenderer>().sprite = upgradeStates[spriteIndex];
            spriteIndex++;
        }
    }
    public void Sale() {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField]
    private bool faceTarget = true;
    [SerializeField]
    private GameObject upgradeMenuPrefab = null;
    [SerializeField]
    private Sprite[] upgradeStates = null;
    private int spriteIndex = 0;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float damage = 0f;
    [SerializeField]
    private float range = 1f;
    [SerializeField]
    private int costToBuy = 0;
    [SerializeField]
    private int costToUpgrade = 0;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup Fields")]
    private string enemyTag = "Player";
    public Transform target = null;

    public GameObject bulletPrefab;

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

    public bool Deployed { get; private set; }

    protected virtual void Start()
    {
            Debug.Log("T1");

        Deployed = false;
    }

    public virtual void OnDeploy()
    {
        Debug.Log("U even dep?");
        Deployed = true;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target == null || !Deployed)
            return;

        Debug.Log("U even target?");
        if (faceTarget)
        {

            Debug.Log("T1");
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);

    }

    void UpdateTarget()
    {
        GameObject[] enemiesInRange = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }


    }

    public void Buy() {

    }

    public void Upgrde()
    {
        if (upgradeStates != null && upgradeStates.Length > SpriteIndex && DefensiveManager.Instance.checkCurrencyUpgrade(this))
        {
            DefensiveManager.Instance.towerDragged(this);
            GetComponent<SpriteRenderer>().sprite = upgradeStates[SpriteIndex];
            SpriteIndex++;

            damage *= 2;
        }
    }
    public void Sell()
    {
        DefensiveManager.Instance.towerSale(this);
        TowersManager.Instance.Towers.Remove(this);
<<<<<<< HEAD
        Destroy(gameObject);   
    }

    public void Reinit()
    {
        TowersManager.Instance.Towers.Remove(this);
        Destroy(gameObject);
    }

    public void OnMouseDown() {
        if (upgradeMenuPrefab != null) {
=======
        Destroy(gameObject);
    }
    public void OnMouseDown()
    {
        if (upgradeMenuPrefab != null)
        {
>>>>>>> ff6dc029be08c6d39c67fe1f6ad1e954792a8fdf
            GameObject go = Instantiate<GameObject>(upgradeMenuPrefab);

            UpgradeMenu upgradeMenu = go.GetComponentInChildren<UpgradeMenu>();
            upgradeMenu.Initiate(this);
            upgradeMenu.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        }
    }
}

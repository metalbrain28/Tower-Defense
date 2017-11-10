using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Singleton<EnemyGun> {

    public GameObject EnemyBullet;

	// Use this for initialization
	void Start () {
        Invoke("FireBullet",1.3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FireBullet()
    {
        GameObject defense = GameObject.Find("Tower");
        if(defense != null)
        {
            GameObject bullet =(GameObject) Instantiate(EnemyBullet);
            bullet.transform.position = transform.position;
            Vector2 direction = defense.transform.position - bullet.transform.position;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private Tower tower = null;

	// Use this for initialization
	public void Initiate (Tower clickedTower) {
        tower = clickedTower;
    }

    public void UpogradeLevel1() {
        if (tower != null)
            tower.Upgrde();

        Debug.Log("UpogradeLevel1");
        Destroy(gameObject);
    }
    public void SaleTower() {
        tower.Sale();
        Destroy(gameObject);

    }
    public void ExitMenu() {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

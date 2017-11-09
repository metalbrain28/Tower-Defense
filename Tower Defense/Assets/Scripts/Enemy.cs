using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy> {

     public GameObject[] waypoints;
     public float life ;
     public float speed = 0.5f;
     public int targetWaypoint;
     public bool isBoss = false;
    protected bool dead;
    Animator animator;
   /* public GameObject projectile;
    public Vector2 velocity;
    public Vector2 offset = new Vector2(0.2f, 0.3f);*/

    // Use this for initialization
    void Start () {
         if (isBoss == true)
             life = 200f;
         else
             life = 100f;
        this.transform.position = waypoints[0].transform.position;
        animator = gameObject.GetComponent<Animator>() as Animator;
        animator.Play("goRight");
        HealthBar.maxHealth = life;

    }
     
    // Update is called once per frame
    void Update () {
        
        float step = speed*Time.deltaTime ;
        if (targetWaypoint == 12)
        {
            DefensiveManager.Instance.enemiesEscaped();
            Killed();
        } else {
            if (gameObject.transform.position.y < waypoints[targetWaypoint].transform.position.y)
            {
                animator.Play("goUp");
            }
            if (gameObject.transform.position.y > waypoints[targetWaypoint].transform.position.y)
            {
                animator.Play("goDown");
            }
            if (gameObject.transform.position.x < waypoints[targetWaypoint].transform.position.x)
            {
                animator.Play("goRight");
            }
            /*  if (isBoss)
              {
                 GameObject go =  Instantiate(projectile, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
              }
               */

            transform.position = Vector3.MoveTowards(transform.position, waypoints[targetWaypoint].transform.position, step);
            if (gameObject.transform.position == waypoints[targetWaypoint].transform.position)
                targetWaypoint++;

        }
        
      
    }

    public event System.Action OnDeath;

    
	public void Killed(){
        //EnemyManager.Instance.enemies.Remove(this.gameObject);
        dead = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
        animator.Play("die");
        Destroy(this.gameObject);
	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        HealthBar.health -= 10f;
        if(HealthBar.health <= 0)
        {
            animator.Play("die");
        }
    }
}

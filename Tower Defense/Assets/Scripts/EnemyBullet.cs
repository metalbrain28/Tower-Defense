using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {


    float speed;
    Vector2 b_direction;
    bool isReady;

    //set default values for bullet speed and state
    private void Awake()
    {
        speed = 0.5f;
        isReady = false;
    }
   public void SetDirection(Vector2 direction)
    {
        b_direction = direction.normalized;
        isReady = true;
    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (isReady)
        {
            Vector2 position = transform.position;
            position += b_direction * speed * Time.deltaTime;
            transform.position = position;
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if (transform.position.x < min.x || transform.position.x>max.x
                || transform.position.y<min.y || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }

        }
	}
}

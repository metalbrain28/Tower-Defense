using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    //private Enemy _target;

    private float bulletSpeed = 10.0f;

    public void Seek(Transform _target) {
        target = _target;
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        
        float distanceThisFrame = bulletSpeed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame) {
            HitTarget();
            target.gameObject.SetActive(false);
            DefensiveManager.Instance.Currency = DefensiveManager.Instance.Currency + 3;
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        
    }

    void HitTarget() {
        Debug.Log("HIT");
        Destroy(gameObject);
        Destroy(target);
    }
}

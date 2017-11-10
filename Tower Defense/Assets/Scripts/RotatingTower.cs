using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatingTower : Tower {
    private float RotateSpeed = 1f;
    private float Radius = 1f;

    private Vector2 centre;
    private float angle;
    

    public override void OnDeploy() {
        base.OnDeploy();
        centre = transform.position;
    }

    protected override void Update() {
        base.Update();

        if (!Deployed)
            return;

        angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = centre + offset;
    }
}
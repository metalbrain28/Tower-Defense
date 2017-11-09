using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField]
    private Tower prefabToSpawn = null;
    private Tower instance = null;
    private BoxCollider2D boxCollider;
    private bool okToPlace = true;

    public void OnBeginDrag(PointerEventData eventData) {
        if (prefabToSpawn != null) {
            instance = Instantiate<Tower>(prefabToSpawn);
        }
    }

    public void OnDrag(PointerEventData data) {
        if (instance != null) {
            Vector3 wsPosition = Camera.main.ScreenToWorldPoint(data.position);
            //wsPosition.x = (int)wsPosition.x + 0.5f;
            //wsPosition.y = (int)wsPosition.y - 0.5f;
            wsPosition.z = 0.5f;
            instance.transform.position = wsPosition;

            boxCollider = instance.GetComponent<BoxCollider2D>();
            Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
            if (overlap.Length > 1) {
                instance.GetComponent<Renderer>().material.color = Color.red;
                okToPlace = false;
            } else {
                instance.GetComponent<Renderer>().material.color = Color.white;
                okToPlace = true;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log(okToPlace);
        //if (instance != null && TowersManager.Instance != null) {
            if (okToPlace && DefensiveManager.Instance.checkCurrencyBuy(instance)) {
                TowersManager.Instance.Towers.Add(instance);
                DefensiveManager.Instance.towerDragged(instance);
                instance = null;
            } else {
                Destroy(instance.gameObject);
            }
            

        //}
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour {

    public float moveSpeed = 10f;
    public static event Action OnPickUpCoin;

    private void Start () {
        StartCoroutine (Move ());
    }

    private IEnumerator Move () {
        while (true) {
            transform.Translate (moveSpeed * Time.deltaTime * Vector3.down, Space.World);

            if (transform.position.y < -23f){
                Destroy(gameObject);
            }

            yield return null;
        }
    }
    
    private void OnTriggerEnter2D (Collider2D other) {
        
        if (other.CompareTag ("Player")) {
            //do somthing
            AudioManager.Instance.Play("PickUp");
            OnPickUpCoin();
            Destroy (gameObject);
        }
    }
}
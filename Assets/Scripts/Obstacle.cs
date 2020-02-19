using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    
    public float moveSpeed = 5f;

    private void Start() {
        StartCoroutine(Move());
    }

    private IEnumerator Move(){
        while(true){
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.down, Space.World);

            if (transform.position.y < -23f){
                Destroy(gameObject);
            }

            yield return null;
        }
    }

}
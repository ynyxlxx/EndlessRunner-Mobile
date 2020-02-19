using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Camera mainCamera;
    public Transform followedObject;
    public float followOffset;
    public bool rotateTheCamera;

    [SerializeField]
    private float cameraMoveSpeed = 5f;

    private void Awake () {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }
    }

    private void Start () {
        //StartCoroutine (FollowTheCam ());
        StartCoroutine (RotateCamera ());
    }

    private IEnumerator FollowTheCam () {
        while (true) {
            mainCamera.transform.position = Vector3.Lerp (
                mainCamera.transform.position,
                new Vector3 (followedObject.position.x, followedObject.position.y + followOffset, mainCamera.transform.position.z),
                Time.deltaTime * cameraMoveSpeed);

            yield return null;
        }
    }

    private IEnumerator RotateCamera () {
        while(rotateTheCamera){
            // mainCamera.transform.rotation *= Quaternion.Euler(0, 0, 0.1f);
            if (followedObject.transform.position.x < 0){
                mainCamera.transform.eulerAngles += Vector3.forward * Time.deltaTime * 2f ;
            }else if (followedObject.transform.position.x > 0){
                mainCamera.transform.eulerAngles -= Vector3.forward * Time.deltaTime * 2f;
            }

            yield return null;
        }
    }
}
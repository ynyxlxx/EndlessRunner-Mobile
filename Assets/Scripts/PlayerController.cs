using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float horizontalMovementRestrict = 10f;

    private float minRotationAngle = 20f;
    private float maxRotationAngle = 160f;
    private float mouseToTransformDstThreshold = 1f;
    private float rotationOffset = -90f;

    [HideInInspector]
    public bool slowMotion { get; private set; }

    public float slowDownFactor = 0.05f;
    public float slowMotionLength = 2f;

    private void Start () {
        slowMotion = false;

        StartCoroutine (MouseFollow (minRotationAngle, maxRotationAngle));
        StartCoroutine (SlowMotion ());
    }

    private IEnumerator MouseFollow (float minAngle, float maxAngle) {

        while (true) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            mousePos.x = Mathf.Clamp (mousePos.x, -Screen.width / 2f, Screen.width / 2f);
            Vector3 targetPos = new Vector3 (mousePos.x, transform.position.y + 5, mousePos.z);
            Vector3 dirToTarget = (targetPos - transform.position);

            float angle = 0f;
            if (dirToTarget.sqrMagnitude > mouseToTransformDstThreshold * mouseToTransformDstThreshold) {
                dirToTarget = dirToTarget.normalized;
                angle = Mathf.Atan2 (dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler (0, 0, angle + rotationOffset);
                transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.unscaledDeltaTime * rotationSpeed);
            }

            if (angle > 90f) {
                float ratio = (angle - 90f) / 90f;
                transform.Translate (2 * ratio * moveSpeed * Time.unscaledDeltaTime * Vector2.left, Space.World);
            } else if (angle < 90f) {
                float ratio = -(angle - 90f) / 90;
                transform.Translate (2 * ratio * moveSpeed * Time.unscaledDeltaTime * Vector2.right, Space.World);
            }

            float clampedX = Mathf.Clamp (transform.position.x, -horizontalMovementRestrict / 2f, horizontalMovementRestrict / 2f);
            transform.position = new Vector3 (
                clampedX,
                transform.position.y,
                transform.position.z
            );

            yield return null;
        }

    }

    // private IEnumerator SlowMotion () {

    //     while (true) {

    //         if (Input.GetMouseButtonDown (0)) { 
    //             if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint (Input.mousePosition))){
    //                 slowMotion = true;
    //             }
    //             slowMotion = true;
    //             Time.timeScale = slowDownFactor;
    //             Time.fixedDeltaTime = Time.deltaTime * 0.02f;
    //         } else if (Input.GetMouseButtonUp (0)) {
    //             slowMotion = false;
    //         }

    //         if (slowMotion == false) {
    //             Time.timeScale += (1 / slowMotionLength) * Time.unscaledDeltaTime;
    //             Time.timeScale = Mathf.Clamp (Time.timeScale, 0, 1);
    //         }

    //         yield return null;
    //     }
    // }

    private IEnumerator SlowMotion () {
        while (true) {

            if (Input.GetMouseButton (0)) {
                Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);
                if (hit.collider != null) {
                    slowMotion = true;
                }
            }

            if (slowMotion){
                Time.timeScale = slowDownFactor;
                Time.fixedDeltaTime = Time.deltaTime * 0.02f;
                yield return new WaitForSeconds(0.3f);
                slowMotion = false;
            }

            Time.timeScale += (1 / slowMotionLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp (Time.timeScale, 0, 1);

            yield return null;
        }
    }

}
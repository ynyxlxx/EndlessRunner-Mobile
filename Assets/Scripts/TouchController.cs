using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public float rotationSpeed = 5f;
    public float moveSpeed = 10f;
    public float horizontalMovementRestrict = 10f;

    [HideInInspector]
    public bool slowMotion { get; private set; }

    public float slowDownFactor = 0.05f;
    public float slowMotionLength = 1f;

    private float doubleTapTime = 0.2f;

    private void Start () {

        StartCoroutine (Move ());
        StartCoroutine (SlowMotion ());
    }

    private IEnumerator Move () {
        while (true) {
            Vector2 rotTarget = this.transform.position;

            if (Input.touchCount > 0) {
                rotTarget = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
                rotTarget.y += 5;
            } else {
                rotTarget.y += 5;
            }

            Vector2 direction = rotTarget - (Vector2) transform.position;
            float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotationSpeed * Time.unscaledDeltaTime);

            if (angle + 90f > 90f) {
                float ratio = angle / 90f;
                transform.Translate (2 * ratio * moveSpeed * Time.unscaledDeltaTime * Vector2.left, Space.World);
            } else if (angle < 90f) {
                float ratio = -angle / 90;
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

    private IEnumerator SlowMotion () {
        while (true) {

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary) {
                Vector2 pos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
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
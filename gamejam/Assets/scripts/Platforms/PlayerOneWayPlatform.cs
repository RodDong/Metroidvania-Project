using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentPlatform;
    [SerializeField] private BoxCollider2D playerCollider;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            if (currentPlatform != null) {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "OneWayPlatform") {
            this.GetComponent<movement>().canJump = true;
        }
        if (other.gameObject.CompareTag("OneWayPlatform")) {
            currentPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("OneWayPlatform")) {
            currentPlatform = null;
        }
    }
    
    private IEnumerator DisableCollision() {
        PolygonCollider2D platformCollider = currentPlatform.GetComponent<PolygonCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}

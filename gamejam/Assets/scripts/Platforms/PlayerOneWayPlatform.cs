using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentPlatform;
    [SerializeField] private BoxCollider2D playerCollider;
    public bool isDropping = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S) 
        && gameObject.GetComponent<movement>().canJump) {
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
        // if (other.gameObject.CompareTag("OneWayPlatform")) {
        //    currentPlatform = null;
        // }
    }
    
    private IEnumerator DisableCollision() {
        currentPlatform.layer = LayerMask.NameToLayer("PlatformWithoutPlayerCollision");
        isDropping = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
        //PolygonCollider2D platformCollider = currentPlatform.GetComponent<PolygonCollider2D>();
        //Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.3f);
        currentPlatform.layer = LayerMask.NameToLayer("Platform");
        isDropping = false;
        //Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}

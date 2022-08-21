using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeVisualDetection : MonoBehaviour
{
    [SerializeField]
    GameObject soundDetection;
    [SerializeField]
    GameObject playerDustTrail;
    public float distance = 20f;
    public float startAngle = -30f;
    public float finishAngle = 30f;
    public int segments = 10;
    // direction:
    // = -1 -> left
    // = 1 -> right
    public int direction = -1;

    private void Update() {
        // wtf
        if (soundDetection.GetComponent<MeleeEnemyDetection>().isFacingRight) {
            direction = -1;
        } else {
            direction = 1;
        }

        RaycastSweep();
    }

    void RaycastSweep() {
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;

        int increment = Mathf.RoundToInt(Mathf.Abs(startAngle - finishAngle) / segments);

        RaycastHit2D hit;
        int playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        int wallLayerMask = 1 << LayerMask.NameToLayer("InvisibleWall");
        for (float i = startAngle; i < finishAngle; i += increment) {
            targetPos = (Quaternion.Euler(0, 0, i) * Vector2.right * direction).normalized * distance + startPos;
            hit = Physics2D.Linecast(startPos, targetPos, playerLayerMask | wallLayerMask);
            //TODO: add wall layer
            if (hit && hit.collider.tag == "player" && playerDustTrail.GetComponent<DustTrailVFX>().playing) {
                MeleeEnemyDetection detection = soundDetection.GetComponent<MeleeEnemyDetection>();
                if (!detection.hasTarget) {
                    soundDetection.GetComponent<MeleeEnemyDetection>().position = new Vector3(hit.transform.position.x, hit.transform.position.y, 0);
                    detection.hasTarget = true;
                    Invoke("loseTarget",3f);
                }
            }
            Debug.DrawLine(startPos, targetPos, Color.green);
        }
    }

    void loseTarget(){
        soundDetection.GetComponent<MeleeEnemyDetection>().hasTarget = false;
    }
}

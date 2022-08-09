using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerVisualDetection : MonoBehaviour
{
    [SerializeField]
    GameObject soundDetection;
    [SerializeField]
    GameObject enemy;
    public float distance = 20f;
    public float startAngle;
    public float finishAngle;
    public int segments = 10;
    // direction:
    // = -1 -> left
    // = 1 -> right
    public int direction = -1;

    private void Update() {
        // wtf wtf
        


        RaycastSweep();
    }

    void RaycastSweep() {
        Vector3 startPos = transform.position;
        Vector3 targetPos = Vector3.zero;
        
        int increment = Mathf.RoundToInt(Mathf.Abs(startAngle - finishAngle) / segments);

        RaycastHit2D hit;
        for (float i = startAngle; i < finishAngle; i += increment) {
            targetPos = (Quaternion.Euler(0, 0, i) * Vector2.left).normalized * distance;
            if(Mathf.Abs(enemy.transform.eulerAngles.y) < 90 && targetPos.x > 0){
                targetPos.x *= -1;
            }else if (Mathf.Abs(enemy.transform.eulerAngles.y) > 90 && targetPos.x < 0){
                targetPos.x *= -1;
            }
            targetPos += startPos;
            // TODO: replace player with particle effect
            hit = Physics2D.Linecast(startPos, targetPos, 1 << LayerMask.NameToLayer("Player"));
            if (hit) {
                RangeEnemyDetection detection = soundDetection.GetComponent<RangeEnemyDetection>();
                if (!detection.hasTarget) {
                    soundDetection.GetComponent<RangeEnemyDetection>().position = hit.transform.position;
                    detection.hasTarget = true;
                    Invoke("loseTarget",3f);
                }
            }
            Debug.DrawLine(startPos, targetPos, Color.green);
        }
    }

    void loseTarget(){
        soundDetection.GetComponent<RangeEnemyDetection>().hasTarget = false;
    }
}
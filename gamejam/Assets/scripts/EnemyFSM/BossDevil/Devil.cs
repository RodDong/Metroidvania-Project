using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    [SerializeField] GameObject danmaku_prefab;
    [SerializeField] GameObject fires;
    private State currentState;
    private Animator animator;
    private GameObject target;
    private float moveSpeed;
    private float rushSpeed;
    private bool isFacingRight;

    void Start()
    {
        currentState = new FollowState();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("player");
        moveSpeed = 2.0f;
        rushSpeed = 15.0f;
    }

    void Update()
    {
        currentState.Execute(this);
    }

    public void ChangeState(State state)
    {
        currentState = state;
    }

    
    public abstract class State
    {
        public abstract void Execute(Devil enemy);
    }

    public class IdleState : State
    {
        public override void Execute(Devil enemy)
        {
            enemy.animator.Play("idle");
        }
    }

    public class FollowState : State {
        private float coolDown = 4.0f;
        public override void Execute(Devil enemy)
        {
            // in-state logic
            enemy.animator.Play("idle");
            var step = enemy.moveSpeed * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position, step);

            if (enemy.transform.position.x > enemy.target.transform.position.x && enemy.isFacingRight) {
                Vector3 temp = enemy.transform.localScale;
                temp.x *= -1;
                enemy.transform.localScale = temp;
                enemy.isFacingRight = !enemy.isFacingRight;
            }
            if (enemy.transform.position.x < enemy.target.transform.position.x && !enemy.isFacingRight) {
                Vector3 temp = enemy.transform.localScale;
                temp.x *= -1;
                enemy.transform.localScale = temp;
                enemy.isFacingRight = !enemy.isFacingRight;
            }

            // transitions
            if (enemy.gameObject.GetComponent<EnemyDamage>().isDead) {
                enemy.ChangeState(new DeathState());
            }

            if (coolDown > 0) {
                coolDown -= Time.deltaTime;
            }

            if (coolDown <= 0) {
                if (enemy.GetComponent<EnemyDamage>().getHP() >= 200) {
                    int randomNum = Random.Range(0, 100);
                    if (randomNum < 70) {
                        enemy.ChangeState(new RangeState());
                    } else {
                        enemy.ChangeState(new RushState());
                    }
                } else {
                    int randomNum = Random.Range(0, 100);
                    if (randomNum < 20) {
                        enemy.ChangeState(new FireState());
                    } else if (randomNum < 60) {
                        enemy.ChangeState(new RushState());
                    } else {
                        enemy.ChangeState(new RangeState());
                    }
                }
            }
        }
    }

    public class RushState : State
    {
        private bool hasRecord = false;
        private Vector3 targetPos;
        private float tangent;
        public override void Execute(Devil enemy)
        {
            // in-state logic
            enemy.animator.Play("rush");
            if (!hasRecord) {
                float xDiff = enemy.transform.position.x - enemy.target.transform.position.x;
                float yDiff = enemy.transform.position.y - enemy.target.transform.position.y;
                tangent = yDiff / xDiff;
                float degrees = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;
                enemy.transform.eulerAngles = new Vector3(0, 0, degrees);
                targetPos = enemy.transform.position;

                if (xDiff > 0) {
                    targetPos.x -= 20;
                    targetPos.y -= 20 * tangent;
                } else {
                    Vector3 temp = enemy.transform.localScale;
                    temp.x *= -1;
                    enemy.transform.localScale = temp;
                    enemy.isFacingRight = !enemy.isFacingRight;
                    targetPos.x += 20;
                    targetPos.y += 20 * tangent;
                }

                hasRecord = true;
            }

            var step = enemy.rushSpeed * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, step);

            // transitions
            if (Vector3.Distance(enemy.transform.position, targetPos) < 0.5f) {
                enemy.transform.eulerAngles = Vector3.zero;
                enemy.ChangeState(new FollowState());
            }

            if (enemy.gameObject.GetComponent<EnemyDamage>().isDead) {
                enemy.ChangeState(new DeathState());
            }
        }
    }

    public class RangeState : State
    {
        private float timer = 0.92f;
        private int count = 0;
        public override void Execute(Devil enemy)
        {
            // in-state logic
            enemy.animator.Play("range");

            if (timer > 0) {
                timer -= Time.deltaTime;
            }

            if (timer <= 0 && count != 2) {
                Instantiate(enemy.danmaku_prefab);
                count++;
                timer = 0.3f;
            }

            // transitions
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("range")
            && enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                enemy.ChangeState(new FollowState());
            }

            if (enemy.gameObject.GetComponent<EnemyDamage>().isDead) {
                enemy.ChangeState(new DeathState());
            }
        }
    }

    public class FireState : State
    {
        public override void Execute(Devil enemy)
        {
            // in-state logic
            enemy.animator.Play("fire");
            enemy.fires.SetActive(true);

            // transitions
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("fire")
            && enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                enemy.fires.SetActive(false);
                enemy.ChangeState(new FollowState());
            }

            if (enemy.gameObject.GetComponent<EnemyDamage>().isDead) {
                enemy.fires.SetActive(false);
                enemy.ChangeState(new DeathState());
            }
        }
    }

    public class DeathState : State {
        public override void Execute(Devil enemy) {
            // in-state logic
            enemy.animator.Play("death");

            if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("death")
            && enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                Destroy(enemy.gameObject);
                // play boss defeat animation
                // 播片
            }
        }
    }
}
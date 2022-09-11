using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    private State currentState;
    private Animator animator;
    private GameObject target;
    private float moveSpeed;
    private float rushSpeed;

    void Start()
    {
        currentState = new IdleState();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = 1.0f;
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
            if (Input.GetKeyDown(KeyCode.J)) {
                float xDiff = enemy.transform.position.x - enemy.target.transform.position.x;
                float yDiff = enemy.transform.position.y - enemy.target.transform.position.y;
                float degrees = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;
                enemy.transform.eulerAngles = new Vector3(0, 0, degrees);
                enemy.animator.Play("idleToRush");
            }
            
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("idleToRush")
            && enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                enemy.ChangeState(new RushState());
            }
        }
    }

    public class FollowState : State {
        public override void Execute(Devil enemy)
        {
            enemy.animator.Play("idle");
            var step = enemy.moveSpeed * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position, step);
        }
    }

    public class RushState : State
    {
        public override void Execute(Devil enemy)
        {
            enemy.animator.Play("rush");
            var step = enemy.rushSpeed * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position, step);
            // TO ADD: if collider with wall/player/ground, transit to follow state
            if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) < 0.5f) {
                enemy.transform.eulerAngles = Vector3.zero;
                enemy.ChangeState(new FollowState());
            }
        }
    }

    public class RangeState : State
    {
        public override void Execute(Devil enemy)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManAI : MonoBehaviour
{
    private FishState currentState;
    [HideInInspector]public Animator animator;
    void Start()
    {
        currentState = new Idle();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

}
public abstract class FishState{
        public abstract void Execute(FishManAI fish);
    }

    public class Idle : FishState{
        public override void Execute(FishManAI fish)
        {
            fish.animator.Play("Idle");
            
        }
    }

    public class SwordAttack : FishState{
        public override void Execute(FishManAI fish)
        {
            fish.animator.Play("SwordAttack");
        }
    }

    public class Walk : FishState{
        public override void Execute(FishManAI fish)
        {
            fish.animator.Play("Walk");
        }
    }

    public class HarpoonAttack : FishState{
        public override void Execute(FishManAI fish)
        {
            fish.animator.Play("Throw");
        }
    }

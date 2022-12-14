using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class ToadIdle : IState<Toad>
{
    #region Initialize

    public static ToadIdle Instance { get; private set; }

    static ToadIdle()
    {
        Instance = new ToadIdle();
    }

    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Idle");
    }

    public void Execute(Toad enemy)
    {
        enemy.ChangeState();
    }

    public void Exit(Toad enemy)
    {

    }

    #endregion
}

public class ToadJump : IState<Toad>
{
    #region Initialize

    public static ToadJump Instance { get; private set; }

    static ToadJump()
    {
        Instance = new ToadJump();
    }

    #endregion

    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Jump");
        enemy.Flip();
        enemy.Jump();
    }

    public void Execute(Toad enemy)
    {
        if (enemy.isFalling && enemy.coll.enabled == false)
        {
            enemy.Flip();
            enemy.EnableColliderWhileFalling();
        }
        // 到达顶端，准备下落
        if (enemy.isRising && enemy.transform.position.y >= enemy.maxJumpHeight)
        {
            enemy.TitanFall();
        }

        // 已经落地
        if (!enemy.isRising && !enemy.isFalling)
        {
            enemy.coll.enabled = true;
            enemy.ChangeState();
        }
    }

    public void Exit(Toad enemy)
    {
        enemy.isRising = enemy.isFalling = false;
    }

}

public class ToadAttack : IState<Toad>
{
    #region Initialize

    public static ToadAttack Instance { get; private set; }

    static ToadAttack()
    {
        Instance = new ToadAttack();
    }

    #endregion

    public async void Enter(Toad enemy) 
    {
        if (enemy.Flip()) {
            await Task.Delay(1000);
            enemy.animator.Play("Attack");
        } else {
            enemy.animator.Play("Attack");
        }
        
        enemy.tongueCol.enabled = true;
    }
    

    public void Execute(Toad enemy)
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            enemy.ChangeState();
        }
    }

    public void Exit(Toad enemy)
    {
        enemy.disableTongue();
    }

}

public class ToadDeath : IState<Toad>
{
    public static ToadDeath Instance { get; private set; }

    static ToadDeath()
    {
        Instance = new ToadDeath();
    }


    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Death");
        enemy.bossMusicController.Stop();
        enemy.bossSummonGators.DisableAll();
        enemy.portal.SetActive(true);
        enemy.portalText.SetActive(true);
        enemy.bossDefeatMenu.SetActive(true);
    }

    public void Execute(Toad enemy)
    {
    }

    public void Exit(Toad enemy)
    {
        enemy.backgroundMusicController.backgroundMusic1.time = 0f;
        enemy.backgroundMusicController.backgroundMusic2.time = 0f;
        enemy.backgroundMusicController.timer = 0f;
        enemy.backgroundMusicController.backgroundMusic1.Play();
    }
}

public class ToadRoar : IState<Toad>
{
    public static ToadRoar Instance { get; private set; }

    static ToadRoar()
    {
        Instance = new ToadRoar();
    }


    public void Enter(Toad enemy)
    {
        enemy.animator.Play("Roar");
        enemy.audioPlayer.Play();
    }

    public void Execute(Toad enemy)
    {
        enemy.Flip();
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            enemy.ChangeState();
        }
    }

    public void Exit(Toad enemy)
    {
        enemy.audioPlayer.Stop();
    }
}
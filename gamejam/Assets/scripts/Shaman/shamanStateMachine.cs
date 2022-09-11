using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shamanStateMachine : MonoBehaviour
{
    private ShamanState curState;
    [HideInInspector] public Animator animator;
    [SerializeField] public shamanDetection shamanDetect;
    [HideInInspector] public AnimatorStateInfo shamanAnimatorInfo;
    [SerializeField] public GameObject earthSpike;
    GameObject wave1, wave2;
    [SerializeField] ObjectPool wavePool;
    [HideInInspector] public bool isRight, waveInstantiated;
    private float waveCD = 3.0f;

    void Start()
    {
        //Shaman spawn facing left 
        isRight = false;
        animator = gameObject.GetComponent<Animator>();
        curState = new shamanIdle();
    }


    void Update()
    {
        shamanAnimatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        waveCD-=Time.deltaTime;
        isRight = shamanDetect.position.x - gameObject.transform.position.x > 0;
        if(shamanDetect.hasTarget && curState.getStateName()!="shamanWaveAttack"){
            waveCD = 6.0f;
            curState = new shamanEarthSpike();
        }else if(waveCD<=0.0f && !shamanDetect.hasTarget){
            waveCD = 6.0f;
            curState = new shamanWaveAttack();

        }else if ((curState.getStateName()=="shamanWaveAttack" && shamanAnimatorInfo.normalizedTime>=0.9f) || (curState.getStateName() == "shamanEarthSpike"&& shamanAnimatorInfo.normalizedTime>=0.9f)){
            curState = new shamanIdle();
        }
        curState.Execute(this);
    }
    public void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void instantiateWave(){
        Quaternion tempQuaternion1 = new Quaternion();
        Quaternion tempQuaternion2 = new Quaternion();
        tempQuaternion1.eulerAngles = new Vector3(0,180,0);
        tempQuaternion2.eulerAngles = new Vector3(0,0,0);
        wave1 = wavePool.Spawn(new Vector2(transform.position.x+5f, transform.position.y+4f), tempQuaternion1);
        wave2 = wavePool.Spawn(new Vector2(transform.position.x-5f, transform.position.y+4f), tempQuaternion2);
        wave2.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
        wave1.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
    }
}

public abstract class ShamanState{
    public abstract void Execute(shamanStateMachine shaman);
    public abstract string getStateName();
}

public class shamanIdle : ShamanState{
    public override void Execute(shamanStateMachine shaman){
        shaman.waveInstantiated = false;
        shaman.animator.Play("Idle");
    }
    public override string getStateName()
    {
        return "shamanIdle";
    }
}

public class shamanEarthSpike : ShamanState{
    public override void Execute(shamanStateMachine shaman)
    {
        shaman.rotateRelativeToPlayer();
        shaman.waveInstantiated = false;
        if(shaman.shamanAnimatorInfo.IsName("Idle")){
            shaman.animator.Play("RaiseArm");
        }
        else if(shaman.shamanAnimatorInfo.normalizedTime >= 0.4f && shaman.shamanAnimatorInfo.IsName("RaiseArm")){
            shaman.animator.Play("PutDown");
            shaman.earthSpike.GetComponent<thorn>().ResetAnimation();
            shaman.earthSpike.SetActive(true);
        }
    }
    public override string getStateName()
    {
        return "shamanEarthSpike";
    }
}

public class shamanWaveAttack : ShamanState{
    public override void Execute(shamanStateMachine shaman)
    {
        shaman.animator.Play("Floor");
        if(!shaman.waveInstantiated){
            shaman.instantiateWave();
        }
        shaman.waveInstantiated = true;
    }
    public override string getStateName()
    {
        return "shamanWaveAttack";
    }
}

public class shamanBuildPlatform : ShamanState{
    public override void Execute(shamanStateMachine shaman)
    {
        
    }
    public override string getStateName()
    {
        return "shamanBuildPlatform";
    }
}

public class shamanRainIce : ShamanState{
    public override void Execute(shamanStateMachine shaman)
    {
        
    }
    public override string getStateName()
    {
        return "shamanRainIce";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class shamanStateMachine : MonoBehaviour
{
    private ShamanState curState;
    [HideInInspector] public Animator animator;
    [SerializeField] public shamanDetection shamanDetect;
    [HideInInspector] public AnimatorStateInfo shamanAnimatorInfo;
    [SerializeField] public GameObject earthSpike;
    GameObject wave1, wave2;
    Collider2D bossCollider;
    [SerializeField] ObjectPool wavePool;
    [SerializeField] public GameObject icePlatform1, icePlatform2;
    [HideInInspector] public bool isRight, waveInstantiated;
    [SerializeField] GameObject iceShards;
    [SerializeField] GameObject exit1, exit2;
    [SerializeField] GameObject portal;
    private float waveCD = 3.0f;
    private float shardsCD = 5.0f;
    private int fullHP = 300;
    Quaternion faceRight = new Quaternion();
    Quaternion faceLeft = new Quaternion();
    Vector3 originPos;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "attackArea") {
            Debug.Log("HIT");
        }
    }

    void Start()
    {
        //Shaman spawn facing left 
        isRight = false;
        animator = gameObject.GetComponent<Animator>();
        curState = new shamanIdle();
        bossCollider = gameObject.GetComponent<Collider2D>();
        faceRight.eulerAngles = new Vector3(0, 0, 0);
        faceLeft.eulerAngles = new Vector3(0, 180, 0);
        originPos = gameObject.transform.position;
    }


    void Update()
    {
        shamanAnimatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        waveCD-=Time.deltaTime;
        shardsCD-=Time.deltaTime;
        isRight = shamanDetect.position.x - gameObject.transform.position.x > 0;
        
        bossCollider.enabled = (curState.getStateName() != "shamanBuildPlatform" || transform.position != originPos);
        if(gameObject.GetComponent<EnemyDamage>().getHP() > 3*fullHP/4){

            if(shamanDetect.hasTarget && curState.getStateName()!="shamanWaveAttack"){
                waveCD = 5.0f;
                curState = new shamanEarthSpike();
            }else if(waveCD<=0.0f && !shamanDetect.hasTarget){
                waveCD = 5.0f;
                curState = new shamanWaveAttack();
            }else if ((curState.getStateName()=="shamanWaveAttack" && shamanAnimatorInfo.normalizedTime>=0.9f) || (curState.getStateName() == "shamanEarthSpike"&& shamanAnimatorInfo.normalizedTime>=0.9f)){
                curState = new shamanIdle();
            }

        }
        else if((gameObject.GetComponent<EnemyDamage>().getHP() <= 3*fullHP/4 && gameObject.GetComponent<EnemyDamage>().getHP() >= fullHP/2) || (gameObject.GetComponent<EnemyDamage>().getHP() <= fullHP/4 && gameObject.GetComponent<EnemyDamage>().getHP() >= 0)){
            if(curState.getStateName() != "shamanRainIce" && curState.getStateName() != "shamanBuildPlatform"){
                curState = new shamanBuildPlatform();
            }
            else if(curState.getStateName() == "shamanRainIce" ||
                (curState.getStateName() == "shamanBuildPlatform" && shamanAnimatorInfo.normalizedTime>=0.9f)){
                if(gameObject.GetComponent<EnemyDamage>().getHP() <= fullHP/4 && gameObject.GetComponent<EnemyDamage>().getHP() >= 0){
                    gameObject.transform.position = icePlatform2.transform.position;
                    gameObject.transform.rotation = faceLeft;
                }else{
                    gameObject.transform.position = icePlatform1.transform.position;
                    gameObject.transform.rotation = faceRight;
                }
                if (shardsCD <= 0) {
                    shardsCD = 5.0f;
                    curState = new shamanRainIce();
                } else if (curState.getStateName() == "shamanRainIce" && shamanAnimatorInfo.normalizedTime>=3.0f) {
                    curState = new shamanBuildPlatform();
                }
            }
            
        }else if(gameObject.GetComponent<EnemyDamage>().getHP() <= fullHP/2 && gameObject.GetComponent<EnemyDamage>().getHP() > fullHP/4){
            gameObject.transform.position = originPos;
            if(shamanDetect.hasTarget && curState.getStateName()!="shamanWaveAttack"){
                waveCD = 5.0f;
                curState = new shamanEarthSpike();
            }else if(waveCD<=0.0f && !shamanDetect.hasTarget){
                waveCD = 5.0f;
                curState = new shamanWaveAttack();
            }else if ((curState.getStateName()=="shamanWaveAttack" && shamanAnimatorInfo.normalizedTime>=0.9f) 
            || (curState.getStateName() == "shamanEarthSpike"&& shamanAnimatorInfo.normalizedTime>=0.9f) 
            || curState.getStateName() == "shamanRainIce"){
                Debug.Log(2);
                curState = new shamanIdle();
            }

        }else if(gameObject.GetComponent<EnemyDamage>().getHP()<=0){
            curState = new shamanDeath();
            portal.SetActive(true);
            exit1.GetComponent<trapdoorRoom3>().isOpen = true;
            exit2.GetComponent<trapdoorRoom3>().isOpen = true;
        }
        
        curState.Execute(this);
    }
    public void rotateRelativeToPlayer()
    {
        if (isRight)
        {
            gameObject.transform.localRotation = faceLeft;
        }
        else
        {
            gameObject.transform.localRotation = faceRight;
        }
    }
    public void instantiateWave(){
        Quaternion tempQuaternion1 = new Quaternion();
        Quaternion tempQuaternion2 = new Quaternion();
        tempQuaternion1.eulerAngles = new Vector3(0,180,0);
        tempQuaternion2.eulerAngles = new Vector3(0,0,0);
        wave1 = wavePool.Spawn(new Vector2(transform.position.x+1f, transform.position.y+1.8f), tempQuaternion1);
        wave2 = wavePool.Spawn(new Vector2(transform.position.x-1f, transform.position.y+1.8f), tempQuaternion2);
        wave2.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3, ForceMode2D.Impulse);
        wave1.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse);
    }

    public async void summonIceShards() {
        for (int i = 0; i < iceShards.transform.childCount; i++) {
            await Task.Delay(200);
            if (gameObject.transform.eulerAngles.y == 0)
                iceShards.transform.GetChild(i).gameObject.SetActive(true);
            else
                iceShards.transform.GetChild(iceShards.transform.childCount - 1 - i).gameObject.SetActive(true);
        }
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
        else if(shaman.shamanAnimatorInfo.normalizedTime >= 0.8f && shaman.shamanAnimatorInfo.IsName("RaiseArm")){
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
        if (shaman.shamanAnimatorInfo.IsName("Raise Idle")) {
            shaman.animator.Play("PutDown");
        } else if (shaman.shamanAnimatorInfo.normalizedTime >= 0.8f && shaman.shamanAnimatorInfo.IsName("PutDown")) {
            shaman.animator.Play("Create Ice");
        } else if (!shaman.shamanAnimatorInfo.IsName("PutDown")) {
            shaman.animator.Play("Create Ice");
            shaman.icePlatform1.SetActive(true);
            shaman.icePlatform2.SetActive(true);
        }
    }
    public override string getStateName()
    {
        return "shamanBuildPlatform";
    }
}

public class shamanRainIce : ShamanState{
    public override void Execute(shamanStateMachine shaman)
    {
        if (shaman.shamanAnimatorInfo.normalizedTime >= 0.8f && shaman.shamanAnimatorInfo.IsName("RaiseArm")){
            shaman.animator.Play("Raise Idle");
        } else if (!shaman.shamanAnimatorInfo.IsName("Raise Idle")) { 
            shaman.animator.Play("RaiseArm");
            shaman.summonIceShards();
        } 
    }
    public override string getStateName()
    {
        return "shamanRainIce";
    }
}

public class shamanDeath : ShamanState{
    public override void Execute(shamanStateMachine shaman){
        shaman.animator.Play("Death");
    }
    public override string getStateName(){
        return "shamanDeath";
    }
}
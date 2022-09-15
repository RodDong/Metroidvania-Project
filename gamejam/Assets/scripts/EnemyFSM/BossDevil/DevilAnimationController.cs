using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DevilAnimationController : MonoBehaviour
{
    private bool hasPlayed;
    [SerializeField] CinemachineVirtualCamera bossCM;
    [SerializeField] GameObject devil;
    [SerializeField] Animator devilAnimator;
    [SerializeField] AudioSource backgroundMusic;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!hasPlayed && other.tag == "player") {
            if(!backgroundMusic.isPlaying)backgroundMusic.Play();
            ProcessAnimation();
            hasPlayed = true;
        }else if(other.tag == "player"){
            if(!backgroundMusic.isPlaying)backgroundMusic.Play();
            devil.SetActive(true);
        }
    }

    private void ProcessAnimation() {
        bossCM.Follow = null;
        bossCM.GetComponent<Animator>().Play("CamMove");
        devilAnimator.Play("intro");
        Invoke("EndAnimation", 4.5f);
    }

    private void EndAnimation() {
        devil.transform.parent = null;
        devil.transform.eulerAngles = new Vector3(0, 0, 0);
        Destroy(devilAnimator.gameObject);
        devil.GetComponent<SpriteRenderer>().enabled = true;
        devil.GetComponent<BoxCollider2D>().enabled = true;
        devil.GetComponent<Devil>().enabled = true;
        devil.GetComponent<EnemyDamage>().enabled = true;
    }
}

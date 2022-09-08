using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGatorSummoner : MonoBehaviour
{
    [SerializeField] EnemyDamage enemyHealth;
    public List<GameObject> meleeGators;
    public List<GameObject> rangerGators;
    bool hasSummonMelees;
    bool hasSummonRangers;
    private void Update() {
        if (enemyHealth.getHP() <= 3 * enemyHealth.originHP / 4 && !hasSummonRangers) {
            for (int i = 0; i < meleeGators.Count; i++) {
                // TODO: add summon animation && disable enemy AI during animation
                rangerGators[i].SetActive(true);
            }
            hasSummonRangers = true;
        }

        if (enemyHealth.getHP() <= enemyHealth.originHP / 2 && !hasSummonMelees) {
            for (int i = 0; i < meleeGators.Count; i++) {
                meleeGators[i].SetActive(true);
            }
            hasSummonMelees = true;
        }
    }

    public void DisableAll() {
        for (int i = 0; i < meleeGators.Count; i++) {
            meleeGators[i].SetActive(false);
        }
        for (int i = 0; i < rangerGators.Count; i++) {
            rangerGators[i].SetActive(false);
        }
    }
}
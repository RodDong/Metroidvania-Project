using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadVFX : MonoBehaviour
{
    [SerializeField] GameObject toadParent;
    [HideInInspector] public SpriteRenderer[] toadParts;
    [HideInInspector] public List<Color> toadPartColors;

    private void Start() {
        toadParts = toadParent.GetComponentsInChildren<SpriteRenderer>();
        foreach (var part in toadParts) {
            toadPartColors.Add(part.color);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "attackArea" && this.gameObject.layer != LayerMask.NameToLayer("projectile"))
        {
            foreach (var spriteRenderer in toadParts) {
                if (spriteRenderer != null)
                spriteRenderer.material.SetColor("_Color", new Color(1f, 0.6f, 0.6f, 1f));
            }
            Invoke("resetColor", 0.15f);
        }
    }

    private void resetColor()
    {
        for (int i = 0; i < toadParts.Length; i++) {
            if (toadParts[i] != null)
            toadParts[i].material.SetColor("_Color", toadPartColors[i]);
        }
    }
}

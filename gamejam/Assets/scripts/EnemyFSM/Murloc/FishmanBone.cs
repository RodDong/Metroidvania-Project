using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmanBone : MonoBehaviour
{
    [SerializeField] GameObject bone;
    private List<Vector3> bonePos = new List<Vector3>();
    private List<Quaternion> boneRot = new List<Quaternion>();

    private void Start() {
        Transform[] boneList = bone.GetComponentsInChildren<Transform>();
        for (int i = 0; i < boneList.Length; i++) {
            bonePos.Add(boneList[i].position);
            boneRot.Add(boneList[i].rotation);
        }
    }

    public void ResetBones() {
        int i = 0;
        foreach (Transform trans in bone.GetComponentsInChildren<Transform>()) {
            trans.position = bonePos[i];
            trans.rotation = boneRot[i];
            i++;
        }
    }
}

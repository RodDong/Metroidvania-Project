using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmanBone : MonoBehaviour
{
    [SerializeField] GameObject bone;
    private List<Vector3> bonePos = new List<Vector3>();
    private List<Quaternion> boneRot = new List<Quaternion>();

    private void Awake() {
        Transform[] boneList = bone.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < boneList.Length; i++) {
            bonePos.Add(boneList[i].position);
            boneRot.Add(boneList[i].rotation);
        }
    }

    public void ResetBones() {
        Transform[] boneList = bone.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < boneList.Length; i++) {
            boneList[i].position = bonePos[i];
            boneList[i].rotation = boneRot[i];
        }
    }
}

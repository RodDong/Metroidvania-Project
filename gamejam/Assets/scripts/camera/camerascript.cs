using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camerascript : MonoBehaviour
{
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] CinemachineCameraOffset cameraOffset;

    void Update()
    {
        offset.y = 0;
        if (Input.GetKey(KeyCode.W)) {
            offset.y = 10;
        }
        cameraOffset.m_Offset = Vector3.SmoothDamp(cameraOffset.m_Offset, offset, ref velocity, smoothTime);
    }
}

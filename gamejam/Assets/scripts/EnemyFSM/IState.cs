using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://blog.csdn.net/weixin_43757333/article/details/122858387
public interface IState
{
    void OnEnter();
    void OnUpdate();
    void OnExit();
}

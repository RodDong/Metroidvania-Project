using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://blog.csdn.net/weixin_43757333/article/details/122858387

// https://blog.csdn.net/weixin_33811961/article/details/88668257

public interface IState<T>
{
    void Enter(T enemy);
    void Execute(T enemy);
    void Exit(T enemy);
}

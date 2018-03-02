using System.Collections;
using UnityEngine;

public interface IState
{
    void EnterState(GameObject player);
    void UpdateState(GameObject player);
    void ExitState(GameObject player);
}



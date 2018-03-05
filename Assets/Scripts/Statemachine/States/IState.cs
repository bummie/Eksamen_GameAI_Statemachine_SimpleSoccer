using System.Collections;
using UnityEngine;

public interface IState
{

    /// <summary>
    /// Returns the name of the state
    /// Used for debugging
    /// </summary>
    /// <returns></returns>
    string GetStateName();
    
    /// <summary>
    /// Runs once the state has changed to
    /// </summary>
    /// <param name="player"></param>
    void EnterState(GameObject player);

    /// <summary>
    /// Runs every frame
    /// </summary>
    /// <param name="player"></param>
    void UpdateState(GameObject player);

    /// <summary>
    /// Runs once when the state exits
    /// </summary>
    /// <param name="player"></param>
    void ExitState(GameObject player);
}



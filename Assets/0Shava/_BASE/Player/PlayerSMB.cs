using System;
using UnityEngine;

public class PlayerSMB : StateMachineBehaviour {
    public static int shotHash = Animator.StringToHash("shot");
    public bool IsShot;
    public Action<bool> OnShot;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (stateInfo.tagHash == shotHash) {
            IsShot = true;
            //Debug.Log("enter shot " + Time.deltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (stateInfo.tagHash == shotHash) {
            IsShot = false;
            OnShot?.Invoke(false);
            //Debug.Log("exit shot " + Time.deltaTime);
        }
    }
}

//using System;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class GlobalKey : Singletone<GlobalKey> {
//    public InputActionReference inputQ, inputW, inputE, inputESC;

//    public Action OnKeyQ, OnKeyW, OnKeyE, OnESC;

//    private void Awake() {
//        if (inputQ) {
//            inputQ.action.started += ClickQ;
//        }

//        if (inputW) {
//            inputW.action.started += ClickW;
//        }

//        if (inputE) {
//            inputE.action.started += ClickE;
//        }

//        if (inputESC) {
//            inputESC.action.started += ClickEsc;
//        }
//    }

//    private void OnDestroy() {
//        if (inputQ) {
//            inputQ.action.started -= ClickQ;
//        }

//        if (inputW) {
//            inputW.action.started -= ClickW;
//        }

//        if (inputE) {
//            inputE.action.started -= ClickE;
//        }

//        if (inputESC) {
//            inputESC.action.started -= ClickEsc;
//        }
//    }

//    private void ClickQ(InputAction.CallbackContext _) {
//        //Debug.Log("ClickQ");
//        OnKeyQ?.Invoke();
//    }


//    private void ClickW(InputAction.CallbackContext _) {
//        //Debug.Log("ClickW");
//        OnKeyW?.Invoke();
//    }

//    private void ClickE(InputAction.CallbackContext _) {
//        //Debug.Log("ClickE");
//        OnKeyE?.Invoke();
//    }

//    private void ClickEsc(InputAction.CallbackContext _) {
//        Debug.Log("ClickEsc");
//        OnESC?.Invoke();
//    }
//}

//using UnityEngine;
//using YG;

//public class AdsVisibleHandle : MonoBehaviour {
//    void Awake() {
//        YG2.onOpenAnyAdv += OnOpenAnyAdv;
//        YG2.onCloseAnyAdv += OnCloseAnyAdv;
//    }

//    void OnDestroy() {
//        YG2.onOpenAnyAdv -= OnOpenAnyAdv;
//        YG2.onCloseAnyAdv -= OnCloseAnyAdv;
//    }

//    void OnOpenAnyAdv() {
//        YG2.PauseGame(true);
//        Time.timeScale = 0;
//    }

//    void OnCloseAnyAdv() {
//        YG2.PauseGame(false);
//        Time.timeScale = 1;
//    }
//}

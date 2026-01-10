using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyClick : TutorialStepBase {
    private string attackKey = "tutor_attack";
    public EnemyClick(TutorialView v) : base(v) {
    }

    public override async UniTask Use() {
        //var dir = view.standPoint.position - view.spawnPoint.position;
        //var go = Factory.Instance.SpawnSimpleEnemy(view.spawnPoint.position, Quaternion.LookRotation(dir));
        //Debug.DrawRay(go.transform.position, dir, Color.red, 10);
        //var enemy = go.GetComponent<EnemyController>();
        //enemy.Setup_Tutorial(view.standPoint.position, ColorType.Red);

        //await UniTask.WaitUntil(() => Vector3.Distance(go.transform.position, view.standPoint.position) < 0.3f);
        //enemy.Stop();
        //Utility.WorldToRect(enemy.TutorMarkPoint.position, view.GetComponent<RectTransform>(), out var p2);

        //await view.ShowMessage(Localization.Instance.Get(attackKey), p2, Vector2.up * 300, Utility.AnchorType.Default);
        //await view.ShowArrow(p2, Vector2.up * 150, Vector2.up * 120, Utility.AnchorType.Default);
        //Cursor.visible = false;
        //view.SetGraphics(true);
        //GameController.Instance.player.cursorAim.SetActive(true);
        //GameController.Instance.player.shot.SetActive(true);

        //await UniTask.WaitUntil(() => enemy.IsDead);
        //await UniTask.Delay(1000);

        //GameController.Instance.player.cursorAim.SetActive(false);
        //GameController.Instance.player.shot.SetActive(false);

        //await UniTask.Delay(500);
        //view.HideArrow();
        //await view.HideMessage();
        //await UniTask.Delay(500);

    }
}

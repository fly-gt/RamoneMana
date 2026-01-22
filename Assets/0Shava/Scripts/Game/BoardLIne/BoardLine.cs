using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class BoardLine : Singletone<BoardLine> {
    public ObjectPool pool;
    public List<GameObject> lines = new();

    public void Initialize() {
        transform.SetZ(1);
    }

    public async void DrawLine(Vector3 from, Vector3 to) {
        GameObject lineGo = await pool.Get();
        SpriteRenderer spriteRenderer = lineGo.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, 0);
        lines.Add(lineGo);
        lineGo.transform.position = from;
        lineGo.transform.up = to - from;
        lineGo.transform.position -= lineGo.transform.up * spriteRenderer.size.x * 0.5f; 
        var magnitude = (to - from).magnitude;
        DrawAsync(spriteRenderer, magnitude);
    }

    public void Clear() {
        foreach (var l in lines) {
            pool.Return(l);
        }

        lines.Clear();
    }

    private async void DrawAsync(SpriteRenderer spriteRenderer, float magnitude) {
        float duration = 0.1f;
        float t = 0;

        while (t <= duration) {
            await UniTask.Yield(destroyCancellationToken);    
            t += Time.deltaTime;
            var delta = t / duration;

            var lerp = Mathf.Lerp(0f, magnitude + 0.2f, delta);
            spriteRenderer.size = new Vector2(spriteRenderer.size.x, lerp);
        }
    }
}

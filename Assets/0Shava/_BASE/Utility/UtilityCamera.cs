using UnityEngine;

public static class UtilityCamera {
    public static Vector2 CameraWorldSize(this Camera camera) {
            return new Vector2(
                camera.orthographicSize * 2f * camera.aspect,
                camera.orthographicSize * 2f
            );

        //float worldHeight = gameCamera.orthographicSize * 2f;
        //float worldWidth = worldHeight * gameCamera.aspect;
    }
}

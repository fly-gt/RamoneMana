using System.Collections.Generic;
using UnityEngine;

public static class BoardUtility {
    public static void FillNeighbors(int width, int height, int index, HashSet<int> neighbors) {
        neighbors.Clear();
        
        int x = index % width;  // столбец
        int y = index / width;  // строка

        for (int dx = -1; dx <= 1; dx++) {
            for (int dy = -1; dy <= 1; dy++) {
                if (dx == 0 && dy == 0) continue; // пропускаем саму клетку

                int nx = x + dx;
                int ny = y + dy;

                // проверка границ
                if (nx >= 0 && nx < width && ny >= 0 && ny < height) {
                    int neighborIndex = ny * width + nx;
                    neighbors.Add(neighborIndex);
                }
            }
        }
    }
}

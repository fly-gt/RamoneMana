using UnityEngine;

public static class Sort {
    public static void QuickSort(RaycastHit[] array, int leftIndex, int rightIndex) {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[(leftIndex + rightIndex) / 2].distance;

        while (i <= j) {
            while (array[i].distance < pivot) {
                i++;
            }

            while (array[j].distance > pivot) {
                j--;
            }
            if (i <= j) {
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            QuickSort(array, leftIndex, j);
        if (i < rightIndex)
            QuickSort(array, i, rightIndex);
    }
}

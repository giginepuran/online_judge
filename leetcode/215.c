#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int *heap;
int heapMaxIndex;
int heapUsedIndex;

void heapSwap(int i, int j) {
    int buf;
    buf = heap[i];
    heap[i] = heap[j];
    heap[j] = buf;
    return;
}

void heapifyUp(int i) {
    while (i > 0) {
        int parent = (i - 1) / 2;
        if (heap[parent] > heap[i]) {
            heapSwap(parent, i);
            i = parent;
        } else {
            break;
        }
    }
}

void heapifyDown(int i) {
    int smallest = i;
    int left = 2 * i + 1;
    int right = 2 * i + 2;

    if (left <= heapUsedIndex && heap[left] < heap[smallest])
        smallest = left;
    if (right <= heapUsedIndex && heap[right] < heap[smallest])
        smallest = right;

    if (smallest != i) {
        heapSwap(i, smallest);
        heapifyDown(smallest);
    }
}

void heapAdd(int target) {
    heapUsedIndex++;
    if (heapMaxIndex < heapUsedIndex)
        return;
    heap[heapUsedIndex] = target;
    heapifyUp(heapUsedIndex);
    return;
}

int findKthLargest(int* nums, int numsSize, int k) {
    int i;
    int ans;
    
    heap = (int*)malloc(sizeof(int) * k);
    heapMaxIndex = k-1;
    heapUsedIndex = -1;

    for (i = 0; i < numsSize; i++) {
        if (heapUsedIndex == heapMaxIndex) {
            if (nums[i] <= heap[0])
                continue; // do nothing to heap
            // To keep only k elements in heap, replaced min value with new value
            heap[0] = nums[i];
            heapifyDown(0);
        } else {
            heapAdd(nums[i]);
        }
    }
    ans = heap[0];
    free(heap);
    return ans;
}

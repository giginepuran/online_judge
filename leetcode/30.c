/**
 * Note: The returned array must be malloced, assume caller calls free().
 */

#include <stdio.h>
#include <string.h> 
#include <stdlib.h>
#define TABLE_SIZE 5000 // Example size of hash table

unsigned long hash(const char *key) {
    unsigned long hash = 0;
    int len = strlen(key);
    int i;

    for (i = 0; i < len; i++) {
        hash = (hash * 31) + (key[i] - 'a' + 1) * (i + 1);
    }

    return hash % TABLE_SIZE;
}

typedef struct Entry {
    char *key;
    int values[10000];
    struct Entry *next; // For handling collisions with chaining
} Entry;

typedef struct Dictionary {
    Entry *table[TABLE_SIZE];
} Dictionary;

Dictionary *create_dictionary() {
    int i;
    Dictionary *dict = (Dictionary *)malloc(sizeof(Dictionary));

    for (i = 0; i < TABLE_SIZE; i++) {
        dict->table[i] = NULL;
    }

    return dict;
}

void insert(Dictionary *dict, const char *key, int values[]) {
    unsigned int index = hash(key);
    Entry *new_entry = (Entry *)malloc(sizeof(Entry));

    new_entry->key = strdup(key); // Duplicate key for storage
    memcpy(new_entry->values, values, sizeof(int) * 10000); // Copy values array
    new_entry->next = dict->table[index];
    dict->table[index] = new_entry;
}

int *lookup(Dictionary *dict, const char *key) {
    unsigned int index = hash(key);

    Entry *entry = dict->table[index];
    while (entry != NULL && strcmp(entry->key, key) != 0) {
        entry = entry->next;
    }

    if (entry)
        return entry->values;
    return NULL;
}

void free_dictionary(Dictionary *dict) {
    if (!dict) return; // Check if the dictionary is NULL

    for (int i = 0; i < TABLE_SIZE; i++) {
        Entry *entry = dict->table[i];
        while (entry != NULL) {
            Entry *temp = entry;
            entry = entry->next; // Move to the next entry
            free(temp->key);     // Free the duplicated key
            free(temp);          // Free the Entry itself
        }
    }
    free(dict); // Finally free the Dictionary structure itself
}

enum COMPARE_RESULT {
    COMPARE_RESULT_NOTYET,
    COMPARE_RESULT_SUCCESS,
    COMPARE_RESULT_FAIL,
};
Dictionary *compare_result = NULL;
// ------------------------------^^^definition of Dict^^^---------------------------

typedef struct Node {
    char *word;
    int *word_len;
    struct Node *next;
} Node;

int isMatchNode(char *s, Node *node) {
    int ret;
    if (strncmp(s, word, strlen(word)))
}


int *ans;
int sLen;

int* findSubstring(char* s, char** words, int wordsSize, int* returnSize) {
    int i = 0, ansSize = 0;
    ans = (int*)malloc(sizeof(int) * 10000);

    
    *returnSize = ansSize;
    return ans;
}

int main() {
    char *s = "barfoothefoobarman";
    char *words[] = { "foo", "bar" };
    int wordsSize = 2;
    int i;

    int *ans, returnSize;
    ans = findSubstring(s, words, wordsSize, &returnSize);

    for (i = 0; i < returnSize; i++) {
        printf("%d ", ans[i]);
    }
    return 0;
}



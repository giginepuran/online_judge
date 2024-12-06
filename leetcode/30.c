//#define DEBUG

/**
 * Note: The returned array must be malloced, assume caller calls free().
 */

#include <stdio.h>
#include <string.h> 
#include <stdlib.h>
#define TABLE_SIZE 5000 // Example size of hash table

int hash(const char *key) {
    int hash = 0;
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
    int index = hash(key);
    Entry *new_entry = (Entry *)malloc(sizeof(Entry));

    new_entry->key = strdup(key); // Duplicate key for storage
    memcpy(new_entry->values, values, sizeof(int) * 10000); // Copy values array
    new_entry->next = dict->table[index];
    dict->table[index] = new_entry;
}

int *lookup(Dictionary *dict, const char *key) {
    int index = hash(key);

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

    int i;
    for (i = 0; i < TABLE_SIZE; i++) {
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
// ------------------------------^^^definition of Dict^^^---------------------------
typedef struct Node {
    int set_index;
    char *word;
    size_t word_len;
    struct Node *previous;
    struct Node *next;
    int isMatchNode;
} Node;

typedef struct List {
    Node *head;
    Node *tail;
} List;

enum DIRECTION {
    HEAD,
    TAIL,
    BEFORE,
    AFTER,
};

Dictionary *compare_result = NULL;
int ans[10000];
int done[10000];
int ansSize = 0;
int sLen = 0;
List *holdList;
List *setList;

void printNodes(Node *head) {
    Node *p;
    for (p = head; p; p = p->next)
        printf("%s\n", p->word);
    return;
}

Node *createNode(char *word) {
    Node *p = (Node*)malloc(sizeof(Node));
    p->previous = NULL;
    p->next = NULL;
    p->isMatchNode = 0;
    p->word = word;
    p->word_len = strlen(word);
    p->set_index = -1;
    return p;
}

void freeNodes(Node *node) {
    Node *p;
    while (node) {
        p = node->next;
        free(node);
        node = p;
    }
    return;
}

void enNode(List *list, Node *node, int direction) {
    if (node->next || node->previous) {
        printf("enNode can only used by node without next & previous\n.");
        return;
    }

    // empty list
    if (!list->head) {
        node->next = node->previous = NULL;
        list->head = list->tail = node;
        return;
    }
    
    if (direction == HEAD) {
        list->head->previous = node;
        node->next = list->head;
        list->head = node;
    } else if (direction == TAIL) {
        list->tail->next = node;
        node->previous = list->tail;
        list->tail = node;
    }
    return;
}

void insertNode(List *list, Node *new_node, int direction, Node *ref_node) {
    Node *p;
    if (!ref_node) // if ref_node is NULL, do nothing
        return;

    if (direction == BEFORE) {
        p = ref_node->previous;
        if (p) {
            p->next = new_node;
            new_node->previous = p;
        } else { // ref_node is head of list
            list->head = new_node;
            new_node->previous = NULL;
        }
        new_node->next = ref_node;
        ref_node->previous = new_node;
    } else if (direction == AFTER) {
        p = ref_node->next;
        if (p) {
            p->previous = new_node;
            new_node->next = p;
        } else { // ref_node is tail of list
            list->tail = new_node;
            new_node->next = NULL;
        }
        new_node->previous = ref_node;
        ref_node->next = new_node;
    }
    return;
}

void deNode(List *list, Node *node) {
    // For performance, this function will not check node is in list or not.
    Node *p;
    if(list->head == list->tail) {
        list->head = list->tail = NULL;
        node->next = node->previous = NULL;
        return;
    }

    if (list->head == node) {
        p = node->next;
        list->head = p;
        p->previous = NULL;
    } else if (list->tail == node) {
        p = node->previous;
        list->tail = p;
        p->next = NULL;
    } else {
        p = node->next;
        p->previous = NULL;
        p = node->previous;
        p->next = NULL;
    }
    // clear connection of node at last
    node->next = node->previous = NULL;
    return;
}

int isMatchNode(char *s, int i, Node *node) {
    int result;
    int *values;
    int j;

    if (i >= TABLE_SIZE)
        return COMPARE_RESULT_FAIL;

    // Try to found compare history of word
    values = lookup(compare_result, node->word);

    // If word as key is inserted into Dict
    if (values) {
        switch (values[i]) {
        case COMPARE_RESULT_SUCCESS:
            return COMPARE_RESULT_SUCCESS;
        case COMPARE_RESULT_FAIL:
            return COMPARE_RESULT_FAIL;            
        }
    } else {
        values = (int*)malloc(sizeof(int) * 10000);
        for (j = 0; j < 10000; j++) {
            values[j] = COMPARE_RESULT_NOTYET;
        }
        insert(compare_result, node->word, values);
        free(values);
        values = lookup(compare_result, node->word);
    }

    if (strncmp(&s[i], node->word, node->word_len)) {
        values[i] = result = COMPARE_RESULT_FAIL;
    } else {
        values[i] = result = COMPARE_RESULT_SUCCESS;
    }
    return result;
}

int dfs(char *s, int i) {
    Node *p = NULL, *p2 = NULL;
    int isMatchNode_buf;
#ifdef DEBUG
    printf("---------------------------\n");
    printf("dfs start with\n&s[%d]: %s\n", i, &s[i]);
    printf("holdNodes:\n");
    printNodes(holdNodes);
    printf("setNodes:\n");
    printNodes(setNodes);
#endif
    if (!holdList->head) {
        int suc_index;

        suc_index = holdList->head->set_index;
        ans[ansSize++] = suc_index;
        done[suc_index] = COMPARE_RESULT_SUCCESS;
#ifdef DEBUG
        printf("Answer found!\ni = %d, holdList->head->word = %s, suc_index=%d\n", i, holdList->head->word, suc_index);
#endif
        // Window slide
        while (isMatchNode(s, i, holdList->head) == COMPARE_RESULT_SUCCESS) {
            p = holdList->head;
            deNode(holdList, p);
            p->set_index = i;
            enNode(holdList, p, TAIL);
            suc_index = holdList->head->set_index;
            ans[ansSize++] = suc_index;
            done[suc_index] = COMPARE_RESULT_SUCCESS;
            i += p->word_len;
        }
        holdList->head = setList->head;
        holdList->tail = setList->tail;
        setList->head = setList->tail = NULL;
        return 1;
    }

    int *dupaMatch = (int*)malloc(TABLE_SIZE * sizeof(int));
    for (p = holdList->head; p; p = p2) {
        p2 = p->next;
        if (!dupaMatch[hash(p->word)] && isMatchNode(s, i, p) == COMPARE_RESULT_SUCCESS) {
            // label word compared in this layer
            dupaMatch[hash(p->word)] = 1;
            // Try dfs in next layer
            deNode(holdList, p);
            enNode(setList, p, TAIL);
            p->set_index = i;
            isMatchNode_buf = p->isMatchNode;
            p->isMatchNode = 1;
            if (dfs(s, i + p->word_len))
                return 1;
            // dfs failed, restore the previous state
            deNode(setList, p);
            insertNode(holdList, p, BEFORE, p2);
            p->set_index = -1;
            p->isMatchNode = isMatchNode_buf;
        }
    }
    // Only 1 node remain in 

    return 0;
}

/*
Dictionary *compare_result = NULL;
int ans[10000];
int done[10000];
int ansSize = 0;
int sLen = 0;
Node *holdNodes;
Node *setNodes;

*/

int* findSubstring(char* s, char** words, int wordsSize, int* returnSize) {
    int i;
    ansSize = 0;
    sLen = strlen(s);
    compare_result = create_dictionary();
    holdList = (List*)malloc(sizeof(List));
    setList = (List*)malloc(sizeof(List));

    for (i = 0; i < 10000; i++)
        done[i] = COMPARE_RESULT_NOTYET;
    
    for (i = 0; i < wordsSize; i++)
        enNode(holdList, createNode(words[i]), TAIL);

    for (i = 0; i < sLen; i++) {
        if(done[i] == COMPARE_RESULT_NOTYET)
            dfs(s, i);
    }
    freeNodes(holdList->head);
    freeNodes(setList->head);
    free_dictionary(compare_result);
    *returnSize = ansSize;
    return ans;
}

int main() {
    int i;
    int returnSize;
    int *ret;
    int wordsSize;
    //char *s = "barfoothefoobarman";
    //char *words[] = { "foo", "bar" };
    /* 
    char *s = "bcabbcaabbccacacbabccacaababcbb";
    char *words[] = { "c","b","a","c","a","a","a","b","c" };
    wordsSize = 9;
    */
    /*
    char *s = "barfoofoobarthefoobarman";
    char *words[] = { "bar","foo","the" };
    wordsSize = 3;
    */
    
    char s[5002];
    char *words[5000];
    for (i = 0; i < 5001; i++)
        s[i] = 'a';
    s[i] = '\0';
    for (i = 0; i < 5000; i++)
        words[i] = "a";
    wordsSize = 5000;
    

    ret = findSubstring(s, words, wordsSize, &returnSize);

    printf("returnSize = %d\n", returnSize);
    for (i = 0; i < returnSize; i++) {
        printf("%d ", ret[i]);
    }
    printf("\n");
    return 0;
}



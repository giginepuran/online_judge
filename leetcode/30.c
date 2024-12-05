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
    struct Node *next;
    int isMatchNode;
} Node;

Dictionary *compare_result = NULL;
int ans[10000];
int done[10000];
int ansSize = 0;
int sLen = 0;
Node *holdNodes;
Node *setNodes;
Node *endOfHoldNodes;
Node *endOfSetNodes;

void printNodes(Node *head) {
    Node *p;
    for (p = head; p; p = p->next)
        printf("%s\n", p->word);
    return;
}

Node *createNode(char *word) {
    Node *p = (Node*)malloc(sizeof(Node));
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

void enNode(Node **head, Node *node) {
    Node *p;
    if (!head) 
        return;
    if (!*head) {
        *head = node;
        return;
    }
    p = *head;
    while (p->next)
        p = p->next;
    p->next = node;
    return;
}

void deNode(Node **head, Node *node) {
    Node *p;
    if (!head || !*head)
        return NULL;
    if (*head == node) {
        *head = NULL;
        p = node->next;
        node->next = NULL;
        return p;
    }
    p = *head;
    while (p->next) {
        if (p->next == node) {
            p->next = NULL;
            p = node->next;
            node->next = NULL;
            return p;
        }
        p = p->next;
    }
    return NULL;
}

int isMatchNode(char *s, int i, Node *node) {
    int result;
    int *values;
    int j;

    if (i + node->word_len > sLen) {
        result = COMPARE_RESULT_FAIL;
    } else if (strncmp(&s[i], node->word, node->word_len)) {
        result = COMPARE_RESULT_FAIL;
    } else {
        result = COMPARE_RESULT_SUCCESS;
    }

    values = lookup(compare_result, node->word);
    if (!values) {
        values = (int*)malloc(sizeof(int) * 10000);
        for (j = 0; j < 10000; j++) {
            values[j] = COMPARE_RESULT_NOTYET;
        }
        insert(compare_result, node->word, values);
        free(values);
        values = lookup(compare_result, node->word);
    }
    values[i] = result;
    return result;
}

int dfs(char *s, int i) {
    Node *p = NULL;
#ifdef DEBUG
    printf("---------------------------\n");
    printf("dfs start with\n&s[%d]: %s\n", i, &s[i]);
    printf("holdNodes:\n");
    printNodes(holdNodes);
    printf("setNodes:\n");
    printNodes(setNodes);
#endif
    if (!holdNodes) {
        int suc_index;

        suc_index=setNodes->set_index;
        ans[ansSize++] = suc_index;
        done[suc_index] = COMPARE_RESULT_SUCCESS;
#ifdef DEBUG
        printf("Answer found!\ni = %d, setNodes->word = %s, suc_index=%d\n", i, setNodes->word, suc_index);
#endif
        // Window slide
        while (isMatchNode(s, i, setNodes) == COMPARE_RESULT_SUCCESS) {
            p = setNodes;        
            p->set_index = i;
            setNodes=deNode(&setNodes, p);
            enNode(&setNodes, p);
            suc_index=setNodes->set_index;
            ans[ansSize++] = suc_index;
            done[suc_index] = COMPARE_RESULT_SUCCESS;
            i += p->word_len;
        }
        holdNodes = setNodes;
        setNodes = NULL;
        return 1;
    }

    Node *remainNodes = NULL;
    int *dupaMatch = (int*)malloc(TABLE_SIZE * sizeof(int));
    for (p = holdNodes; p; p = p->next) {
        if (!dupaMatch[hash(p->word)] && isMatchNode(s, i, p) == COMPARE_RESULT_SUCCESS) {
            dupaMatch[hash(p->word)] = 1;
            p->set_index = i;
            p->isMatchNode = 1;
        } else {
            p->isMatchNode = 0;
        }
    }

    for (p = holdNodes; p; p = p->next) {
        if (p->isMatchNode) {
            remainNodes = deNode(&holdNodes, p);
            enNode(&setNodes, p);
            enNode(&holdNodes, remainNodes);
            
            if (dfs(s, i + p->word_len))
                return 1;
            
            deNode(&setNodes, p);
            enNode(&holdNodes, p);
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
    
    compare_result = create_dictionary();
    ansSize = 0;
    sLen = strlen(s);
    holdNodes = NULL;
    setNodes = NULL;

    for (i = 0; i < 10000; i++)
        done[i] = COMPARE_RESULT_NOTYET;
    
    for (i = 0; i < wordsSize; i++)
        enNode(&holdNodes, createNode(words[i]));

    for (i = 0; i < sLen; i++) {
        if(done[i] == COMPARE_RESULT_NOTYET)
            dfs(s, i);
    }
    freeNodes(setNodes);
    freeNodes(holdNodes);
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



#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifdef INPUT_FILE
#define INPUT fp
#else
#define INPUT stdin
#endif

#ifdef INPUT_FILE
static FILE *fp;
#endif

/* typedef */
typedef struct _section {
	int x1;
	int x2;
	struct _section *next;	// Must discrete to current section
	struct _section *prev;	// Must discrete to current section
} Section;

/* Global variables */
static int size_of_ans_set;
static int index_of_edges_in_ans_set[3000];
static int index_of_edges_in_length_decent[3000];
static int edges[3000 * 2];	// save edges in format: [a1, b1, a2, b2, a3, b3, ...]
Section *p_ans_section;

/* Functions */
Section *new_section(int x1, int x2);
void sort_edges_in_length_decent(int n);
int merge_edge_if_good(Section **section_head, int a, int b);

int main(int argc, char **argv) {
	char buffer[20];
	int test_i, test_n;
	int i, n, index;
	int ret;
	Section *p_section;

#ifdef INPUT_FILE
	fp = fopen(argv[1],"r");
	if (fp == NULL) {
		perror("Failed to open file");
		return 1;
	}
#endif

	fgets(buffer, sizeof(buffer), INPUT);
	sscanf(buffer, "%d", &test_n);
	for (test_i = 0; test_i < test_n; test_i++) {
		fgets(buffer, sizeof(buffer), INPUT);
		sscanf(buffer, "%d", &n);

		// initialize
		size_of_ans_set = 0;
		p_ans_section = NULL;
		for (i = 0; i < n; i++) { index_of_edges_in_length_decent[i] = i; }

		// get n edges, add to set_ori
		for (i = 0; i < n; i++) {
			fgets(buffer, sizeof(buffer), INPUT);
			sscanf(buffer, "%d %d", &edges[2*i], &edges[2*i+1]);
		}
		
		// Main logic
		// sort edges first
		sort_edges_in_length_decent(n);

		// Start pick edge from longest edge
		i = 0;
		while (i < n) {
			index = index_of_edges_in_length_decent[i];
			ret = merge_edge_if_good(&p_ans_section, 
						edges[2 * index], 
						edges[2 * index + 1]);
			if (ret == 1) {
				index_of_edges_in_ans_set[size_of_ans_set++] = index;
			}
			i++;
		}

		// Print answer
		printf("%d\n", size_of_ans_set);
		for (i = 0; i < size_of_ans_set; i++) {
			printf("%d", index_of_edges_in_ans_set[i] + 1);
			if (i != (size_of_ans_set - 1))
				printf(" ");
		}
		printf("\n");

		// free section
		while (p_ans_section) {
			p_section = p_ans_section;
			p_ans_section = p_ans_section->next;
			free(p_section);
		}
	}

#ifdef DEBUG
#endif
	return 0;
}

Section *new_section(int x1, int x2) {
	Section *p_section = (Section*)malloc(sizeof(Section));
	p_section->x1 = x1;
	p_section->x2 = x2;
	p_section->next = NULL;
	p_section->prev = NULL;
	return p_section;
}

void sort_edges_in_length_decent(int n) {
	int i, j;
	int index1;
	int index2;

	for (i = 0; i < n-1; i++) {
		for (j = 0; j < n-1-i; j++) {
			index1 = index_of_edges_in_length_decent[j];
			index2 = index_of_edges_in_length_decent[j + 1];
			if ((edges[2 * index1 + 1] - edges[2 * index1]) < 
				(edges[2 * index2 + 1] - edges[2 * index2])) {
				// swap short edge to the right
				index_of_edges_in_length_decent[j] = index2;
				index_of_edges_in_length_decent[j + 1] = index1;
			}
		}
	}


}

int merge_edge_if_good(Section **section_head, int a, int b) {
	Section *p_section = NULL;
	Section *p_section_next = NULL;
	Section *p_new_section = NULL;
	int merge_flag = 0;

	p_section = *section_head;

	if (p_section == NULL) {
		*section_head = new_section(a, b);
		return 1;
	}

	// check edge with each section
	while (p_section && !merge_flag) {
		// b < x1: this is a new section at the left
		if (b < p_section->x1)
			break;

		// x1 < a < b < x2: bad edge, ignore it.
		if (p_section->x1 <= a && b <= p_section->x2) {
			return 0;
		}

		// a < x1 <= b: overlap! add edge to extend section to left
		if (a < p_section->x1 && p_section->x1 <= b) {
			p_section->x1 = a;
			merge_flag = 1;
		}
			
		// a <= x2 < b: overlap! add edge to extend section to right
		if (a <= p_section->x2 && p_section->x2 < b) {
			p_section->x2 = b;
			merge_flag = 1;
			// Check merge with right section or not
			if (p_section->next && 
				p_section->x2 >= p_section->next->x1) {
				p_section_next = p_section->next;

				p_section->x2 = p_section_next->x2;
				p_section->next = p_section_next->next;
				if (p_section->next)
					p_section->next->prev = p_section;
				
				free(p_section_next);
			}
		}
		p_section = p_section->next;
	}

	if (merge_flag)
		return 1;

	// merge_flag == 0 means edge is outside of any section
	p_new_section = new_section(a, b);
	// if p_section is NULL: edge is at right side of any section
	if (p_section == NULL) {
		for(p_section = *section_head; p_section->next!=NULL; p_section=p_section->next);
		p_section->next = p_new_section;
		p_new_section->prev = p_section;
		return 1;
	}

	// if p_section is not NULL: edge is at left side of p_section
	p_section->prev = p_new_section;
	p_new_section->next = p_section;

	if (p_section == *section_head) {
		*section_head = p_new_section;
	}
	return 1;
}
 

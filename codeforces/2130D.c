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

/* Global variables */

/* Functions */


int main(int argc, char **argv) {
	char buffer[30000];
	int test_i, test_n;
	int i, j, n;
	char *p;
	int a[5001];
	int stay[5001];
	int mirror[5001];
	int fixed[5001];
	int focus_index;
	int inv_stay;
	int inv_mirror;
	int f_reset_backward;
	int f_reset_foreward;
	int ans;

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
		for (i = 1; i <= n; i++)
			fixed[i] = 0;
		
		// get input: p1 p2 p3 ...
		fgets(buffer, sizeof(buffer), INPUT);
		for (p = buffer, i = 1; i <= n;) {
			p += sscanf(p, "%d", &stay[i]);
			while (*(p++) != ' ');
			a[i] = stay[i];
			mirror[i] = 2 * n - stay[i];
#ifdef DEBUG
			printf("p[%d] = %d\n", i, stay[i]);
#endif
			i++;	
		}
		
		// First, fix a1 and an
		a[1] = stay[1] < mirror[1] ? stay[1] : mirror[1];
		a[n] = stay[n] > mirror[n] ? stay[n] : mirror[n];
		fixed[1] = 1;
		fixed[n] = 1;

		focus_index = 2;
		while (1) {
			// if all fixed -> best -> focus_index = n
			for (i = focus_index; i < n; i++) {
				if (!fixed[i]) { focus_index = i; break; }
			}
			if (focus_index == n)
				break;
	
			inv_stay = 0;
			inv_mirror = 0;		
			f_reset_backward = 0;
			f_reset_foreward = 0;
			// check inv from 1 to focus_index - 1
			for (i = 1; i < focus_index; i++) {
				inv_stay += (a[i] > stay[focus_index]);
				inv_mirror += (a[i] > mirror[focus_index]);
			}

			// check inv from focus_index + 1 to n
			for (i = focus_index + 1; i <= n; i++) {
				inv_stay += (stay[focus_index] > a[i]);
				inv_mirror += (mirror[focus_index] > a[i]);
			}
			
			fixed[focus_index] = 1;

			if (inv_stay >= inv_mirror) {	// mirror is better
				if (a[focus_index] == mirror[focus_index]) {	// a is already choose as mirror
					focus_index++;
					continue;
				}
				// if a < mirror (new) -> reset fixed foreward
				if (a[focus_index] < mirror[focus_index])
					f_reset_foreward = 1;
				// else, a > mirror (new) -> reset fixed backward
				else
					f_reset_backward = 1;

				a[focus_index] = mirror[focus_index];

			} else if (inv_stay < inv_mirror) {	// stay is better
				if (a[focus_index] == stay[focus_index]) {	// a is already choose as stay
					focus_index++;
					continue;
				}
				// if a < stay (new) -> reset fixed foreward
				if (a[focus_index] < stay[focus_index])
					f_reset_foreward = 1;
				// else, a > stay (new) -> reset fixed backward
				else
					f_reset_backward = 1;

				a[focus_index] = stay[focus_index];
			}

			// reset foreward or backward
			if (f_reset_foreward) {
				for (i = focus_index + 1; i < n; i++) { fixed[i] = 0; }
				focus_index++;
			}
			else if (f_reset_backward) {
				for (i = 2; i < focus_index; i++) { fixed[i] = 0; }
				focus_index = 2;
			} else { printf("WTF?\n"); }
		}


#ifdef DEBUG
#endif

		// Answer
		ans = 0;
		for (i = 1; i < n; i++) {
			for (j = i + 1; j <= n; j++) {
				ans += (a[i] > a[j]);
			}
		}
		printf("%d\n", ans);
	}
	return 0;
}



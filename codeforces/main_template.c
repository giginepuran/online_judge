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

int main(int argc, char **argv) {

#ifdef INPUT_FILE
	fp = fopen(argv[1],"r");
	if (fp == NULL) {
		perror("Failed to open file");
		return;
	}
#endif

#ifdef DEBUG
#endif
	return 0;
}

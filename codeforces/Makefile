CC = gcc
CFLAGS = -Wall -Wextra -DINPUT_FILE

SRC = main.c
APP = main

DEBUG_MAIN = debug_main
MAIN = main

all: $(MAIN) $(DEBUG_MAIN)

$(DEBUG_MAIN): $(SRC)
	$(CC) $(CFLAGS) -DDEBUG -o $@ $^

$(MAIN): $(SRC)
	$(CC) $(CFLAGS) -o $@ $^

clean:
	rm -rf $(DEBUG_MAIN) $(MAIN)

test:
	./$(DEBUG_MAIN) $(INPUT)

debug_test:
	./$(MAIN) $(INPUT)



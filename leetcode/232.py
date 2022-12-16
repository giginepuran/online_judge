class MyQueue:

    def __init__(self):
        self.stack_forward = []
        self.stack_inverse = []

    def push(self, x: int) -> None:
        self.stack_forward.append(x)

    def pop(self) -> int:
        if len(self.stack_inverse) is 0:
            while len(self.stack_forward) is not 0:
                self.stack_inverse.append(self.stack_forward.pop())
        return self.stack_inverse.pop()

    def peek(self) -> int:
        if len(self.stack_inverse) is 0:
            while len(self.stack_forward) is not 0:
                self.stack_inverse.append(self.stack_forward.pop())
        return self.stack_inverse[len(self.stack_inverse)-1]

    def empty(self) -> bool:
        return len(self.stack_forward) is 0 and len(self.stack_inverse) is 0

# Your MyQueue object will be instantiated and called as such:
# obj = MyQueue()
# obj.push(x)
# param_2 = obj.pop()
# param_3 = obj.peek()
# param_4 = obj.empty()

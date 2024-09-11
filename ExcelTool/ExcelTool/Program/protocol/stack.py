class Stack(object):
    def __init__(self):
        self.stack = []
    
    def push(self, value): # 进栈
        self.stack.append(value)
    
    def pop(self):  # 出栈
        if self.stack :
            res = self.stack[-1]
            self.stack.pop()
            return res
        else:
            raise LookupError("stack is empty")

    def is_empty(self):  # 如果栈为空
        return not bool(self.stack)
    
    def top(self):
        # 取出目前stack中最新的元素
        return self.stack[-1]

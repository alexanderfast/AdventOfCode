

def do(inp):
    current = None
    count = 0
    ret = ""
    for c in inp:
        if current is None:
            current = c
            count = 1
        elif current == c:
            count += 1
        elif current != c:
            ret += str(count) + current
            current = c
            count = 1
    ret += str(count) + c
    return ret

# test
test1 = """
1
11
21
1211
111221
1321131112
"""

for i in (i.strip() for i in test1.split("\n")[1:] if i):
    print do(i)

# part 1/2
inp = "1321131112"
for i in range(50):
    inp = do(inp)

print len(inp)

def increment(nums, i):
    if i <= -1:
        nums.insert(0, 0)
    else:
        if nums[i] >= 25:
            nums[i] = 0
            increment(nums, i - 1)
        else:
            nums[i] += 1

def generate(seed = None):
    nums = [0]
    if seed is not None:
        nums = [ord(c) - 97 for c in seed]
    while True:
        yield "".join((chr(i + 97) for i in nums))
        increment(nums, len(nums) - 1)

def rule1(s):
    pairs = 0
    i = 0
    m = len(s) - 2
    while i < m:
        b = ord(s[i])
        if b + 1 == ord(s[i + 1]) and b + 2 == ord(s[i + 2]):
            return True
        i += 1

def rule2(s):
    return "i" not in s and "o" not in s and "l" not in s

def rule3(s):
    pairs = 0
    i = 0
    m = len(s) - 1
    while i < m:
        if s[i] == s[i + 1]:
            pairs += 1
            if pairs == 2:
                return True
            else:
                i += 1
        i += 1

def validate(s):
    return rule2(s) and rule1(s) and rule3(s) # rule2 is cheapest

def getnext(s):
    g = generate(s)
    g.next()
    while True:
        p = g.next()
        if validate(p):
            return p

## test
for t in ("hijklmmn", "abbceffg", "abbcegjk"):
    print t, validate(t)

for t in ("abcdefgh", "ghijklmn"):
    print t, getnext(t)

## part1
print "hxbxwxba", getnext("hxbxwxba")

# part2
print "hxbxxyzz", getnext("hxbxxyzz")

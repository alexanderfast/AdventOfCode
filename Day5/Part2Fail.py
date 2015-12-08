
def getPairs(s):
    """ key is pair, value is starting index """
    d = {}
    for i in range(len(s)):
        pair = s[i:i+2]
        if len(pair) >= 2:
            if not pair in d.keys():
                d[pair] = [i]
            else:
                d[pair].append(i)
    return {k: v for (k, v) in d.iteritems() if len(v) >= 2}

def hasPair(s):
    result = False
    pairs = getPairs(s)
    if len(pairs) < 1:
        return False
    used = {}
    def increment(i):
        if not i in used.keys():
            used[i] = 0
        used[i] += 1
    for k, v in pairs.iteritems():
        for i in v:
            increment(i)
            increment(i + 1)
    print pairs
    return max(map(len, pairs.values())) >= 2 and max(used.values()) < 2 # char can only be used once

def hasPairWithLetterInBetween(s):
    d = {}
    for i in range(len(s)):
        pair = s[i:i+3]
        if len(pair)  < 3:
            break
        if pair[0] == pair[2]:
            if not pair in d.keys():
                d[pair] = [i]
            else:
                d[pair].append(i)
    print d
    return len(d) > 0

def isNice(s):
    return hasPair(s) and hasPairWithLetterInBetween(s)

def test(s):
    print isNice(s), s

#test("aaa")
#test("qjhvhtzxzqqjkmpb")
#test("xxyxx")
#test("uurcxstgmygtbstg")
#test("ieodomkazucvgmuy")

for l in (i.strip() for i in open("input.txt").readlines()):
    a = hasPair(l)
    b = hasPairWithLetterInBetween(l)
    if a and b:
        continue
    print a, b, l

print map(isNice, (i.strip() for i in open("input.txt").readlines())).count(True)
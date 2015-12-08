
def hasPair(s):
    for i in range(len(s))[0::1]:
        pair = s[i:i+2]
        if len(pair) >= 2:
            if s.count(pair) >= 2:
                return True
    return False

def hasPairWithLetterInBetween(s):
    for i in range(len(s))[0::1]:
        pair = s[i:i+3]
        if len(pair) >= 3:
            if pair[0] == pair[2]:
                return True
    return False

def isNice(s):
    return hasPair(s) and hasPairWithLetterInBetween(s)

def test(s):
    print isNice(s), s

test("qjhvhtzxzqqjkmpb")
test("xxyxx")
test("uurcxstgmygtbstg")
test("ieodomkazucvgmuy")

print map(isNice, (i.strip() for i in open("input.txt").readlines())).count(True)


def vowelCount(s):
    return sum(map(lambda x: s.count(x), "aeiou"))

def blacklist(s):
    for i in "ab", "cd", "pq", "xy":
        if i in s:
            return True

def repeatedChar(s):
    l = len(s)
    for i in range(l):
        if i+1 == l:
            return False
        if s[i] == s[i+1]:
            return True

def isNice(s):
    if blacklist(s):
        return False
    return vowelCount(s) >= 3 and repeatedChar(s)

def test(s):
    print isNice(s), s

#test("ugknbfddgicrmopn")
#test("aaa")
#test("jchzalrnumimnmhp")
#test("haegwjzuvuyypxyu")
#test("dvszwmarrgswjxmb")

print map(isNice, (i.strip() for i in open("input.txt").readlines())).count(True)

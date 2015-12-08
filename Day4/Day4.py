import md5

numberOfZeros = 6

def test(s, i):
    m = md5.md5()
    m.update(s)
    m.update(str(i))
    d = m.hexdigest()
    return d, len(d) - len(d.lstrip('0'))

#print test("abcdef", 609043)
#print test("pqrstuv", 1048970)

i = 0
while True:
    d, c = test("iwrupvqb", i)
    if c >= numberOfZeros:
        print d, i
        break
    i += 1

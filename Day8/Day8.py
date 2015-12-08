import re

chars = 0
memChars = 0

for l in open("Input.txt").read().split('\n'):
    m = l[1:-1]
    m = m.replace('\\"', '"')
    m = m.replace("\\\\", ".")

    o = 0
    while "\\x" in m:
        i = m.find("\\x", o)
        if i < 0:
            break;
        s = m[i:i+4]
        try:
            e = s.encode("utf8")
            m = m.replace(s, "0") # constant value, all same length
        except LookupError:
            pass
        o = i

    cl = len(l)
    ml = len(m)
    chars += cl
    memChars += ml
    print cl, ml, l, m

print chars, "-", memChars, "=", chars - memChars

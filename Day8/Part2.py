import re

chars = 0
memChars = 0

for l in open("Input.txt").read().split('\n'):
    e = '"' + re.escape(l) + '"'
    print len(l), len(e), l, e
    chars += len(l)
    memChars += len(e)

print memChars, "-", chars, "=", memChars - chars

import itertools

#inp = """
#London to Dublin = 464
#London to Belfast = 518
#Dublin to Belfast = 141
#"""

inp = """
Faerun to Tristram = 65
Faerun to Tambi = 129
Faerun to Norrath = 144
Faerun to Snowdin = 71
Faerun to Straylight = 137
Faerun to AlphaCentauri = 3
Faerun to Arbre = 149
Tristram to Tambi = 63
Tristram to Norrath = 4
Tristram to Snowdin = 105
Tristram to Straylight = 125
Tristram to AlphaCentauri = 55
Tristram to Arbre = 14
Tambi to Norrath = 68
Tambi to Snowdin = 52
Tambi to Straylight = 65
Tambi to AlphaCentauri = 22
Tambi to Arbre = 143
Norrath to Snowdin = 8
Norrath to Straylight = 23
Norrath to AlphaCentauri = 136
Norrath to Arbre = 115
Snowdin to Straylight = 101
Snowdin to AlphaCentauri = 84
Snowdin to Arbre = 96
Straylight to AlphaCentauri = 107
Straylight to Arbre = 14
AlphaCentauri to Arbre = 46
"""

distanceMap = {}
cities = []

for i in (i.strip() for i in inp.split("\n")[1:] if i):
    c, d = i.split(" = ")
    distanceMap[c] = int(d)
    c = c.split(" to ")
    cities.extend(c)

cities = set(cities)

def getdistance(a, b):
    key = a + " to " + b
    if key in distanceMap.keys():
        return distanceMap[key]
    return distanceMap[b + " to " + a]

def pairwise(iterable):
    a, b = itertools.tee(iterable)
    next(b, None)
    return itertools.izip(a, b)

routes = {}

for i in itertools.permutations(cities):
    dist = 0
    for p in pairwise(i):
        a, b = p
        dist += getdistance(a, b)
    routes[i] = dist
    #print i, "=", dist

lowestr = None
lowestd = None
for k, v in routes.iteritems():
    #if lowestd is None or v < lowestd: (part1)
    if lowestd is None or v > lowestd:
        lowestr = k
        lowestd = v

print "Result:", lowestr, "=", lowestd

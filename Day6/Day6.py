sizex = 1000
sizey = 1000
g = [0 for i in range(1000*1000)]

def get(x, y):
    return g[(sizex * y) + x]

def set(x, y, state):
    g[(sizex * y) + x] = state

def setArea(x1, y1, x2, y2, state):
    for y in range(y1, y2 + 1):
        for x in range(x1, x2 + 1):
            set(x, y, state)

def toggle(x1, y1, x2, y2):
    for y in range(y1, y2 + 1):
        for x in range(x1, x2 + 1):
            current = get(x, y)
            target = 1 if current == 0 else 0
            set(x, y, target)

def parse(s):
    p = s.split(' ')
    if p[0] == "toggle":
        verb = p[0]
        xy1 = p[1].split(',')
        xy2 = p[3].split(',')
    else:
        verb = p[0] + ' ' + p[1]
        xy1 = p[2].split(',')
        xy2 = p[4].split(',')
    return verb, int(xy1[0]), int(xy1[1]), int(xy2[0]), int(xy2[1])

count = 0
for l in (i.strip() for i in open("input.txt").readlines()):
    verb, x1, y1, x2, y2 = parse(l)
    state = None
    if verb == "turn on":
        setArea(x1, y1, x2, y2, 1)
    if verb == "turn off":
        setArea(x1, y1, x2, y2, 1)
    if verb == "toggle":
        toggle(x1, y1, x2, y2)
    print count
    count += 1

print g.count(1)

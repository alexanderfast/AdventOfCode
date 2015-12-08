


def parse(s):
    t = 0
    s = list((int(i) for i in s.split("x")))
    return s

def calc1(l, w, h):
    sides = ((l*w), (w*h), (h*l))
    return sum(map(lambda x: 2*x, sides)) + min(sides)

def calc2(l, w, h):
    a, b, c = sorted((l, w, h))
    return (a+a+b+b)+(l*w*h)

lines = open(r"C:\users\afast\desktop\in.txt").readlines()
#lines = ["2x3x4", "1x1x10"]

def part1():
    return sum(map(lambda x: calc1(*parse(x)), lines))

def part2():
    return sum(map(lambda x: calc2(*parse(x)), lines))
    
print "part1:", part1()
print "part2:", part2()

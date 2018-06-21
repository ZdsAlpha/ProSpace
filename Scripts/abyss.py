Space=0
Solid=1

BigMouthMan = 10
DullMan = 11
HampbackMan = 12
Boss = 13

Health0 = 20
Fuel0 = 21
Boost0 = 22
Score0 = 23

Life = 29

Checkpoint = 40
Finish = 41

class Region:
    def __init__(self,height):
        self.height = height
        self.region = []
    def set(self,x,y,t):
        for i in range(0,x-len(self.region)+1):
            self.region.append([])
        for j in range(0,y-len(self.region[x])+1):
            self.region[x].append(Space)
        self.region[x][y] = t

def curve(x,a=1,b=1.3):
    return (4*b**(x-a))/((1+b**(x-a))**2)
def curve2(x,a=1,bl=1.3,br=1.1):
    if x==a:
        return 1
    elif x<a:
        curve(x,a,bl)
    elif x>a:
        curve(x,a,br)

Seed=0
def Init(seed):
    global Seed
    Seed=seed

def Probability(Level):
    return 0.1 + curve(Level,2,1.05)

def CanAttach(Height):
    return Height > 10

def Generate(Level,Height):
    region = Region(Height-1)
    #--Generate Region--
    for i in range(0,Height+1):
        region.set(0,i,Solid)
    for i in range(0,6):
        region.set(i,0,Solid)
    for i in range(5,Height+10):
        region.set(5,i,Solid)
    if Seed%10==5:
        region.set(3,4,DullMan)
    if Seed%7==3:
        region.set(3,6,Fuel0)
    Height = 0
    #------------------
    return region.region,Height
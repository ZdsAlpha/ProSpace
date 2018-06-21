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
def curve2(x,a=1,bl=1.3,br=1.3):
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
    return curve(Level,2)/10

def CanAttach(Height):
    return True

def Generate(Level,Height):
    region = Region(Height-1)
    #--Generate Region--
    r = 5+Seed%5
    for i in range(r):
        region.set(2*i,Height+i,Solid)
        if (Seed+i)%61==0:
            region.set(2*i,Height+i+1,Checkpoint)
        elif (Seed+i)%91==54:
            region.set(2*i,Height+ i+1,Life)
        elif (Seed+i)%51==12:
            region.set(2*i,Height+ i+2,BigMouthMan)
    Height += r
    #------------------
    return region.region,Height
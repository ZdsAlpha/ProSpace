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
    return 0.02 + curve(Level,1,1.1)

def CanAttach(Height):
    return True

def Generate(Level,Height):
    region = Region(Height-1)
    #--Generate Region--
    for i in range(0,15 + Seed%10):
        region.set(i,Height,Solid)
        if (Seed+i)%8==0:
            if (Seed+i) % 11 == 0:
                region.set(i,Height+2,HampbackMan)
            if (Seed+i) % 10 == 0:
                region.set(i,Height+1,Checkpoint)
        elif (Seed+i)%6==1:
            if (Seed+i) % 21 == 5:
                region.set(i,Height+2,Life)
        elif (Seed+i)%6==2:
            if (Seed+i) % 17 == 7:
                region.set(i,Height+2,HampbackMan)
        elif (Seed+i)%6==3:
            if (Seed+i) % 5 == 4:
                region.set(i,Height+2,Score0)
            elif (Seed+i) % 8 == 5:
                region.set(i,Height+2,Health0)
        elif (Seed+i)%6==4:
            if (Seed+i) % 9 == 5:
                region.set(i,Height+2,Fuel0)
        elif (Seed+i)%6==5:
            if (Seed+i) % 4593455 == 4535:
                region.set(i,Height+2,Finish)
    #------------------
    return region.region,Height
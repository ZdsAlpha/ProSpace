Null=[]
Space=[0]
Solid=[1]

BigMouthMan = [10]
DullMan = [11]
HampbackMan = [12]
Boss = [13]

Health0 = [20]
Fuel0 = [21]
Boost0 = [22]
Score0 = [23]

Life = [29]

Checkpoint = [40]
Finish = [41]

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
    return curve(Level,2,1.2)/2

def CanAttach(Height):
    return True

def Generate(Level,Height):
    #--Generate Region--
    Region = [[]]
    #------------------
    return Region,Height
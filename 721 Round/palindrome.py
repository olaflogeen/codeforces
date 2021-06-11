#Define players as constants
ALICE = True
BOB = False

def assign(s, char, i):
    l = list(s)
    l[i] = char
    return "".join(l)

#Check if the string is a palindrome
def check_palindrome(s):
    #Reverse the string
    p = s[::-1]
    return p==s

#Count given s signs (either 1 or 0)
def count_signs(sign, s):
    ret = 0
    for char in s:
        ret += int(char == sign)
    return ret

#Check if and where the s string can become
#a palindrome in the next action
#return either -1 or [index]
def can_palindrome(s):
    s_length = len(s)
    signs = count_signs('1', s)
    if (s_length % 2 == 1):
        if(s[s_length/2 + 0.5] == '0'):
            return s_length/2 + 0.5
    if (signs % 2 == s_length % 2):
        return -1
    for i in range(s_length):
        if (s[i] != s[s_length - s - 1]):
            return i
    return -1

#Pick a firs 0
def pick_first(s):
    for i in range(len(s)):
        if (s[i] == '0'):
            s = assign(s, '1', i)
    return s

class Game:
    s = ""
    length = 0
    last_reverse = 1
    alice_bucks = 0
    bob_bucks = 0

    def __init__(self, text, l):
        self.length = l
        self.s = text
    
    def strategy1(self):
        if (check_palindrome(self.s)):
            self.last_reverse -= 1
            #Check if there's a way to insert a '1' into the palindrome with it remaining a palindrome
            if(self.length % 2 == 1 and self.length > 1):
                if (self.s[int(self.length / 2.0 + 0.5)] == '0'):
                    #Add a '1'
                    s = assign(self.s, '1', int(self.length / 2.0 + 0.5))
                    return 1
            self.s = pick_first(self.s)
            return 1
        else:
            if self.last_reverse <= 0:
                self.last_reverse = 2
                #reverse string
                self.s = self.s[::-1]
                return 0
            else:
                self.last_reverse -= 1
                c_pal = can_palindrome(s)
                if c_pal > -1:
                    self.s = assign(self.s, '1', c_pal)
                    return 1
                self.s = pick_first(self.s)
                return 1              

    #Make a move for the player
    #Return true if the game is finished
    def move(self, player):
        if (count_signs('1', self.s) == self.length):
            return True
        bucks = self.strategy1()
        if (player == ALICE):
            self.alice_bucks += bucks
        else:
            self.bob_bucks += bucks
        return False
    def play(self):
        current_player = ALICE
        while not (self.move(current_player)):
            #change the players each turn
            current_player = not current_player
        if (self.alice_bucks == self.bob_bucks):
            return "DRAW"
        if (self.alice_bucks > self.bob_bucks):
            return "BOB"
        else:
            return "ALICE"

def calculate_win(s, l):
    zeros = count_signs('0', s)
    if (zeros == 1):
        return "BOB"
    if (zeros % 2 == 1):
        return "ALICE"
    return "BOB"

#Get the number of tests
t = int(input())
#Loop for number of tests - get all the strings
winners = []
for i in range(t):
    #Get the length of the string - redundant ?
    length = int(input())
    #Get the actual string
    s = input()
    #game = Game(s, length)
    #win = game.play()
    winners.append(calculate_win(s, length))

for w in winners:
    print(w)
#https://codeforces.com/problemset/problem/1538/C
#The challenge is to find pairs without a brute force algorithm

def main():
    #Number of test cases
    k = int(input())
    returns = []
    #Get the test cases
    for i in range(k):
        #n, l, r
        ints = list(map(int, input().split(" ")))
        n = ints[0]
        l = ints[1]
        r = ints[2]
        
        #Get the array
        array = list(map(int, input().split(" ")))
        #Sort it in oreder to perform a binary search
        array.sort()
        #find the number of pairs and append it to the returns array
        returns.append(find_pairs(array))
    for i in returns:
        print(str(i) + " ", end='')

def find_pairs(array):
    return 0

if __name__ == "__main__":
    main()
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
        returns.append(find_pairs(array, l, r))
    for i in returns:
        print(i, end='\n')

def find_pairs(array: list, bottom, top):
    return brute_force(array, bottom, top)

def brute_force(array: list, bottom, top):
    #find the bottom indexes
    bottom_indexes = [-1,-1]
    bottom_array = [x-bottom for x in array]
    for i in range(len(bottom_array)):
        j = search(bottom_array, array[i], i)
        if j > -1:
            bottom_indexes = sorted([i, j])
            break
    #find the top indexes
    top_indexes = [-1, -1]
    top_array = [x-top for x in array]
    for i in range(len(array)-1,-1,-1):
        j = search(top_array, array[i], i, False)
        if j > -1:
            top_indexes = sorted([i, j])
            break
    #calculate the "distance" between both indexes
    distance = 0
    return distance

def flip (array):
    return [array[x] for x in range(len(array) - 1, -1 ,- 1)]

def clamp (v, min, max = None):
    if max != None:
        if v > max:
            return max
    if v < min:
        return min
    return v

def subtraction_method(array: list, bottom, top):
    #subtract bottom from every array element, then add this to the 
    bottom = [x+y for x,y in zip([i - bottom for i in array], array)]

def search(array, obj, skip_index = -1, from_left = True):
    r = range(len(array))
    if not from_left:
        r = range(len(array)-1, -1, -1)
    for i in r:
        if i == skip_index:
            continue
        if from_left:
            if array[i] + obj >= 0:
                return i
        else:
            if array[i] + obj <= 0:
                return i
    return -1

if __name__ == "__main__":
    main()
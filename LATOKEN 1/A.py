#https://codeforces.com/contest/1534/problem/A

def main():
    test_cases = int(input())
    for i in range(test_cases):
        line = input()
        n,m = [int(x) for x in line.split()]
        grid = ""
        for y in range(n):
            grid += input()
        phase = find_first(grid, n, m)
        established_grid = establish_grid(n, m, phase)
        if check(grid, established_grid):
            print("YES")
            print_grid(established_grid, n, m)
        else:
            print("NO")

        
def check(grid, established_grid):
    g = list(grid)
    e = list(established_grid)
    for i in range(len(g)):
        if g[i] == "W" or g[i] == "R":
            if g[i] != e[i]:
                return False
    return True

def print_grid(g, n, m):
    grid = ""
    l = list(g)
    for i in range(n):
        for x in range(m):
            grid += l[x + i * m]
        if i != (n - 1):
            grid += '\n'
    print(grid)

def find_first(grid: str, n, m):
    l = list(grid)
    phase = True
    for y in range(n):
        for x in range(m):
            i = x + m*y
            if l[i] == "W" or l[i] == "R":
                if l[i] == "R":
                    if not phase: 
                        return True
                    else:
                        return False
                if l[i] == "W":
                    if phase:
                        return True
                    else:
                        return False
                break
            phase = not phase
        if n%2 == 0:
            phase = not phase
    return phase

#Establish the grid with m by n size
#if phase is True then the starting letter is W (white), else R (Red)
def establish_grid(n, m, phase: bool):
    grid = ""
    for x in range (n):
        for y in range (m):
            if phase:
                grid += 'W'
            else:
                grid += 'R'
            phase = not phase
        if n%2 == 0:
            phase = not phase
    grid.strip()
    return grid


if __name__ == "__main__":
    main()
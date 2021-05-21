import java.util.Scanner;

public class K{
    
    public static void main(String[] args){
        Scanner input = new Scanner(System.in);
        //input.useDelimiter("\n");
        
        //Get the number of lines
        int tests = Integer.parseInt(input.nextLine());
        
        for (int ix = 0; ix < tests; ++ix){
            int n = Integer.parseInt(input.nextLine());
            System.out.println(""+findLargest(n));
        }
        input.close();
    }

    public static int findLargest(int a, int last, int and){
        and = a & last & and;
        if (and == 0){
            return last;
        }else{
            return findLargest(a, last-1, and);
        }
    }

    public static int findLargest(int a){
        return findLargest(a, a, a);
    }
}
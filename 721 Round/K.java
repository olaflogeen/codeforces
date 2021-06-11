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

    public static int findLargest(int n){
        int what = 0;
        for (int ix = 0; ix < 64;++ix){
            what += (int) Math.pow(2, ix);
            if ((n & (~what) ) == 0)
                return what - (int) Math.pow(2,ix);
        }
        return 0;
    }

    
}
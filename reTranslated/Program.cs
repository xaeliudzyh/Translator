namespace Namespace{
 class Program{ 
   static void Main(string[] args){
            int a=1,b=10,c=5;
            for(int i=0;i<10;i++)
            {
              a++;
              b+=2 ;
              c*=2;
            }
            if(a>c)
                Console.WriteLine(a+b+c);
            else
                Console.WriteLine(a*b*c);
   }
 }
}

# Tiny-Three-Pass-Compilier 

## Description

  Tiny compiler . You can make some simple fucstion fand calculate it with args.
  Supported +,-,/,* operators.
  Also can parse expression without spaces.

## Code snippet 

   ```
   
  Console.WriteLine( Simulator.Calculate("[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2) + 1.76", new double[3] { 4, 8, 16 }));   // output :3.76
   
  Console.WriteLine( Simulator.Calculate("[ x ] (x+5.1)", new double[1] { 15 }));   // output :20.1
   
  Console.WriteLine( Simulator.Calculate("[ x ] x + 10", new double[1] { 1.56 }));   // output :1.56
   
  Console.WriteLine( Simulator.Calculate("[ x y l ] x * y + l", new double[3] { 100, 100 , 7.98 }));   // output :10007.98 
   
  Console.WriteLine( Simulator.Calculate("[a, b, c, d] ( 3 *a + 2*b + 5*c+ 7*d+100)", new double[4] { 4, 8,6,18 }));   // output :284 
   
   ```

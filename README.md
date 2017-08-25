# CodeExercise

Open Solution in Visual Studio 2015 and buildl solution to get Nuget packages downloaded. <br />


<b>Problem Description</b> <br />

Problem:<br />
You are in your house wearing pajamas. You must be appropriately dressed for the temperature before leaving your house.
Your challenge is to programmatically process a list of commands for getting ready, enforce related rules, and display appropriate output.

Inputs:<br />
1.	Temperature Type (one of the following) <br />
•	HOT <br />
•	COLD

2.	Comma separated list of numeric commands <br />

Command	|	Description	HOT |	Response		| COLD Response
-----------------------------------------------------------------
1		|	Put on footwear	|	“sandals”	    |  “boots”
2		|	Put on headwear	|	“sun visor”	    |   “hat”
3		|	Put on socks	|	fail	        |   “socks”
4		|	Put on shirt	|	“t-shirt”	    |   “shirt”
5		|	Put on jacket	|	fail	        |   “jacket”
6		|	Put on pants	|	“shorts”	    |    “pants”
7		|	Leave house	 	|	“leaving house”	|    “leaving house”
8		|	Take off pajamas|	“Removing PJs”	|	“Removing PJs”
---------------------------------------------------------------------
Rules: <br />
•	Initial state is in your house with your pajamas on <br />
•	Pajamas must be taken off before anything else can be put on <br />
•	Only 1 piece of each type of clothing may be put on <br />
•	You cannot put on socks when it is hot <br />
•	You cannot put on a jacket when it is hot <br />
•	Socks must be put on before shoes <br />
•	Pants must be put on before shoes <br />
•	The shirt must be put on before the headwear or jacket <br />
•	You cannot leave the house until all items of clothing are on (except socks and a jacket when it’s hot) <br />
•	If an invalid command is issued, respond with “fail” and stop processing commands <br />

Examples <br />

Success <br />

Input: HOT 8, 6, 4, 2, 1, 7<br />
Output: Removing PJs, shorts, t-shirt, sun visor, sandals, leaving house<br />
Input: COLD 8, 6, 3, 4, 2, 5, 1, 7<br />
Output: Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house<br />
 
Failure<br />

Input: HOT 8, 6, 6<br />
Output: Removing PJs, shorts, fail<br />
Input: HOT 8, 6, 3<br />
Output: Removing PJs, shorts, fail<br />
Input: COLD 8, 6, 3, 4, 2, 5, 7<br />
Output: Removing PJs, pants, socks, shirt, hat, jacket, fail<br />
Input: COLD 6<br />
Output: fail<br />

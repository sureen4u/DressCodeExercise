# CodeExercise

Open Solution in Visual Studio 2015 and buildl solution to get Nuget packages downloaded. <br />


<b>Problem Description</b> <br />

<b>Problem:</b><br />
You are in your house wearing pajamas. You must be appropriately dressed for the temperature before leaving your house.<br />
Your challenge is to programmatically process a list of commands for getting ready, enforce related rules, and display appropriate output.<br />

<b>Inputs:</b><br />
1.	Temperature Type (one of the following) <br />
•	HOT <br />
•	COLD

2.	Comma separated list of numeric commands <br />

<table><tr><td>Command</td><td>Description</td><td>HOt Response</td><td>Cold Response</td></tr>
<table><tr><td>1</td><td>Put on footwear</td><td>“sandals”</td><td>“boots”</td></tr>
<table><tr><td>2</td><td>Put on headwear</td><td>“sun visor”</td><td>“hat”</td></tr>
<table><tr><td>3</td><td>Put on socks</td><td>fail</td><td>“socks”</td></tr>
<table><tr><td>4</td><td>Put on shirt</td><td>“t-shirt”</td><td>“shirt”</td></tr>
<table><tr><td>5</td><td>Put on jacket</td><td>fail</td><td>“jacket”</td></tr>
<table><tr><td>6</td><td>Put on pants</td><td>“shorts”</td><td>“pants”</td></tr>
<table><tr><td>7</td><td>eave house</td><td>“leaving house”</td><td>“leaving house”</td></tr>
<table><tr><td>8</td><td>Take off pajamas</td><td>“Removing PJs”</td><td>“Removing PJs”</td></tr>

Command	|	Description	HOT |	Response		| COLD Response<br />
1		|	Put on footwear	|	“sandals”	    |  “boots”     <br />
2		|	Put on headwear	|	“sun visor”	    |   “hat”   <br />
3		|	Put on socks	|	fail	        |   “socks”<br />
4		|	Put on shirt	|	“t-shirt”	    |   “shirt”<br />
5		|	Put on jacket	|	fail	        |   “jacket”<br />
6		|	Put on pants	|	“shorts”	    |    “pants”<br />
7		|	Leave house	 	|	“leaving house”	|    “leaving house”<br />
8		|	Take off pajamas|	“Removing PJs”	|	“Removing PJs”<br />

<b>Rules:</b> <br />
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

<b>Examples</b> <br />

<b>Success</b> <br />

Input: HOT 8, 6, 4, 2, 1, 7<br />
Output: Removing PJs, shorts, t-shirt, sun visor, sandals, leaving house<br /><br />
Input: COLD 8, 6, 3, 4, 2, 5, 1, 7<br />
Output: Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house<br /><br />
 
<b>Failure</b><br />

Input: HOT 8, 6, 6<br />
Output: Removing PJs, shorts, fail<br /><br />
Input: HOT 8, 6, 3<br />
Output: Removing PJs, shorts, fail<br /><br />
Input: COLD 8, 6, 3, 4, 2, 5, 7<br />
Output: Removing PJs, pants, socks, shirt, hat, jacket, fail<br /><br />
Input: COLD 6<br />
Output: fail<br /><br />

# ProtoBombe
A code implementation of Alan Turing's bombe.

Note: for a very realistic software Bombe, be sure to check out this one:
http://www.lysator.liu.se/~koma/turingbombe/

How to use:
We will be using the same cypher text, crib, and map as in the example from ~koma's web site:
http://www.lysator.liu.se/~koma/turingbombe/TuringBombeTutorial.pdf

Cyphertext:

S N M K G G S T Z Z U G A R L V

Crib:

W E T T E R V O R H E R S A G E


   1. Create a map file in JSON format.  See map.json for an example.
   2. Make sure the map file is correctly specified in the app.config file.
   3. Run BombeProto1.exe with the starting position of the rotors.  If you don't pass it any it will default to Z Z Z.
   4. All possible matches for that map will print out with the plugboard settings.
   

Mapping file:

	1. wheelTypes: An array of the rotors you want to test. Order is important. (The first wheel is the left-most on the enigma.)
	2. reflectorType: Which reflector to use.
	3. enableDiagonalBoard: If 'true', the diagonalboard is enabled. (The very first bombe didn't have a diagonal board, so this feature allows you to see the difference it makes.)
	4. currentEntry: which bus should receive the signal.
	5. inputLetter: which letter on the aforementioned bus should receive the signal.
	6. mapEntries: an array of Enigma machines, how many steps ahead of the key they are, and which buses they map to. Order *is not* important.
	
	
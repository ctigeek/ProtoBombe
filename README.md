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
   


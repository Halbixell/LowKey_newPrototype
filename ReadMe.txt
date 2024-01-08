ReadMeFile: LowKey Escape from Asgard

Content:
1) Content of the Prototype
2) Controls and Functionality
3) Win-Condition
4) Moves
5) Enemies
6) Example-Solution for testing


1) Content of the Prototype
The Screen is split into Inventory (Boxes on the right side), Movement-path (Boxes on the bottom on the screen), and 
the Rooms of Asgard (the playing are in the middle). Loki (the player Character) is trapped in one of those rooms and wants to escape unseen.
(Exit for now represented by a green tile).
Loki, the player-character is represented by the Player from the PokemonGame, the two enemies by the corresponding NPCs.
The Prototype consist of one level for testing.


2) Controls and Functionality
The Inventory holds the 4 different Moves, along with how many of the corresponding move are present. The core challenge of the game is in
choosing the correct sequence of moves to let Loki walk from his starting position to the exit without being spotted by the enemies.
By dragging and dropping the moves from the inventory to the Movement-Path, the Player can choose the Path Loki will take.
When the Start-Button is pressed, Loki will walk according to the set path and Enemies will walk according to their Logic.

Should a Movement-option be in the wrong place, the player has two option to remove or replace it:
a)drag the Movement-option to the Trash. The Movement-option will be deleted and the inventory will recieve it back.
b)drag another Movement-option from the inventory over the space, where the unwanted option is situated. It will the replace the old Movement-option.
Should the Player drop a Movement-option over any space that is not the Path or the Trash, it will jump back to its original space.

(CAUTION: Do not drag a Movement-option directly from the inventory to the trash, as this will lead to an error and delete the movement-option out of the inventory. We will fix it soon.
By pressing the RESTART-Button, the level will be reset ot its initial position.


3) Win-Condition
The Level is won when Loki enters the Win-tile after walking the set Path.
The Level is lost if Loki is at any point in the Visual-Area of the Enemies.
For now, if Loki walks his path and neither case occurs, the Player may try again.
This will be changed in a later Version of the game to let the player have one try tp escape the room before resetting.


4) Moves
The Player has 4 Moves at their disposal, all of which are named after well known children of Loki. Every Move consists of 4 Steps:
a) Sleipnir, the eight-legged Horse (Horse): right-right-right-down
b) Fenrir, the God-devourer (Wolf): right-up-down-right
c) Jörmungandr, the World-serpent (Snake): up-right-down-down
d) Hel, the Queen of the Dead (Face): right-right-right-right

If Loki cannot perform a move in its entirety (if there is a wall in the way), the impossible steps of the Movement-options will simply be ignored.


5) Enemies
The Movement of Enemies is split into 2 parts, which is started as soon as Loki starts moving:
a) Movement
b) Looking

Every Enemy in the prototype has 4 Moves:
a) right-look-right-look
b) down-look-down-look
c) left-look-left-look
d) up-look-up-look

In the Prototype are 2 Enemies:
Enemy 1 (Lower edge of the Screen) will move in the following pattern: a)-b)-c)-d)-repeat
Enemy 2 (Closer to Loki) will move in the following pattern: c)-b)-a)-d)-repeat
















6) Example-Solutions for testing
Path to Win: Face-Face-Horse-Wolf-Snake-Horse-Snake-Snake-Horse-Wolf
Path to Lose: Horse-Snake-Horse
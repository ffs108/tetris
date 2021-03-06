Learning C# through a recreation of Tetris

Assets are courtesy of "L-Gad" over at https://l-gad.itch.io/. Please feel free to check them out and support them if you so choose.


This is a simple interpretation of Tetris in a C# base. This was also a learning experience for Visual Studio and a lot of the tools that are available in the IDE. I wanted to get my feet wet and had never touched C# before this. I loosely grasped the the interactive xaml implemention feature and ended up learning that the backend for the XAML page was very similar to a JS doc connecting to an HTML page so that in itself was not too hard. It obviously did all of the audio-visual stuff.


I did not use Unity and simply created the game over a grid represented by a 2d array(this ended up being most common implemention I saw around the internet as well, and the one that made sense for my purpose since I want to grasp some of the fundamentals and strengths of the language). Each piece is actually just a numeric ID that matches their place in an array containing the different colored tiles. The different block children classes actually contain all possible positioms a piece can have in the context of the gamegrid. This gives the illusion of actual game pieces with actual rotation. There is a sweep for line clears constantly iterating over the 2d array and movement is handled through a Position object that carries different locations within the same array. A GameState object is responsible for keeping track of all these changes, whether the game is over, and piece movement.

The game then plays like every implementation of Tetris I've seen, with a hold and an image of what tile is next. There is also a timer and score that are working through the xaml.cs file constantly being updated on screen. I decided to also implement the pause functionality in the xaml.cs file instead of the gamestate object since it was easier to control the stop and start of the timer and to create an interactive "menu" when paused. Syntax-wise C# was not too hard to pick up. It has a few elements I can associate with JS but I can see is clear Java influence and in some instances the code could clearly be ported 1:1.

If you want to use this go ahead and create a shortcut wherever it has a few large(at least relatively the .wav files used a large) assets that are needed but it works as is. You could also delete the publish files(not sure if you actually have to, not very familiar with VS) and import the solution right over or fork this and add something I maybe didn't implement or what ever you'd like..

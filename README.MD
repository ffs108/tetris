Learning C# through a recreation of Tetris

Assets are courtesy of "L-Gad" over at https://l-gad.itch.io/. Please feel free to check them out and support them if you so choose.


This is a simple interpretation of Tetris in a C# base. This was also a learning experience for Visual Studio and a lot of the tools that are available in the IDE. As I wanted to get my feet wet and had never touched C# before this. I loosely grasped the the interactive xaml implemention feature and ended up learning that the backend for the XAML page was very similar to a JS doc connecting to an HTML page so that in itself was not too hard. It obviously did all of the audio-visual stuff.


I did not use Unity and simply created the game over a grid represented by a 2d array(the most common implemention I saw around the internet and the one that made sense for my pupose since I want to grasp some of the fundamentals and strengths of the language). Each piece is actually just a numeric ID that matches their place in an array containing the different colored tiles. This gives the illusion of actual game pieces. There is a sweep for line clears constantly iterating over the 2d array and movement is handled through a Position object that carries different locations within the same array. The game then plays like every implementation of Tetris with a hold and an image of what tile is next. There is also a timer and score that are working through the xaml.cs file constantly being updated on screen. Syntax-wise C# was not too hard to pick up. It has a few elements I can associate with JS but I can see is clear Java influence and in some instances the code could clearly be ported 1:1.

If you want to use this go ahead and create a shortcut wherever it has a few large(at least relatively the .wav files used a large) assets that are needed but it works as is. You could also delete the publish files(not sure if you actually have to, not very familiar with VS) and import the solution right over.
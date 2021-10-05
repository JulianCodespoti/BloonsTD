# .DotNET Bloons

A recreation of the game "Bloons Tower Defense", written in C# and using both WPF and Splashkit.

![](C:\Users\julia\OneDrive\Desktop\Coding\BloonsGame\BloonsGame\GitHubImages\BloonsGame.png)
# Description

BloonsTD. A fundamental game that shaped my childhood, that I would spend countless of playing as the days went by. Thus, it became evident that for my University project, I would recreate none other than the game itself.

Through the use of SplashKit - a software toolkit produced by my University that provides functions for drawing bitmaps, shapes and text onto a window -, as well as WPF, I have in turned produced a game that contains many of the features of which is typically contained in a general tower defense game. The following features are only some of the many implementations I have included:

- Enemies (bloons) that travel the map, using a coordinate system.
- 3 custom maps (and more to be implemented in the future).
- The upgrading and selling of towers.
- An algorithm that uses the maps checkpoints to determine whether a tower can be placed.
- 3 types of towers, with varying ShotTypes, as well as 3 types of enemies (bloons).
- Targeting options for towers that can be selected in the GUI (First, Last, Strong, Weak).
- An algorithm to allow towers to shoot projectiles in a linear path, depending on the targeting, with various bitmaps for the different tower's projectiles.
- A start menu which allows the user to select a map, as well as the ability pause and restart the game.
- A loss screen which displays the amount of rounds you lasted.
- A map manager which contains a list of maps, obtained from deserializing the corresponding map's JSON files.
- Various OOP design patterns (Such as the strategy, singleton and factory pattern).
- A game loop method that runs continuously during gameplay.


# How to Run
This is a step by step guide on running this program.

## You must have SplashKit installed.
To install SplashKit, follow the link https://www.splashkit.io/articles/installation/ and select your operating system. Proceed to follow the corresponding instructions on the next page.

## To run in Visual Studio
- Run the solution "BloonsTD-main\BloonsGame\BloonsGame.sln" in Visual Studio

- Goto Project -> BloonsLibrary Properties -> Debug then change the Working Directory to the project directory. For example, on my computer the project directory is: "C:\Users\Julian\Desktop\Coding\BloonsGame\BloonsLibrary"

- Save the changes.

- Run "BloonsGame" at the top of the window.

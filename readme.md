# .DotNET Bloons

A recreation of the game "Bloons Tower Defense", written in C# and using both WPF and Splashkit.

![Image](/BloonsGame/GitHubImages/Image3.png?raw=true)
# Bloons Game Description

BloonsTD. A fundamental game that shaped my childhood, that I would spend countless of playing as the days went by. Thus, it became evident that for my University project, I would recreate none other than the game itself.

Through the use of SplashKit - a software toolkit produced by my University that provides functions for drawing bitmaps, shapes and text onto a window -, as well as WPF, I have in turned produced a game that contains many of the features of which is typically contained in a general tower defense game. The following features are only some of the many implementations I have included:

- Enemies (bloons) that travel the map, using a coordinate system.
- The upgrading and selling of towers.
- Included program that allows for the creation and editing of maps.
- An algorithm that uses the maps checkpoints to determine whether a tower can be placed.
- 3 types of towers, with varying ShotTypes, as well as 3 types of enemies (bloons).
- Targeting options for towers that can be selected in the GUI (First, Last, Strong, Weak).
- An algorithm to allow towers to shoot projectiles in a linear path, depending on the targeting, with various bitmaps for the different tower's projectiles.
- A start menu which allows the user to select a map, as well as the ability pause and restart the game.
- A loss screen which displays the amount of rounds you lasted.
- A map manager which contains a list of maps, obtained from deserializing the corresponding map's JSON files.
- Various OOP design patterns (Such as the strategy, singleton and factory pattern).
- A game loop method that runs continuously during gameplay.


# Map Editor Description

To compliment the program, a map editor has been included in the solution, allowing the user to both create, as well as edit maps. These maps can then be selected in the BloonsGame program and played with.

Akin to the BloonsGame project, both Splashkit and WPF have been utilized throughout the design of the Map Creator, whereby the initial interface is set up with WPF, whereas the Map Creator UI itself is set up with Splashkit. The following features of the map creator tool are only some of the various implementations I have incorporated in the project: 

- A graphical interface which allows both the selection and deselection of track tiles.
- Algorithms put in place to prevent the user from adding track tiles that don't align with the current path of the track, as well as ensuring the tiles that are being removed must be removed in order from most recently placed tiles.
- Event handlers which get invoked when a button is pressed (whereby the 3 buttons are the save button, and the two reference tile buttons).
- A series of calculations that can determine the verticle and horizontal grid lines, as well as the grid points, for any window or individual grid dimension.
- Various design patterns, such as factory patterns for the creation of tiles and tilebuttons, as well as a singleton for the tile information.
- Method of saving the map state by serializing all of the information and saving the json file into the folder where the Bloons Game processes these maps.

# Design Patterns Included In Solution

When initially creating the game, knowledge of topics such as design patterns, clean architecture, abstraction, polymorphism and various Object Orientating Programming componenets were scarce. Thus, it became evident that in order to produce a game that could be both easy to maintain, as well as flexible, a solid foundation would be required. In order to achieve this, the application would require minimal coupling, as well as appropriate abstraction, to ensure this would be met.

## Design patterns: 
To ensure that the process for developing the game remained clean and efficient, various design patterns have been incorporated, as they not only allow you to build software faster, but utilize methodologies of doing so that have been rigorously tested and put into practice many, many times. These patterns ultimately consist of generalized solutions to problems that are commonly faced in coding, and provide a template to solving said problem that can be utilized in many situations.

The following are the design patterns that have been used in my code. With them will be an explanation of the pattern, how I've incorporated it in my code, and the reasoning for doing so:

## Singleton
The Singleton design pattern essentially serves as a way of producing one instance of a class that can be used as a global point of access to the object. Thus, it can be utilized effectively in a program that requires multiple instances of a class throughout said program.
The Singleton pattern works by having a static member in the singleton class that maintians a reference to the instance, as well as a method that returns the aforementioned static member.
Within my Bloons Game project, I found that I continually referenced the same various objects all thorughout my code, thus, it became clear to me that the Singleton design pattern would help solve this issue. Additionally, when analysing my code, it further became clear that the objects I would continually reuse predominately related to information regarding the game state, consequently, I decided to name the singleton "GameState" and provide it with the various information throughout my program that incorporated the game state, such as the list of bloons, list of towers, player information, projectile information and more. This information can be seen in the following snippet, where "_state" is the static member.
``` csharp
private static GameState _state;
public readonly List<Bloon> Bloons = new List<Bloon>();
public readonly List<Tower> Towers = new List<Tower>();
public Dictionary<Color, int> BloonsSpawned = new Dictionary<Color, int>();
public Dictionary<Color, int> BloonsToBeSpawned = new Dictionary<Color, int>();
public readonly Player Player = new Player();
public readonly ProjectileManager ProjectileManager = new ProjectileManager();
```
The function "GetGameStateInstance" serves as a global point of access to the object:
``` csharp
public static GameState GetGameStateInstance()
{
    if (_state == null)
    {
        lock (Locker)
        {
            if (_state == null)
            {
                _state = new GameState();
            }
        }
    }

    return _state;
}
```

## Factory
The Factory design pattern is a revolves around producing a **factory** object that creates other objects. Its benefit is that it has the capacity to create new objects whilst conjointly hiding the instantiation logic from the user. I found that whilst coding my game, at one point I needed to produce an instance of a "tower", depending on the type of tower the user had selected. Thus, I in turn developed the "TowerFactory" class, which would take in a tower name and produce a new instance of the corresponding tower. The following is a snippet of the Factory design pattern in my code:
```csharp
public static class TowerFactory
{
    public static Tower CreateTowerOfType(string tower)
    {
        if (tower == DartTower.Name) return new DartTower();

        if (tower == LaserTower.Name) return new LaserTower();

        if (tower == SniperTower.Name) return new SniperTower();

        throw new Exception("You are trying to create a tower type that does not exist.");
    }
}
```
Here is how I have utilized the aforementioned **factory** in my code.
```csharp
// Create a new tower depending on the selected tower and write tower description in GUI
var selectedTower = TowerFactory.CreateTowerOfType(towerPlacer.SelectedInGui);
_guiRenderer.WriteTowerDescription(towerPlacer, selectedTower);
```
Additionally, I have further utilized the factory method throughout my Map Creator tool, whereby I have produced a factory for both the tile buttons, as well as the tiles themselves. Initially, I had developed a class for the **Track tile buttons**, as well as the **Grass tile buttons**, however, the classes did not contain any extra implementations that would warrant the need to place their functionalities in an entire class. Thus, I in turn utilized the factory method, to create different versions of the buttons, whilst hiding the instantiation logic from the user, without the need of additionally button classes. 
```csharp
public class TileButtonFactory
{
    public static TileButton CreateTileOfType(TileType tileType, Point2D position)
    {
        if (tileType == TileType.Checkpoint) return new TileButton(TileType.Checkpoint, new Bitmap("stoneBig", "../BloonsLibrary/Resources/stoneBig.png"), ButtonTypes.AddRegularTile, position);

        if (tileType == TileType.Normal) return new TileButton(TileType.Normal, new Bitmap("grassBig", "../BloonsLibrary/Resources/grassBig.png"), ButtonTypes.AddCheckpointTile, position);

        throw new Exception("You are trying to create a tower type that does not exist.");
    }
}
```
As previously mentioned, I further have a factory for creating the tile objects, so that depending on the selected tile button the user has pressed, I can create an instance of the tile. 
```csharp
public class TileFactory
{
    public static Tile CreateTileOfType(TileType tileType)
    {
        if (tileType == TileType.Checkpoint) return new CheckpointTile();

        if (tileType == TileType.Normal) return new GrassTile();

        throw new Exception("You are trying to create a tower type that does not exist.");
    }
```
The TileFactory's implementations in my code can be seen below:
```csharp
var tileToAdd = TileFactory.CreateTileOfType(SelectedTileType);
```

## Strategy
The Strategy design pattern essentially allows you encase a variety of algorithms together, isolated from the program, and allow the ability to select the desired algorithm depending on situation during runtime. Thus, it became clear that if i were to implement various targeting options for each tower, that the strategy pattern would be ideal in doing so. 
In order to achieve this, I have implemented the interface **ITarget**.
```csharp
public interface ITarget
{
    TowerTargeting TargetType { get; }

    Bloon BloonToTarget(List<Bloon> bloons);
}
```
The various classes the implement the interface must be able to return a bloon they wish to target, depending on the type of targeting, as well as a way of identifying what the tower's targeting type is, through the use of the enum, **TowerTargeting**.

For instance, if the tower's targeting type is first, the following calculations are utilized in order to determine which bloon the tower will target:
```csharp
 public Bloon BloonToTarget(List<Bloon> bloons) // Returns the bloon that has travelled the least distance.
    {
        Bloon targetBloon = null;
        foreach (var bloon in bloons)
        {
            targetBloon ??= bloon;
            if (targetBloon.DistanceTravelled > bloon.DistanceTravelled)
            {
                targetBloon = bloon;
            }
        }

        return targetBloon;
    }
```
The type of targeting a tower is set to will be controlled in the UI, whereby selecting a tower and clicking on the desired targeting mode will in turn change the tower's targeting property.

![Image](/BloonsGame/GitHubImages/Targeting.png?raw=true) 

By default, the tower's targeting it set to **First**. However, the program is able to determine the trajectory of the bloon's projectile depending on the type of targeting. The tower knows which bloons to target, and the projectile manager utilizes this information to determine the endpoint for the projectile, and linearly interpolates said projectile between this and the tower's position.

```csharp
public Point2D GetProjectileEndPoint(Bloon bloon, Tower tower)
{
    var towerToBloonAngle =
                Math.Atan((bloon.Position.Y - tower.Position.Y) / (bloon.Position.X - tower.Position.X)) + Math.PI; // Calculates angle between tower and bloon.

    if (bloon.Position.X > tower.Position.X) // If the bloon is either to the right or left of the tower, perform a different calculation for the angle.
    {
        towerToBloonAngle = Math.Atan((bloon.Position.Y - tower.Position.Y) /
                                        (bloon.Position.X - tower.Position.X));
    }
    ; var projectileEndPoint = new Point2D() // Extrapolate the distance from the tower to the bloon, to the end of the towers radius in the same angle.
    {
        X = tower.Position.X + (tower.Range * Math.Cos(towerToBloonAngle)),
        Y = tower.Position.Y + (tower.Range * Math.Sin(towerToBloonAngle))
    };
    projectileEndPoint.X -= tower.ShotType.ProjectileWidth / 2; // Bitmap draws from the top-left of the image. Re-configure bitmap so that the origin of the bitmap is where it is drawn and calculated from.
    projectileEndPoint.Y -= tower.ShotType.ProjectileLength / 2;
    return projectileEndPoint;
}
```
Thus, by returning the bloon to target through the use of the strategy pattern, the projectile manager can take this value in through it's method's constructor and successfully determine the endpoint for the projectile's trajectory.


# How to Run
This is a step by step guide on running this program.

## You must have SplashKit installed.
To install SplashKit, follow the link https://www.splashkit.io/articles/installation/ and select your operating system. Proceed to follow the corresponding instructions on the next page.

## To run in Visual Studio
- Run the solution "BloonsTD-main\BloonsGame\BloonsGame.sln" in Visual Studio

- Goto Project -> BloonsLibrary Properties -> Debug then change the Working Directory to the project directory. For example, on my computer the project directory is: "C:\Users\Julian\Desktop\Coding\BloonsGame\BloonsLibrary"

- Save the changes.

- Run "BloonsGame" at the top of the window.

# Images of the game running

## Starting screen

![Image](/BloonsGame/GitHubImages/MainMenu.png?raw=true)
## Selected tower on screen

![Image](/BloonsGame/GitHubImages/Image1.png?raw=true)
## Unable to place tower on path

![Image](/BloonsGame/GitHubImages/Image2.png?raw=true)
## Second tower firing a projectile

![Image](/BloonsGame/GitHubImages/Image4.png?raw=true)
## Unable to place tower on path

![Image](/BloonsGame/GitHubImages/Image5.png?raw=true)
## Upgrading the tower

![Image](/BloonsGame/GitHubImages/Image6.png?raw=true)
## Projectile of the third tower

![Image](/BloonsGame/GitHubImages/Image7.png?raw=true)
## Pause screen

![Image](/BloonsGame/GitHubImages/Image8.png?raw=true)
## Loss Screen

![Image](/BloonsGame/GitHubImages/Image9.png?raw=true)

# Images of the map creator running

## Starting screen

![Image](/BloonsGame/GitHubImages/MapCreatorHomeScreen.png?raw=true)
## Map Interface

![Image](/BloonsGame/GitHubImages/MapInterface.png?raw=true)
## Creating map by adding track tiles

![Image](/BloonsGame/GitHubImages/CreatingMap.png?raw=true)
## Removing track tiles

![Image](/BloonsGame/GitHubImages/EditingTrack.png?raw=true)
## Playing Bloons Game with custom made map


![Image](/BloonsGame/GitHubImages/PlayingWithMap.png?raw=true)




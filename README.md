# MindMetrixAssignmentVR
## Installation

- [ ] Pre-requisite : Version Unity 6000.2.10f1
    - [ ] InputSystem in ProjectSettings
      
> [!Warning]
> The project need the new InputSystem (not the InputAction). If error occurs : go to Project Settings > Player > Other Settings > Configuration > Active Input Handling : InputSystem (new)
> you then need to go to the package manager and install the input system.

> [!Warning]
> If you have pink assets, the shader may need to be adjusted to, say, "Standard" shader for these materials:
> - Assets/Materials
> - Assets/Mini Legion Rock Golem PBR HP Polyart/Materials

> [!Note]
> If you have an error regarding "Library\PackageCache", it is probably because the name is too long. Move the project to the root and the issue should be resolved.

To launch the backend : 
``` 
cd BackendExample
dotnet run 

```

This will automatically launch on port 5000, in localhost.


Open the project in the Editor. The default scene is SampleScene (that is the correct one). To launch the app, press play.

## Goal
The goal of the application is to shoot at the correct time the target sphere appearing on the monsters' head. The smaller the target is, the greater the points. 
It will at first be an orange sphere, which is a "good" range. By waiting until the sphere shrinks enough and turns green, it will become a "perfect" shoot. If you wait too much, the monster selfs destroy and you lose points. 
Points are relative to the speed of the enemy's sphere shrinkage and type of enemy.

The data retrieved from the player when interacting will be a simulated continous signal (like a pupil-size raw input). We want to know what is the state of the player when interacting, cf information retrieved in the Architecture section.

## Architecture
The project follows the Game Development architecture framework :
- GameMode: already in place in the scene, it's defining what type of game settings the level should have. The Game mode provides the following GameObject :
    - GameState : contains the information about the current state of the game (score, number of player, etc)
    - PlayerState : contains information about the current player state (name, own stats, etc)
    - PlayerController: is the logic (aka the brain) of the player. It defines the actions of the player
    - Pawn : physical representation (aka body) of the player in the scene.

This allow for a scalable and cross-platform integration. For instance, by switching the Pawn to a "VRPawn" and override the related methods from the interface linked to the Pawn, the game can easily be switched from desktop to VR. It also ensure an easier multiplayer experience.

The enemies are created using 3 different ScriptableObject :
- ```SO_SpawnableInfo```
- ```SO_VFX```
- ```SO_Sound```

It allows modular enemy creation and definition. The SO_SpawnableInfo will represent a type of enemy (which will be assign its corresponding VFX and sound). The SpawnManager handled in the GameState receives all the type of enemies as a ```List<SO_SpawnableInfo>``` and will randomly spawn it in a spawning area.

### Information retrieved
In the folder Asset/Scripts/DataStruct, there are 4 ScriptableObject that will permit to create seamlessly a JSON, using JsonUtility.ToJson(_ourData_). The following information are :
- EnemyDataEvent : information about the enemy for 1 interaction (reaction)
    - the state of the enemy (when was it killed: perfect kill, ok kill, self destroyed, not touched)
    - the number of point earned (based on the time of state and the information of the enemy, will be negative is self destroyed)
    - the time interval between the interaction and the moment the enemy will be destroy (the closer to 0, the better). The time is in millisecond (found by : time * 1000)
- PlayerDataEvent : information about the player for 1 interaction (reaction)
    - the player username (in case of a multiplayer, it is better to know who interacted. The username should be unique)
    - the value of the continuous signal at the time of the interaction
    - whether the player has hit the target or not
    - the position in the 3D world of the pointer (specifically didn't put the mouse in the viewport view to keep the modularity of crossplatform and keep it in the 3D space)
    - the timestamp on when the action has been triggered.
- GameDataRecap : recap of the game situation after the level ends (summary). It encapsulates :
    - the number of enemy that spawned in total
    - the number of enemy missed
    - the timestamp of when the game ended (would be the same for all players)
- PlayerDataRecap : recap of the player state after the level ends (summary). It encapsulates :
    - the timestamp of when the player entered the game
    - its username (has not been computed, but can create a HUd - Canvas widget - with a text input and bind the text to the username value in the playerstate)
    - the total of event triggered (whether it was by clicking or when a monster got self destroyed)
    - the total number of clicks
    - the number of perfect kills
    - the number of ok kills
    - the number of missclick (clicked in the void/not in the target)
    - the average input value (based on the total event triggered). This is our simulation of a continuous input, faked by having a sin animation curve looping (time based value)

Everytime a reaction is called, it will concatenate the information of the player and enemy before sending it to the backend.
Same reasoning for the summary with the player and game recap.

In the GameState, there is a specific script called "backendHandler" that is there to convert the information into a JSON and call POST to the backend.

## Future improvements possible 
Here is a non-exhaustive list of possible improvement of this project :

- [ ] modular placement of the target (method already created, but right now fixed to "head")
- [ ] multiplayer (structure already in place. Methods in the GameState to retrieve information already thought as if there were multiple players)
- [ ] environment
- [ ] animation of the enemy based on the different states (animation graph already existant, can bind it to the Enum EEnemyState)
- [ ] Package (build) the project as standalone
- [ ] Backend not in localhost
- [ ] Main menu before entering the game with username and save based on player
- [ ] Multilevels (with menu and score recap in between levels to keep the engagement)

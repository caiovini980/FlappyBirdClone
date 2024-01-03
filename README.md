# FlappyBirdClone
The repository holds a Flappy Bird clone made with Unity.

## How the folders are structured?
Looking forward to improve the quality of imports on each dependecy, was considered to divide the folder structure accordingly to features (player, obstacle, timer, etc.) instead of class relevance (controller, entity, etc.)

##  How is the archtecture?
For the archtecture, was used multiple controllers to handle with specific parts of the solution. For example: gameplay, Inputs, Score and UI<br><br>
Muliple Behaviours were implemented to deal with certain object on scene, like: Obstacle spawn and hide, background, player and the obstacle itself<br><br>
For the communication between classes, events were majorly used for it, just like Roblox Studio. But this type of approach needs to be used carefully because it could create a lot of "Spagetti Code" pretty easly<br>

##  Where the assets could be found?
All of them were free assets from websites like [Kenney](https://kenney.nl/assets), [Google Fonts](https://fonts.google.com/) and GitHub.

##  Next steps
For the future:
- Apply order to the execution (the game manager will notify all the controllers to load up when the game started)
- Implement the Event Bus to handle the multiple dependecies
- Would use a [gameplay framework proposal](https://github.com/GiovanniZambiasi/gameplay-framework-unity) to make a better archtecture

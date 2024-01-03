# FlappyBirdClone
The repository holds a Flappy Bird clone made with Unity.

## How the folders are structured?
Visando melhorar a qualidade dos imports em cada dependencia, foi pensado em dividir as pastas de acordo com a feature que estava sendo implementada (player, obstacle, timer, etc.), e não com a importância daquela classe (controller, entity, etc.)

##  How is the archtecture?
Para a arquitetura, foram utilizados multiplos controllers para lidar com partes especificas da solucao, sendo elas: gameplay, SFX, Inputs, Score and UI.<br><br>
Alem disso, diversos Behaviours foram implementados para lidar com certos objetos da cena, como: Obstacle spawn and hide, Background, Player, give points to player and the obstacle itself.<br><br>
Ja para a comunicacao entre as classes, foi pensado em usar majoritariamente eventos para a comunicação entre as classes, assim como é feito no Roblox. Entretanto esse approach precisa ser utilizado com cautela, visto que pode gerar "spagetti code" com facilidade.<br>

##  Where the assets could be found?
Os assets foram todos gratuitos, e foram retirados de sites como [Kenney](https://kenney.nl/assets), [Google Fonts](https://fonts.google.com/) e o próprio GitHub.

##  Next steps
Para os próximos passos:
- Apply order to the execution (the game manager will notify all the controllers to load up when the game started)
- Implement the Event Bus to handle the multiple dependecies
- Would use a [gameplay framework proposal](https://github.com/GiovanniZambiasi/gameplay-framework-unity) to make a better archtecture

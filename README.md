## Balls To Cup

### Game Understanding and Development

Objective: 
Understand the game mechanics and develop the "Balls to Cup" game with 3 levels, including win and lose conditions.

Details:
User is able to rotate the tube by 360 degree by moving his finger on screen 
Created 3 levels of gameplay
Implement the win and lose conditions as per mentioned in GDD
Initially I used the FBX meshes present in the project for tubes but later generated tubes form svg files 


### Controller Development

Objective 
Develop a user-friendly game controller based on the example provided in the GDD to enhance the player's gaming experience.

Details:
I played the reference game MultiMaze , developed a similar controller that user should be able to move the tube by 360 by moving his finger on a circular path on screen 
Also user is able to move the tube by left or right 
From scripting i am calculating a specific angle ( through Controller & Twist Input script ) and then rotating the tube along z axis 
Also paid attention to rotation Speed, considering variations depending on the distance from the center.



### Gameplay Polish and Optimization

Objective:
 Enhance the gameplay experience by optimizing physics and fine-tuning the game's feel.

Details: 
Considering about the optimization & batches i used a single material on ball and just change the color property of same material
Batches vary between 40-110 depending upon the balls amount present in the scene 

Balls can be generated in a range of 10- 100 level wise 
While instantiating the balls in the scene i use flyweight in order to reduce the Garbage collection stuff (Simply duplicate the ball that is already present and in the scene and then change its properties 
I thought using the object pooling but i will not be the best case of it as the balls amount and colors varry from levels to levels, so i destroys the ball when balls fall off
Use a custom shader to create the sky in scene 
Used particles similar to the reference game, when the balls fall in the bucket 


### SVG-Based Level Generation

Objective: 
Develop a system to generate game levels quickly using SVG files for tube shapes.

Details:
Generating a TubeMesh with the SVG files was a little bit tricky task.
For reading the data on SVG files, i have to use the Vector Graphics library from unity 
First i have read data using SVG Parser and got the vector3 points, 
Tried to smoothen them through bezier curve
After that i created a Tube Generator script that generate tube on given points, there are several parameters that we can change including tube radius 
Generally i know that mesh generation at runtime is a little bit costly but i guess this task required us to create levels automatically at runtime, if its not required i would definitely make a editor for tube generation then save it as a prefab then load the prefab as per level i should be more convenient for me 



### Unity Inspector Editable Properties

Objective:
Make game properties easily editable via the Unity Inspector for better flexibility.

Details:


I have Made Several Editable Properties in the Menu Items in Unity Editor. In the Menu there is GameSettings there is a drop down of several editable properties 

When we click on LevelData  we can open the settings for level editor here we can set different edible properties that will vary from levels to levels , Also there is a settings for control configurations & Balls that will spawn have settings too 



Control Config in which we can set Rotation speed and minimum input threshold 


Also Balls Physics Properties and their random colors list


#### Project Version:
This project is developed on the Unity Version 2021.3.21f1 

 #### Build Platform:
The Build Platform is Android and build size is 37.5MB & testing device was RealMe GT Master 



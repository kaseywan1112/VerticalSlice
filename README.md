# GDIM33 Vertical Slice
## Milestone 1 Devlog
![Canvas Scripting Graph](e36e6975-c013-45ab-ae23-3536a6f3b046.png)
1.  I chose my canvas scripting graph, and I want to explain the Visual Scripting graph that controls my NPC interaction and the dialogue UI. The graph uses an On Update event to constantly listen for the player pressing the Space bar. When Space is pressed, the flow first goes into an If node to check a boolean variable called isDialogueOpen. I added this boolean because I found a bug during testing where the player could still click to move the ghost around while talking to the NPC. If the dialogue is not open (False), it then checks a custom Is Player Near node to see if the player is close enough to the NPC object. If both are true, it activates the Dialogue Panel game object and sets the isDialogueOpen variable to True. The top part of the graph handles closing the dialogue: when the player clicks the "ContinueButton", it triggers an On Button Click event, which deactivates the Dialogue Panel and sets isDialogueOpen back to False so the player can move normally again. Basically, it acts as a toggle switch that safely turns the conversation on and off without errors.

![Break Down](<BreakDown (1).jpg>)
2. Here is my new game breakdown, and in the previous version, the connection between the Player and the Dialogue UI was just a simple arrow. In the new update, I changed the "Player" node to show that it is entirely controlled by a State Graph with three main states: Start, Movement, and Dialogue. I also updated the Dialogue UI node to include the isDialogueOpen boolean variable, which acts as a bridge between the player's current state and the UI system.

Here is how the state machine works and how it relates to other systems in my game. The state machine basically acts as the main brain for the player. It starts and goes into the Movement state, which is where my point-and-click navigation system is active. When the player presses Space near an NPC, it triggers a transition that forces the state machine to exit Movement and enter the Dialogue state. This transition is directly related to two other major systems: the Movement System and the UI System. By putting the player in the Dialogue state, it completely shuts off the click-to-move scripts, so the character is frozen in place, fixing the movement bug. At the same time, it talks to the UI system to display the dialogue box and the Continue button.


## Milestone 2 Devlog
1. I decided to build a Cinematic Event System using Timeline to handle two cutscenes: finding the magic lamp and rubbing it to summon the genie, since I already have my inventory and basic movement set up. (Note: My W5 feature was Branching Dialogue, so this is a new one)

Step1: 
1. Make a second virtual camera just to get a close-up shot of the magic lamp.
2. Put a trigger zone by the river, so when the player walks into it, the camera switches over to the close-up.
3. Update my interaction script so that when the player picks up the lamp, it hides the object and drops it straight into the InventoryManager.

(Test: Run the game, walk up to the river, check if the camera swaps, and make sure the lamp actually goes into my inventory UI.)

Step 2:
1. Make two Playable Directors (Timelines). For the first one, just animate the lamp flying out of the lake and landing on the grass.
2. For the second one, record an Animation Track of the player ghost wiggling to look like it's rubbing the lamp.
3. Drop the smoke particle effect into a Control Track so it goes off right after the wiggle.
4. Add an Activation Track to turn on the Bull NPC right inside the smoke, and an Animation Track to make it scale up from 0 to 1 so it looks like it's popping out.

(Test: Hit play on the Timeline window and check if the timing of the smoke and the cow popping out looks good together.)

Step 3:
1. Hook up the inventory UI button (using Event Triggers for PointerUp/Down) so clicking the lamp triggers the second Timeline to play.
2. Write a C# script to grab the whole Timeline stage (the smoke and the cow objects) and teleport it exactly 2 units in front of the player, so the animation plays wherever I am on the map.
3. Add a quick NavMesh check in the script so if I'm facing the water, the cow spawns on the nearest valid grass instead of drowning in the lake.

(Test: Run the game, run right up against the lake, open the inventory, click the lamp, and check if the cow safely spawns on the ground facing me.)

2. Yes, it really helped. At first, I only had three big directions in my head, but writing this breakdown acted like a notebook that saved my logic before I got lost in the editor. I actually used my Test steps a lot during the process to make sure the current piece was working before moving to the next one. If I were to do it again, I would improve my breakdowns by writing down the exact variable names I plan to use right in the steps (like isGenieActive). That way, I wouldn't have to stop and think about naming things while coding, just like what we do in our lecture.

<img width="1345" height="459" alt="微信图片_〉〇〉」-〇「-〈》_〉〈《〇「『_』〈〈" src="https://github.com/user-attachments/assets/b1a689db-3aae-47b4-99f4-672f1bea1857" />
<img width="574" height="461" alt="image" src="https://github.com/user-attachments/assets/76c07488-3923-463d-b519-4eee29e115a9" />

3. First, I built my player's NavMesh click-to-move using Visual Scripting. This graph perfectly bridges with my C# code because the very first thing it does is check an Object Variable (isDialogueOpen) controlled by my C# manager. If I am talking to the Genie, the graph uses a Negate node to instantly block the mouse input so I can't walk away. However, once the player was moving, I realized a static camera felt terrible. Doing smooth Lerp math is really messy in visual nodes, so I wrote a custom C# script (CameraFollow) to handle the smooth tracking in the background. Then, another issue came up: when I decorated the map with trees, the camera kept getting blocked by the leaves. To fix this, I wrote a second C# script (TreeHide). It constantly shoots a physics Raycast from the camera to the player. If the raycast hits a tree collider, the C# script temporarily fades out the tree's material so the player is always visible.

4. Please grade my Timeline system. I have two animations: one where the lamp flies to the shore when you walk right and go through a hitbox, and one where you rub the lamp to summon the Genie with smoke effects. (You can interact with the lamp by left-clicking on the lamp icon) 




## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets

[Ghost character Free](https://assetstore.unity.com/packages/3d/characters/creatures/ghost-character-free-267003)
[Farm Assets](https://animagic3d.itch.io/farm-assets-1)


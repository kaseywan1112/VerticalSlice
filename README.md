# GDIM33 Vertical Slice
## Milestone 1 Devlog
![Canvas Scripting Graph](e36e6975-c013-45ab-ae23-3536a6f3b046.png)
1.  I chose my canvas scripting graph, and I want to explain the Visual Scripting graph that controls my NPC interaction and the dialogue UI. The graph uses an On Update event to constantly listen for the player pressing the Space bar. When Space is pressed, the flow first goes into an If node to check a boolean variable called isDialogueOpen. I added this boolean because I found a bug during testing where the player could still click to move the ghost around while talking to the NPC. If the dialogue is not open (False), it then checks a custom Is Player Near node to see if the player is close enough to the NPC object. If both are true, it activates the Dialogue Panel game object and sets the isDialogueOpen variable to True. The top part of the graph handles closing the dialogue: when the player clicks the "ContinueButton", it triggers an On Button Click event, which deactivates the Dialogue Panel and sets isDialogueOpen back to False so the player can move normally again. Basically, it acts as a toggle switch that safely turns the conversation on and off without errors.

![Break Down](<BreakDown (1).jpg>)
2. Here is my new game breakdown, and in the previous version, the connection between the Player and the Dialogue UI was just a simple arrow. In the new update, I changed the "Player" node to show that it is entirely controlled by a State Graph with three main states: Start, Movement, and Dialogue. I also updated the Dialogue UI node to include the isDialogueOpen boolean variable, which acts as a bridge between the player's current state and the UI system.

Here is how the state machine works and how it relates to other systems in my game. The state machine basically acts as the main brain for the player. It starts and goes into the Movement state, which is where my point-and-click navigation system is active. When the player presses Space near an NPC, it triggers a transition that forces the state machine to exit Movement and enter the Dialogue state. This transition is directly related to two other major systems: the Movement System and the UI System. By putting the player in the Dialogue state, it completely shuts off the click-to-move scripts, so the character is frozen in place, fixing the movement bug. At the same time, it talks to the UI system to display the dialogue box and the Continue button.


## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets

[Ghost character Free](https://assetstore.unity.com/packages/3d/characters/creatures/ghost-character-free-267003)
[Farm Assets](https://animagic3d.itch.io/farm-assets-1)


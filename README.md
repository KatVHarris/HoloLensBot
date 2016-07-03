# HoloLensBot
## Summary
Project for integrating Microsoft Bot framework into Unity for Natural Language understanding in HoloLens applications. The HoloLens portion of the code was adapted from the HoloToolkit code from Microsoft https://github.com/Microsoft/HoloToolkit-Unity 

## Author
Katherine "Kat" Harris - Technical Evangelist at Microsoft. @KATVHARRIS - Twitter

## Functionality 
* Input - Gaze, Tap, Voice
* Shared - Multiplayer interactions

## Feature List
* Helper bot - fills out form
* Login Form
* Settings input Form

## Technical Notes
07/03/16
Built Unity Project with the Hololens settings and tried integrating the Microsoft.Bot.Builder into the project. 
Errors occured: Microsoft.Bot.Builder 1.2.5 is not compatible with UAP,Version=v10.0. 
Potential Solution - Downgrade UAP version? (This might cause errors for Unity and Hololens)
Potential Solution - Wait for Bot Builder to be upgraded
Potential Solution 3 - Use the Node.js SDK to create a server for Unity to ping and access. 

Currently is a simple bubble pop game. 

Using the #WIN_UWP tag for UWP specific code, aka calling the bot framework. This tag spins off a seperate thread to handle UWP specific API calls. Will need to return from this thread to the Unity Thread if we want to implement Chain Dialogs.



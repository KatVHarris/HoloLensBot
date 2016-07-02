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
Currently is a simple bubble pop game. 

Using the #WIN_UWP tag for UWP specific code, aka calling the bot framework. This tag spins off a seperate thread to handle UWP specific API calls. Will need to return from this thread to the Unity Thread if we want to implement Chain Dialogs.



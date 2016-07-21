# HoloLensBot
## Summary
This project integrates Microsoft's LUIS API into Unity for Natural Language understanding in HoloLens applications. The HoloLens portion of the code was adapted from the HoloToolkit code from Microsoft https://github.com/Microsoft/HoloToolkit-Unity. The LUIS API in the sample folder is using the ALIEBot API (https://github.com/KatVHarris/ALIEbot) that answers questions about the show The 100. 

## Author
Katherine "Kat" Harris - Technical Evangelist at Microsoft. @KATVHARRIS - Twitter

## Project Setup
To set up dictation and LUIS drag the revised DictationController (aka Communicator) and LUISManager_Hololens prefabs into your scene. They can be found as Prefabs in the Holograms folder. 

## Future Feature List
* Create Inspector attribute to plug-in any LUIS library
* Create Sample Scene Folder and Sample Scene


## Technical Notes
A blog post about getting HoloLens Speech and Dictation working with LUIS can be found here: http://bit.ly/HoloLUIS

A blog post about the project can be found here: http://katvharris.azurewebsites.net/blog/unity-luis-json/
Currently integrated with LUIS.ai API for ALIEbot. The user can currently type request to get back infromation about a character. Next step is to use Voice Commands to Trigger LUIS calls.  

07/06/16 
The LUIS endpoint is working with direct ping from UnityWeb Request

07/05/16
Currently looking into Hololens and UAP Speech to Text API. Currently there is a listener to trigger certain commands in Hololens, but those are one word solutions, we want a more robust natural language processing to interact with a bot framework. 

Acessing LUIS directly can also be an option - EXAMPLE: 
Making a HTTP request to the LUIS endpoint https://api.projectoxford.ai/luis/v1/application?id=a287f18f-4ae3-4346-b712-2bb9468f81c2&subscription-key=f2b59c258e5042a3b265498b92acd8a8&q=tell%20me%20about%20Clarke

This will return a JSON object that can be read for the next command. 


07/03/16
Built Unity Project with the Hololens settings and tried integrating the Microsoft.Bot.Builder into the project. 
Errors occured: Microsoft.Bot.Builder 1.2.5 is not compatible with UAP,Version=v10.0. 
Potential Solution - Downgrade UAP version? (This might cause errors for Unity and Hololens)
Potential Solution - Wait for Bot Builder to be upgraded
Potential Solution 3 - Use the Node.js SDK to create a server for Unity to ping and access. 


Currently is a simple bubble pop game. 

Using the #WIN_UWP tag for UWP specific code, aka calling the bot framework. This tag spins off a seperate thread to handle UWP specific API calls. Will need to return from this thread to the Unity Thread if we want to implement Chain Dialogs.

## Functionality 
* Input - Gaze, Tap, Voice
* Shared - Multiplayer interactions






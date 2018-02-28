# APPLICATION NAME
This application is a form of the "Hot Dog or Not Hot Dog" Google API Photo Recognition Application. It allows the user to take a photo and then uses Google API technology to guess what that photo is. I wrote this application for the sake of the assignment but plan to add to it in the future to make it more than what was initially required.

## System Design 
This is where you specify all of the system's requirements.  This section should accurately portray the complete operation of your application.  Provide scenarios, use cases, system requirements, and diagrams/screenshots of the system.
This application was designed targetting Android version 7.1 and higher, but will run on most devices, provided they have a camera.
The most common use of an application like this is probably just to see what things it can and cannot guess when you take the picture, so it is currently more of a small "game" than anything potentially useful.

Below are some screenshots taken on my phone, a Samsung Galaxy S7 Edge to illustrate how the application works:

The first screen features only a button used to open the camera on the phone.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205704651014149/Screenshot_20180227-162310.png" width="500"             height="700"/>
</p>

The next screenshot shows the screen after taking a photo. In this example, I used a Nalgene water bottle.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205712645226496/Screenshot_20180227-162328.png" width="500"             height="700"/>
</p>

After confirming the picture is acceptable, the user is taken to a screen where the app's guess is shown.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205714616418305/Screenshot_20180227-162342.png" width="500"             height="700"/>
</p>

If the user chooses "No" on the previous screen, they will be prompted to enter the actual name of the object of which they took a photo.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205711865217034/Screenshot_20180227-162352.png" width="500"             height="700"/>
</p>

After entering the actual name of the object, users will submit that entered text to the app, which will then compare it to the top list of potential objects the API thought it was.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205714339856414/Screenshot_20180227-162425.png" width="500"             height="700"/>
</p>

If the object entered by the user was not found in the top list of objects, they will be taken to this screen.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205713886740481/Screenshot_20180227-162447.png" width="500"             height="700"/>
</p>

However, if the user-entered object is found on the list, the application will return this screen, showing the percent chance that the object pictured was the object they entered.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418212395199102986/Screenshot_20180227-170548.png" width="500"             height="700"/>
</p>


If the application guesses right on the first try, and the user selects "Yes", they will be taken to this SUCCESS screen.
<p align="center">
  <img src="https://cdn.discordapp.com/attachments/250193100687802368/418205716323500032/Screenshot_20180227-162540.png" width="500"             height="700"/>
</p>

## Building the Project
 **Prerequisites:**
  - Unity Editor 6000.0.60f1
  - Visual Studio or Visual Studio Code
  - .NET Standard 2.1

 **1. Clone the Repository** to your local machine.
 
 **2. Launch the Unity Hub** and add the project folder `FlappyBird` as a new project (6000.0.60f1).
 
 **3. Launch the project on the Editor**
 
 **4.** Once the project is successfully loaded in the editor, go to `File` -> `Build and Run` to compile and launch the game. The resulting game will be stored in the `Build` folder in the root folder of the repo.

---

## Controls

* **Space key**: press to fly up

* **ESC key**: press to pause the game

---

## Features

* Difficulty scaling over time
* Procedurally generated obstacles and collectibles
* SFX and VFX corresponding with the player's actions

---

## Parallax Background

* The game features 5 different background textures, each having a different scroll speed compared to the foreground obstacles.
* Each layer is composed of 3 parts lined up next to each other, 2 of which is required to fill the screen.
* When the leftmost part scrolls outside of the player's view, it is then moved to the right so that the edge is exactly next to the rightmost part.
* This is repeated to create an infinitely scrolling backgroun

---

## Asset sources
* [BoldPixels Font](https://assetstore.unity.com/packages/2d/fonts/boldpixels-font-332078)
* [Pixel Adventure 1](https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360)
* [2D Pixel Art Platformer | Biome - American Forest](https://assetstore.unity.com/packages/2d/environments/2d-pixel-art-platformer-biome-american-forest-255694)
* Shostakovich - Jazz Suite No. 1: I. Waltz




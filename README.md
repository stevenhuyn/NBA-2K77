**The University of Melbourne**

# COMP30019 – Graphics and Interaction

Final Electronic Submission (project): **4pm, Fri. 6 November**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, Sun. 25 October**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully

- [ ] Brief explanation of the game

- [ ] How to use it (especially the user interface aspects)

- [ ] How you modelled objects and entities

- [ ] How you handled the graphics pipeline and camera motion

- [ ] Descriptions of how the shaders work

- [ ] Description of the querying and observational methods used, including: description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [ ] Document the changes made to your game based on the information collected during the evaluation.

- [ ] A statement about any code/APIs you have sourced/used from the internet that is not your own.

- [ ] A description of the contributions made by each member of the group.

## Table of contents

- [Team Members](#team-members)
- [Explanation of the game](#explanation-of-the-game)
- [Technologies](#technologies)
- [Using Images](#using-images)
- [Code Snippets ](#code-snippets)

## Team Members

| Name           |     Task      |    State |
| :------------- | :-----------: | -------: |
| Student Name 1 |   MainScene   |     Done |
| Student Name 2 |    Shader     |  Testing |
| Student Name 3 | README Format | Amazing! |

## Explanation

Our game is a basketball game set in the year 2077. In the future, only slam dunks are allowed and you are equipped with a grappling hook. Swing and dunk your way through multiple handcrafted levels set in a cyberpunk-esque universe.

# How to Play

TK: Explain the main menu.

A tutorial will guide you through the key game mechanics. We have implemented familiar first-person shooter controls: WASD to move, space to jump and left click to shoot your grappling hook. The grappling hook can be used to swing and pull balls towards you by continuing to hold down left click. Your crosshair will change colour when you are aiming at a hoop or ball.

Once you're done with the tutorial, try to build up the highest score possible on each of our levels! Score points by picking up balls, flying and dunking. View your score and multiplier in the top-right.

## Technologies

Project is created with Unity 2019.4.3f1.

## Modelling Objects and Entities

For development simplicity, the majority of our objects are unity primitives. We styled these with custom materials and post-processing effects. The hoop's torus was designed in Blender for our project.

Complex entities, such as the hoop and player body, are a combination of multiple objects and colliders. We sourced grappling gun, skybox and particle effect materials from the Unity asset store.

## Graphics Pipeline

## Camera Motion

## Shaders

# Shader 1: Rope

<p align="center">
  <img src="Images/RopeShader.png"  width="300" >
</p>

# Shader 2

# Special Effects

## Querying and Observational Methods

### Demographics

- 21 Male Student
- 21 Male Student
- 21 Male Student
- 12 Male Student
- 18 Male Uni Student

### Methodology

We used two Querying/Observational methods:

- Think Aloud
- Post Walkthrough Interview

#### Think Aloud

For our observational method, we gave the users a small set of instructions on how to play the game as well as an instruction to voice out their thoughts.

From there, minimal input from the interviewer was given as users attempted to play through our levels. We noted down notable responses and collated them together.

#### Post Walkthrough Interview

The post walkthrough interview questions we used were:

- Did the game feel too fast or too slow? How was the pace?
- Was the game easy to understand?
- What pain points did you encounter during the game?
  - Aiming?
  - Movement?
  - Ball dunking?
    Would you change any other aspect of the game?
  - Visual/Clarity
  - Gameplay
    What new features would you like?
    How do the controls compare to other games you’ve played?
    Do you have any other thoughts you would like to discuss?

These questions were ordered in such a way that easier questions were at the start to warm up the individual while harder questions were sandwiched in the middle. Finally we finish off with easier questions at the end.

### Feedback

Through our observational/querying methodologies, we were able to pick up on common complaints and misunderstandings with the mechanics of our game. These included complaints about 'game feel' as well as visual clarity in general. Here are several of them.

#### game felt too floaty

A common complaint with our gameplay was that it felt very 'floaty'. Especially the jumping as well as the movement through the air.

#### Grappling to ring results in players orbiting the ring

When users grappled directly to the ring in an attempt to slam dunk their ball into it, it would often result in users orbiting the ring rather than hitting the ring and dunking it in. This was problematic and took users out of the experience

#### Visual Noise from skybox

The skybox presented some issues with clarity. Since the skybox also featured a bright blue, it often made it difficult for players to make out the platforms against the similarly coloured backdrop.

#### Lack of instruction

Players mentioned that without us telling them the controls, there would be no way to discover the mechanics of the game.

#### Difficulties with aiming

We observed that users often committed to grappling balls expecting it to hit and then were often disappointed or frustrated when it wouldn't hit.

## Changes after Evaluation

In response to the feedback that we received we implemented a multitude of changes.

#### Game felt too floaty

- This was fixed by tuning the physics to avoid floating too much
  _Image_

#### Grappling to ring results in players orbiting the ring

This was fixed by adding an --- (James do you want to explain?)

#### Visual Noise from skybox

We fixed this by modifying the skybox to be more contrasting.

#### Lack of Instruction

This was fixed by adding a tutorial level which guided the user through the controls and objectives of the game in an interactive manner.

#### Difficulties with Aiming

There were several solutions that we came up with for making it easier to aim at balls. Firstly, we wanted to give users a larger margin of error. This was done by sending a larger hitbox in roughly a cone shape so that if the ball is under the reticle, the hook would move towards the ball. Secondly, we also experimented with giving the players feedback as to whether their hook would hit. This was achieved by changing the reticle colour when the hook would definitely hit the ball. These two methods combined turned out to feel significantly better.

## External Resources
https://freesound.org/people/cydon/sounds/268557/
https://freesound.org/people/HuvaaKoodia/sounds/77172/
## Individual Contributions

### Gatlee Kaw

### Steven Nguyen

### Matthew Lui

### James Dyer

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets

You can include a code snippet here, but make sure to explain it!
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```

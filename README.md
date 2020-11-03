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
* [Team Members](#team-members)
* [Explanation of the game](#explanation-of-the-game)
* [Technologies](#technologies)
* [Using Images](#using-images)
* [Code Snipets ](#code-snippets)

## Team Members

| Name | Task | State |
| :---         |     :---:      |          ---: |
| Student Name 1  | MainScene     |  Done |
| Student Name 2    | Shader      |  Testing |
| Student Name 3    | README Format      |  Amazing! |

## Explanation
Our game is a basketball game set in the year 2077. In the future, only slam dunks are allowed and you are equipped with a grappling hook. Swing and dunk your way through multiple handcrafted levels set in a cyberpunk-esque universe. 

# How to Play
WASD for movement
R to reset
Left click to shoot out a grappling hook

Objective: ?? 
## Technologies
Project is created with:
* Unity 2019.4.3f1

## Modelling Objects and Entities

## Graphics Pipeline

## Camera Motion

## Shaders

# Shader 1: Rope

We use a shader to add a visual effect to the rope.

more details later

# Shader 2

# Special Effects

## Querying and Observational Methods

### Demographics
- 21 Male Student
- 21 Male Student
- 21 Male Unemployed
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
Through our observational/querying methodologies, we were able to pick up on common complaints and misunderstandings with the mechanics of our game. These included complaints about 'game feel' as well as visual clarity in general.

#### Game felt too floaty
A common complaint with our gameplay was that it felt very 'floaty'. Especially the jumping as well as the movement through the air. 
TODO



## Changes after Evaluation

## External Resources

## Individual Contributions

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





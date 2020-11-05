**The University of Melbourne**

# COMP30019 – Graphics and Interaction

Final Electronic Submission (project): **4pm, Fri. 6 November**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, Sun. 25 October**

# Project-2 README

# TODO: clean up this first section

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

## Table of Contents
- [COMP30019 – Graphics and Interaction](#comp30019--graphics-and-interaction)
- [Project-2 README](#project-2-readme)
- [TODO: clean up this first section](#todo-clean-up-this-first-section)
  - [Table of Contents](#table-of-contents)
  - [Explanation of the Game](#explanation-of-the-game)
    - [How to Play](#how-to-play)
  - [Overall Design Goals](#overall-design-goals)
- [TODO: describe our design concept + design pillars](#todo-describe-our-design-concept--design-pillars)
  - [Technologies](#technologies)
  - [Modelling Objects and Entities](#modelling-objects-and-entities)
  - [Graphics Pipeline](#graphics-pipeline)
  - [Camera Motion](#camera-motion)
  - [Shaders](#shaders)
    - [Shader 1: Rope](#shader-1-rope)
    - [Shader 2: Hoop Ripple](#shader-2-hoop-ripple)
  - [Special Effects](#special-effects)
    - [Hoop Explosion Effect](#hoop-explosion-effect)
  - [Querying and Observational Methods](#querying-and-observational-methods)
    - [Demographics](#demographics)
    - [Methodology](#methodology)
      - [Think Aloud](#think-aloud)
      - [Post Walkthrough Interview](#post-walkthrough-interview)
    - [Feedback and Solutions](#feedback-and-solutions)
      - [The game felt too floaty](#the-game-felt-too-floaty)
      - [Visual Noise from skybox](#visual-noise-from-skybox)
      - [Lack of instruction](#lack-of-instruction)
      - [Difficulties with aiming](#difficulties-with-aiming)
    - [Grappling to hoop results in players orbiting the hoop](#grappling-to-hoop-results-in-players-orbiting-the-hoop)
  - [Individual Contributions](#individual-contributions)
    - [Gatlee Kaw](#gatlee-kaw)
    - [Steven Nguyen](#steven-nguyen)
    - [Matthew Lui](#matthew-lui)
    - [James Dyer](#james-dyer)
  - [External Resources](#external-resources)

## Explanation of the Game

Our game is a basketball game set in the year 2077. In the future, only slam dunks are allowed and you are equipped with a grappling hook. Swing and dunk your way through multiple handcrafted levels set in a cyberpunk-esque universe.

### How to Play

TK: Explain the main menu.

A tutorial will guide you through the key game mechanics. We have implemented familiar first-person shooter controls: WASD to move, space to jump and left click to shoot your grappling hook. The grappling hook can be used to swing and pull balls towards you by continuing to hold down left click. Your crosshair will change colour when you are aiming at a hoop or ball.

Once you're done with the tutorial, try to build up the highest score possible on each of our levels! Score points by picking up balls, flying and dunking. View your score and multiplier in the top-right.

## Overall Design Goals

# TODO: describe our design concept + design pillars

## Technologies

Project is created with Unity 2019.4.3f1.

## Modelling Objects and Entities

For development simplicity, the majority of our objects are unity primitives. We styled these with custom materials and post-processing effects. The hoop's torus was designed in Blender for our project.

Complex entities, such as the hoop and player body, are a combination of multiple objects and colliders. We sourced grappling gun, skybox and particle effect materials from the Unity asset store.

## Graphics Pipeline

## Camera Motion

## Shaders

### Shader 1: Rope

<p align="center">
  <img src="Images/RopeShader.png" width="300">
</p>

### Shader 2: Hoop Ripple

<p align="center">
  <img src="Gifs/HoopShader.gif" width="500">
</p>

Our second shader is for a wave/ripple-like effect for the inside surface of each hoop. This is achieved by displacing the height of each of the vertices on the surface of the hoop by a sine wave which is a function of the distance from the centre and the current time, generating a radially symmetric rippling effect.

This is calculated in a vertex shader as follows:

```glsl
// Apply a sine wave displacement based on distance to center
float d = length(float2(v.vertex.x, v.vertex.z));
float freq = 1.0 / _Wavelength;
float offset = _Time.y * _Speed;
float t = d * freq + offset;
float h = sin(t);
v.vertex.y = _Amplitude * h;
```

In order to allow for lighting to be calculated properly for the displaced vertices, the normals are also recalculated. If we consider the function we are applying to be `f(x, z)` mapping to an `(x, y, z)` point, we can get the following expression:

`f(x, z) = (x, _Amplitude * sin(freq * d + offset), z)`

In order to calculate the normal vector, we can take the cross product of the partial derivatives `df/dx` and `df/dz`. We have

`df/dx = (1, _Amplitude * cos(t) * freq / d * x, 0)`

and

`df/dz = (0, _Amplitude * cos(t) * freq / d * z, 1)`

where `t = d * freq + offset` as above.

Taking the cross product gives us the vector:

`(_Amplitude * cos(t) * freq / d * x, -1, _Amplitude * cos(t) * freq / d * z)`

In addition, in order to prevent artifacts occuring near the centre of the mesh, we enforce that normals very near to the centre face strictly downwards, creating a small flat spot in the middle. This issue appeared to be due to an irregularity with our mesh, which was created by squashing a sphere down to be very thin and treating it as a disc.

Overall, this leads us to the following:

```glsl
// Calculate new normal
float3 normal;
if (d < 0.01) {
  // Keep the centre looking consistently flat
  normal = float3(0, -1, 0);
} else {
  // Normalized cross product of df/dx and df/dz (partial derivatives)
  normalize(float3(
    _Amplitude * cos(t) * freq / d * v.vertex.x,
    -1,
    _Amplitude * cos(t) * freq / d * v.vertex.z));
}
```

In order to accentuate the overall rippling effect, we also vary the color of each vertex based on the height calculated from the sine wave. This is done by linearly interpolating a coefficient to multiply the color by between some set limits.

```glsl
// Interpolate color based on adjusted height
float c = lerp(_ColorMinValue, _ColorMaxValue, (h + 1) / 2);
o.color.rgb = c * _Color;
```

Here the value of `h` is `sin(d * freq + offset)` as calculated previously.

Finally, a Phong illumination model is applied in a pixel shader using code taken from the workshops to get some simple lighting effects.

## Special Effects

### Hoop Explosion Effect

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

### Feedback and Solutions

Through our observational/querying methodologies, we were able to pick up on common complaints and misunderstandings with the mechanics of our game. These included complaints about 'game feel' as well as visual clarity in general. Here is an overview of the most common points of feedback, along with the solutions we came up with.

#### The game felt too floaty

A common complaint with our gameplay was that it felt very 'floaty'. Especially the jumping as well as the movement through the air.

- This was fixed by tuning the physics to avoid floating too much
  _Image_

#### Visual Noise from skybox

The skybox presented some issues with clarity. Since the skybox also featured a grid-like pattern of black and bright blue, it often made it difficult for players to make out the platforms against the similarly coloured backdrop.

This was solved by changing the backdrop to one which is more contrasting with the platforms and other game objects. (Mostly black, as if in space, but above the Earth to avoid just being in a black void.)

#### Lack of instruction

Players mentioned that without us telling them the controls, there would be no way to discover the mechanics of the game. In response, we implemented a simple tutorial which which guided the user through the controls and objectives of the game in an interactive manner.

#### Difficulties with aiming

We observed that users often committed to grappling balls, expecting it to hit, and then were often disappointed or frustrated when they missed the ball. Since the core gameplay is based around near-constant fast and dynamic movement, aiming at small targets like the balls is particularly difficult. Additionally, we didn't want difficulties with aiming to be prominent as we did not consider it to be the core drive behind the game concept - that being skillful movement and flow.

To solve this, we implemented an "aim assist" feature to improve aiming usability and decrease the chance of a player missing a ball and interrupting the flow of gameplay.

This was implemented by first conducting a sphere cast, covering a fairly sizeable area in the general direction the player is looking, and checking if a ball is present. If this succeeds, the direction vector in which the player is looking is shifted a small amount of degrees (2-3) in the direction of the detected ball. (This number was chosen to match the size of the crosshair in the player's view). A raycast is then done in this direction to determine if the player has a direct line of sight to the ball. If this succeeds, the player's crosshair changes color to orange to clearly indicate that a shot hook would hit the target. If the player then fires a hook, the hook is aimed towards that point.

After testing out this feature, we found that it greatly improved the general flow of the game, and extended it to apply to hoops as well. 

### Grappling to hoop results in players orbiting the hoop

Several players encountered an issue where they would attempt to grapple towards a hoop while having significant lateral velocity, which caused them to overshoot. Since the force being applied by the rope was constant, they then entered a stable elliptical orbit around the hoop, and kept swinging around in circles as long as they held down the button.

We had also found this issue while testing manually, and found that it greatly obstructed the gameplay, as it prevents you from easily reaching the hoop to dunk a ball, an essential part of the core gameplay loop.

To fix this, we applied a positive jerk (rate of change of acceleration) to the pulling force. More simply, we made the pulling force increase the longer the player has been grappling. We set this value low for when grappling onto regular surfaces, but increased it greatly for the hoop specifically. This increasing force over time causes the player to "crash" out of orbit and hit the hoop very quickly in most instances. Even if they overshoot greatly, most of the time they will only complete a few revolutions before reaching the hoop, so we considered the problem to be solved.

## Individual Contributions

### Gatlee Kaw

### Steven Nguyen

### Matthew Lui

### James Dyer

I mostly worked on creating and tuning the basic gameplay loop, including the movement and grappling mechanics. Over the course of the project, I implemented the following features:

- The grappling hook (firing, pulling into walls, pulling balls towards you)
- Spawning and collecting balls
- Tuning basic movement and jumping
- The crosshair and aim assist
- Fixing issues we discovered with game feel in the movement and shooting mechanics
- The ripple effect shader for the hoop interior

## External Resources

https://freesound.org/people/cydon/sounds/268557/
https://freesound.org/people/HuvaaKoodia/sounds/77172/

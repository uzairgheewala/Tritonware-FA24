# Last Hours

![Last Hours Title Screen](path/to/title_screen_image.png)

**Theme:** On the Edge of Your Seat

**Name:** Last Hours

## Table of Contents

- [Project Overview](#project-overview)
- [Team Members](#team-members)
- [Setup Instructions](#setup-instructions)
  - [Prerequisites](#prerequisites)
  - [Cloning the Repository](#cloning-the-repository)
  - [Launching the Game in Unity](#launching-the-game-in-unity)
- [Game Design](#game-design)
  - [Setting](#setting)
  - [Mansion Layout](#mansion-layout)
  - [Characters](#characters)
  - [Backstory](#backstory)
  - [Key Plot Points](#key-plot-points)
  - [Motives for Committing Murder](#motives-for-committing-murder)
  - [Accusation Mechanics](#accusation-mechanics)
  - [Trust Meter](#trust-meter)
- [Art & UI](#art--ui)
  - [Art Style](#art-style)
  - [Assets Status](#assets-status)
  - [UI Elements](#ui-elements)
- [Audio](#audio)
  - [Sound Effects](#sound-effects)
  - [Background Music](#background-music)
- [Programming](#programming)
  - [Character Management](#character-management)
  - [Inventory System](#inventory-system)
  - [Scene and Environment Management](#scene-and-environment-management)
  - [Movement](#movement)
  - [Dialogue System](#dialogue-system)
  - [Relationships & Interactions](#relationships--interactions)
  - [Interrogation & Insanity Mechanics](#interrogation--insanity-mechanics)
  - [Game State Management](#game-state-management)
- [Current Status](#current-status)
- [Future Work](#future-work)
- [Contributing](#contributing)
- [Contact](#contact)

---

## Project Overview

**Last Hours** is a detective-style game where players must survive and uncover the mysteries of a haunted manor. Set during a tense college reunion, players investigate murders occurring one by one as they attempt to identify the culprit before all characters fall victim.

**Priority of Focus:**
- **Mechanics**
- **Art**
- **Story**

## Team Members

| Name             | Discord        | GitHub Username  | Email                 |
|------------------|----------------|------------------|-----------------------|
| Jolina Leong     | j.xuli         | joleongcse8a     |                       |
| William          | maxwill1083    | Wjtai            |                       |
| Uzair Gheewala   | kayabaengine   | uzairgheewala    |                       |
| Annie Xu         | annie007       | Annie0728        | annie.xu777@yahoo.com |
| Benjamin Mendoza | mcbeanb        | BenM2405         |                       |
| Teo Imoto-Tar    | teo, 1teo, teooi | teooi           |                       |

## Setup Instructions

### Prerequisites

- **Git:** [Download Git](https://git-scm.com/downloads) and follow the installation instructions.
- **Unity:** [Download Unity](https://unity.com/download) and install the latest Unity release.

### Cloning the Repository

1. **Create a GitHub Account:** If you don't have one, go to [GitHub](https://github.com/login) and sign up.
2. **Clone the Repository:**
   - Open Terminal or Git Bash.
   - Navigate to your desired directory:
     ```bash
     cd path/to/your/directory
     ```
   - Configure your Git username and email:
     ```bash
     git config --global user.name "Your Name"
     git config --global user.email "you@example.com"
     ```
   - Clone the repository:
     ```bash
     git clone https://github.com/uzairgheewala/Tritonware-FA24.git
     ```

*Refer to [Git Cheat Sheet](https://education.github.com/git-cheat-sheet-education.pdf) for additional Git commands.*

### Launching the Game in Unity

1. **Open Unity Hub.**
2. **Add the Project:**
   - Click **Add**.
   - Select **Add project from disk**.
   - Navigate to the cloned repository folder (`Tritonware-FA24`) and open it.

## Game Design

### Setting

A college reunion of 5 (+2 if time permits) people takes place in the doctor's mansion. The isolated mansion, far from civilization, becomes the setting for a series of murders occurring throughout the night. Players must identify the murderer before all characters are killed.

### Mansion Layout

**1F:**
- Entrance Hall
- Dining Room
- Kitchen
- Living Room
- Courtyard
- Garage

**2F:**
- Bedroom
- Bathroom
- Library
- Recreation Room
- Hidden Room (Owner’s or Wife’s)

**3F:**
- Attic
- Rooftop Balcony

### Characters

**Player:**
- Self-insert character, a forensic pathologist who reconnects with old friends.

**Doctor (Daniel):**
- Owner of the mansion.
- Prideful, dishonest, and manipulative.
- Secretly involved in illegal drug sales.

**Wife (Wendy):**
- Timid and kind-hearted facade.
- Daughter of the hospital chairman.
- Controls her husband and manipulates situations to her advantage.

**Journalist (James):**
- Friendly and easygoing.
- Investigates the doctor's secrets.
- Has feelings for the Player.

**Pharmaceutical Scientist (Phoebe):**
- Genius, aloof, and focused.
- Assists the doctor in illegal drug production.
- Feels guilty about the journalist’s situation.

*Additional characters can be added if time permits.*

### Backstory

All characters pursued medical careers and met in medical school. The Player, a forensic pathologist, had a fallout with the group due to a drug-stealing incident orchestrated by Wendy, the wife. The Player wrongly accused others, leading to the journalist’s expulsion and Phoebe's career derailment. Years later, the Doctor invites everyone for a reunion to uncover who sent him a threat letter.

### Key Plot Points

- **Threat Letter:** Anonymous letter threatening to expose the Doctor's secrets.
- **Reunion Party:** Doctor aims to identify the culprit among his friends.
- **Hidden Agendas:** Each character has motives tied to past events and current secrets.

### Motives for Committing Murder

- **Doctor:**
  - Escape from Wife’s control.
  - Preserve his illegal drug business.
  
- **Wife:**
  - Protect her family's reputation.
  - Control her husband and eliminate threats.

- **Journalist:**
  - Revenge for expulsion.
  - Protect the PS (Pharmaceutical Scientist).

- **PS:**
  - Revenge against the Doctor.
  - Protect herself from manipulation.

### Accusation Mechanics

Players’ actions affect the trust scores of characters. Accusations are based on evidence and trust metrics.

### Trust Meter

- **Scale:** 1-100
- **Initial Trust Levels:**
  - **Mansion Owner:** ~80
  - **Wife:** 50
  - **Journalist:** 0
  - **PS:** 50

## Art & UI

### Art Style

- **2D, Flat Art**
- **Perspective:** Top-down

### Assets Status

| Asset                                | Status      |
|--------------------------------------|-------------|
| Walking Sprites (Left, Right, Up, Down) | COMPLETED   |
| Death Sprites (x3)                   | COMPLETED   |
| Floor 1 Environment                  | COMPLETED   |
| Floor 2 Environment                  | In Progress |
| Title Screen Art                     | In Progress |
| Defeat/Loss Screen Art               | In Progress |
| Inventory Menu UI                    | To Do       |
| Dialogue Box UI                      | To Do       |
| Character Portraits                  | COMPLETED   |
| Interactable Objects/Clues           | To Do       |
| Additional Character Sprites (x2)    | Dropped     |
| Floor 3 Environment                  | Dropped     |

*Additional assets may be added based on time availability.*

### UI Elements

- **Inventory Menu** (Extra if needed)
- **Dialogue Box** (Extra if needed)

## Audio

### Sound Effects

| SFX                         | Status      |
|-----------------------------|-------------|
| Selecting Choice            | COMPLETED   |
| Confirm Choice              | COMPLETED   |
| Start Game                  | COMPLETED   |
| Dialogue Sound              | COMPLETED   |
| Picking Up an Item          | In Progress |
| Steps                       | In Progress |
| Opening Door                | In Progress |
| Interacting with NPC Jingle | COMPLETED   |

### Background Music

| BGM                          | Status      |
|------------------------------|-------------|
| Title Music                  | COMPLETED   |
| Mansion Music (8 Tracks)     | COMPLETED   |
| Interrogation Music          | COMPLETED   |
| Credit Music                 | COMPLETED   |

### Background Ambience

- **Room Ambience:** COMPLETED
- **Interrogation Background:** In Progress

## Programming

### Character Management

- **CharacterBase:** Static attributes for each character.
- **CharacterInstance:** Dynamic data like inventory, relationships, position.
- **CharacterManager:** Initializes and manages all character instances.
- **CharacterDisplay:** Manages sprites, animations, and interactions.

*TODO:*
- Create specific prefabs for every character. (DONE)
- Combine interaction and dialogue systems.
- Integrate clue/evidence generation with interactions and interrogations.
- Implement Navmesh for NPC behavior.
- Flesh out inventory mechanics and item assignments.

### Inventory System

- **Item Types:** Clue
- **Attributes:** Name, Description, Sprite, Position, State
- **ItemBase:** Holds attributes and types.
- **ItemManager:** Singleton that tracks and spawns items.

*TODO:*
- Create specific prefabs for every item. (DONE)
- Assign items to characters upon death.
- Implement item despawning upon interaction.

### Scene and Environment Management

- **Scene Management:** Unity’s built-in SceneManager with custom transition classes.
- **Camera:** Orthographic camera with Cinemachine.
- **Lighting:** Unity 2D Universal Render Pipeline.

*TODO:*
- Create/import tilebase assets. (Dropped)
- Integrate managers and mechanics with the GameManager. (DONE)

### Movement

- **Player Controls:** Free movement with WASD or arrow keys. Collision detection and walk/idle animations.
- **TODO:**
  - Implement 2D Navmesh or A* Pathfinding for NPCs.
  - Doorway interaction for room transitions (COMPLETED).

### Dialogue System

- **Features:**
  - Text blips (similar to Undertale, Celeste, Banjo Kazooie)
  - Dynamic textboxes with animations.
  - Dialogue options (Yes/No).
  
- **Components:**
  - **DialogueManager:** Singleton that manages UI panels, shows text, and handles dialogue choices.

*TODO:*
- Automatic dialogue box generation.
- Flesh out Dialogue Generation GUI. (DONE)

### Relationships & Interactions

- **Relationship Values:** -100 to 100
  - Positive (>50): Friendly/Strong
  - Negative (<-50): Hostile
  - Neutral (0): No significant relationship
- **Relationship Types:** Ally, Neutral, Enemy
- **Interaction Types:** Conversation, Gift, Help, Confrontation

*TODO:*
- Implement relationship decay.
- Sophisticated time-decay mechanisms.
- Define interaction effects and execution logic.

### Interrogation & Insanity Mechanics

- **Interrogation:**
  - Generates interrogation panels with relevant questions based on collected information.
  - Questions can be assertive or empathetic.
  - Logs interrogation statements for later reference.
  
- **Contradiction Detection:**
  - Allows players to identify contradictions in statements to gather clues.
  
- **Accusation:**
  - Set conditions for valid accusations.
  - Implement evidence evaluation and consequences.
  
- **Insanity Triggers:**
  - Death of close NPCs, contradictory evidence, incorrect accusations, disturbing scenes.
  
- **Insanity Effects:**
  - Screen blurring, color effects, hallucinations, distorted sounds, limited interactions, delayed responses.
  
- **Insanity Recovery:**
  - Medicines, positive interactions, AI behavior, dynamic alibis, live responses to player actions.

*TODO:*
- Implement FSM for AI behavior. (DONE)
- Develop Game State Editor and Generator GUI.
- Define and implement accusation conditions.

### Game State Management

- **States:**
  - Title Screen
  - Playing
  - Paused
  - Dialogue
  - Game Over
  
- **Features:**
  - Auto-save and Auto-load (Not Applicable)
  - Pause/Unpause linked with GameManager.
  - Dynamic Storyline Generator with randomized events, variable clue distribution, killer and ending variations.

*TODO:*
- Integrate all game states with the GameManager.
- Implement dynamic and randomized game elements.

## Current Status

### Completed

- **Art:**
  - Walking Sprites (Left, Right, Up, Down)
  - Death Sprites (x3)
  - Floor 1 Environment
  - Character Portraits
  - Complete Floor 2 Environment
  - Finalize Title and Defeat/Loss Screens
  - Develop Inventory and Dialogue UI elements
  - Create Interactable Objects and Clues

- **Audio:**
  - Selecting Choice, Confirm Choice, Start Game SFX
  - Dialogue Sound, Interacting with NPC Jingle
  - Room Ambience, Interrogation BGM
  - Title Music, Mansion Music (8 tracks), Interrogation Music, Credit Music

- **Programming:**
  - Player movement and collision
  - Doorway interaction
  - Dialogue Generation GUI
  - Create specific prefabs for every character
  - Develop specific item prefabs
  - All scenes and objects
  - 

### In Progress

- **Art:**
  - Defeat/Loss Screen Art

- **Audio:**
  - Picking Up an Item SFX
  - Steps, Opening Door SFX
  - Interrogation Background Ambience

### Dropped

- **Art:**
  - Additional Character Sprites (x2)
  - Floor 3 Environment

## Future Work

### Art & UI

### Audio

- **Sound Effects:**
  - Finalize Picking Up Items, Steps, and Door Opening SFX

### Programming

- **Character Management:**
  - Integrate interaction and dialogue systems
  - Implement Navmesh for NPC behavior
  - Flesh out inventory mechanics and item assignments

- **Inventory System:**
  - Assign items to characters upon death
  - Implement item despawning upon interaction

- **Dialogue System:**
  - Automate dialogue box generation
  - Enhance dialogue options and interactions

- **Relationships & Interactions:**
  - Implement relationship decay
  - Develop interaction effects and execution logic

- **Interrogation & Insanity Mechanics:**
  - Implement FSM for AI behavior
  - Develop Game State Editor and Generator GUI
  - Define conditions for making accusations

- **Game State Management:**
  - Integrate all game states with the GameManager
  - Implement dynamic storyline and randomized events

### Testing & Debugging

- **Ensure all managers follow Singleton pattern**
- **Test scene transitions thoroughly**
- **Monitor and fix any lingering bugs**

## Contributing

1. **Fork the Repository**
2. **Create a Feature Branch**
   ```bash
   git checkout -b feature/YourFeature

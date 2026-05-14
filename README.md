# 🐦 Flappy Bird — Unity 2D Clone

![Unity](https://img.shields.io/badge/Unity-2D-000000?style=flat&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=csharp&logoColor=white)
![Status](https://img.shields.io/badge/Status-Complete-3fb950?style=flat)
![Platform](https://img.shields.io/badge/Platform-PC-58a6ff?style=flat)
![Input](https://img.shields.io/badge/Input-New_Input_System-a78bfa?style=flat)

> A fully playable 2D Flappy Bird clone built with Unity and C# — my first game development project,
> created while preparing for a Game Jam hackathon at Birzeit University 🇵🇸

![Gameplay Screenshot](Assets/Screenshots/preview.png)

---

## 🎮 How to Play

| Action | Control |
|--------|---------|
| Flap upward | `Space` |
| Restart after game over | Click the restart button |

- Fly through the gaps between pipes to score points
- Hit a pipe or the ground → game over
- Each pipe pair you clear = **+1 score**

---

## ✨ Features

- **Custom pixel-art duck sprite** — original white pixel bird with orange beak
- **Procedural pipe spawning** — pipes appear at randomized heights every 3 seconds
- **Physics-based movement** — real gravity using Unity's `Rigidbody2D`
- **Live score counter** — updates in real-time using Unity UI Text
- **Sound effects** — game over sound on collision, score sound on passing a pipe
- **Game over screen** — with a restart button that reloads the scene
- **New Input System** — uses Unity's modern `UnityEngine.InputSystem`

---

## 🏗️ Architecture & How It Works

The game is split into **5 scripts**, each with a single responsibility:

### `BirdScript.cs` — Player Controller
Handles all bird logic: input, physics, death, and audio.

```csharp
// Space key detected via Unity's New Input System
if (Keyboard.current.spaceKey.wasPressedThisFrame && birdIsAlive)
    Myrigid.linearVelocity = Vector2.up * flashspeed;

// On any collision → bird dies, plays sound, triggers game over
private void OnCollisionEnter2D(Collision2D collision)
{
    birdIsAlive = false;
    audioSource.Play(); // plays gameOverSound from Resources/
    logic.gameOver();
}
```

**Key design choice:** Audio clips are loaded at runtime from `Assets/Resources/` using `Resources.Load<AudioClip>()`, so no manual drag-and-drop in the Inspector is needed.

---

### `PipeSpawning.cs` — Procedural Generation
Spawns a new pipe prefab every `spawnRate` seconds at a random Y position.

```csharp
void spawn()
{
    float lowestPoint  = transform.position.y - heightOffset; // -5.7
    float highestPoint = transform.position.y + heightOffset; // +5.7
    Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
}
```

The spawner acts as a **factory** — it doesn't know what the pipe does, it just creates them at timed intervals with randomized vertical positions.

---

### `PipeMoveScript.cs` — Pipe Movement & Cleanup
Moves every pipe leftward at a constant speed, then destroys it when it leaves the screen.

```csharp
void Update()
{
    transform.position += Vector3.left * speed * Time.deltaTime;
    if (transform.position.x <= deadzone) // -43 world units
        Destroy(gameObject);
}
```

Using `Time.deltaTime` makes the movement **frame-rate independent** — the game runs at the same speed on any machine.

---

### `PipeMiddle.cs` — Score Trigger
An invisible trigger collider sits in the gap between each pipe pair. When the bird (Layer 3) passes through it, the score increases.

```csharp
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.layer == 3) // Bird layer
    {
        logic.addScore(1);
        audioSource.Play(); // plays scoreSound
    }
}
```

This is the classic **invisible trigger zone** pattern used in 2D games to detect "passing through" a gap.

---

### `LogicManader.cs` — Game State Manager
Central controller for score, UI, game over, and scene reload.

```csharp
public void addScore(int scoreToAdd)
{
    playerScore += scoreToAdd;
    scoreText.text = playerScore.ToString(); // updates UI instantly
}

public void gameOver()
{
    gameOverScreen.SetActive(true); // shows the game over panel
}

public void restartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // full reload
}
```

---

## 📁 Project Structure

```
Assets/
├── Resources/
│   ├── gameOverSound.wav     # loaded at runtime by BirdScript
│   └── scoreSound.wav        # loaded at runtime by PipeMiddle
├── Scenes/
│   └── MainScene             # the only scene
├── Scripts/
│   ├── BirdScript.cs         # player input, physics, death
│   ├── PipeSpawning.cs       # procedural pipe factory
│   ├── PipeMoveScript.cs     # pipe movement + cleanup
│   ├── PipeMiddle.cs         # score trigger zone
│   └── LogicManader.cs       # score, UI, game over, restart
├── Sprites/
│   └── minecraft_pi...       # pixel duck + pipe sprites
└── Screenshots/
    └── preview.png           # used in this README
```

---

## ▶️ How to Run

1. **Clone the repo**
   ```bash
   git clone https://github.com/Holmes-99/flappy-bird-unity
   ```
2. Open **Unity Hub** → click **Add** → select the cloned folder
3. Open `Assets/Scenes/MainScene`
4. Press **Play ▶️** in the Unity Editor

> Requires **Unity 2022.3+** and the **New Input System** package (already in `Packages/`)

---

## 🧠 What I Learned

- Setting up a **2D Unity project** from scratch (physics, colliders, prefabs)
- Using Unity's **New Input System** for keyboard detection
- **Procedural spawning** with randomized parameters
- **Frame-rate independent movement** with `Time.deltaTime`
- Loading audio assets at **runtime** with `Resources.Load`
- Managing **game state** across multiple scripts via a central manager
- **Scene management** for restarting the game

---

## 👩‍💻 About Me

**Shatha Abualrob** — 3rd year Computer Engineering student at Birzeit University 🇵🇸  
Currently learning Unity & C# | Preparing for Game Jam hackathon

[![GitHub](https://img.shields.io/badge/GitHub-Holmes--99-181717?style=flat&logo=github)](https://github.com/Holmes-99)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Shatha_Abualrob-0A66C2?style=flat&logo=linkedin)](https://linkedin.com/in/shatha-abualrub-632a05331)

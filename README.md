<div align="center">

# 🐦 Flappy Bird — Unity 2D Clone

[![Unity](https://img.shields.io/badge/Unity-2D-000000?style=flat&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Status](https://img.shields.io/badge/Status-Complete-3fb950?style=flat)](#)
[![Platform](https://img.shields.io/badge/Platform-PC-58a6ff?style=flat)](#)
[![Input](https://img.shields.io/badge/Input_System-New-a78bfa?style=flat)](#)

> A fully playable 2D Flappy Bird clone - my first Unity project.

---
[![Download](https://img.shields.io/badge/Download-EXE-ff6b6b?style=flat&logo=windows&logoColor=white)](https://github.com/Holmes-99/flappy-bird-unity/releases/latest)
![Gameplay](Assets/Screenshots/gameplay.gif)

</div>

---

## 📖 Table of Contents

- [How to Play](#-how-to-play)
- [Features](#-features)
- [Architecture](#-architecture--how-it-works)
- [Project Structure](#-project-structure)
- [How to Run](#%EF%B8%8F-how-to-run)
- [What I Learned](#-what-i-learned)
- [About Me](#-about-me)

---

## 🎮 How to Play

| Action | Control |
|--------|---------|
| Flap upward | `Space` |
| Restart | Click restart button on game over screen |

- Fly through the gaps between the green pipes
- Hit a pipe or the ground → **game over**
- Each pipe pair cleared = **+1 score**

---

## ✨ Features

- 🐤 Custom pixel-art duck sprite
- 🌿 Procedural pipe spawning at randomized heights
- ⚙️ Physics-based gravity using `Rigidbody2D`
- 🔢 Live score counter via Unity UI
- 🔊 Sound effects — score sound + game over sound
- 💀 Game over screen with restart button
- ⌨️ Unity **New Input System** for keyboard input

---

## 🏗️ Architecture & How It Works

The game uses **5 scripts**, each with one job:

<details>
<summary><b>🐦 BirdScript.cs — Player Controller</b></summary>

<br>

Handles input, physics, death detection, and audio.

```csharp
// Space key via Unity's New Input System
if (Keyboard.current.spaceKey.wasPressedThisFrame && birdIsAlive)
    Myrigid.linearVelocity = Vector2.up * flashspeed;

// On collision → bird dies, sound plays, game over triggers
private void OnCollisionEnter2D(Collision2D collision)
{
    birdIsAlive = false;
    audioSource.Play(); // gameOverSound loaded from Resources/
    logic.gameOver();
}
```

**Design note:** Audio is loaded at runtime via `Resources.Load<AudioClip>()` — no manual Inspector drag-and-drop needed.

</details>

<details>
<summary><b>🌿 PipeSpawning.cs — Procedural Generation</b></summary>

<br>

Spawns a pipe prefab every `spawnRate` seconds at a random Y position.

```csharp
void spawn()
{
    float lowestPoint  = transform.position.y - heightOffset;
    float highestPoint = transform.position.y + heightOffset;
    Instantiate(Pipe,
        new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0),
        transform.rotation);
}
```

Acts as a **factory** — creates pipes at timed intervals with randomized vertical positions, keeping every run unique.

</details>

<details>
<summary><b>➡️ PipeMoveScript.cs — Movement & Cleanup</b></summary>

<br>

Moves every pipe leftward at constant speed, destroys it when off-screen.

```csharp
void Update()
{
    transform.position += Vector3.left * speed * Time.deltaTime;
    if (transform.position.x <= deadzone) // -43 world units
        Destroy(gameObject);
}
```

`Time.deltaTime` makes movement **frame-rate independent** — runs identically on any machine.

</details>

<details>
<summary><b>🎯 PipeMiddle.cs — Score Trigger</b></summary>

<br>

An invisible trigger collider sits in the gap between each pipe pair. When the bird passes through it, score increases.

```csharp
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.layer == 3) // Bird layer
    {
        logic.addScore(1);
        audioSource.Play(); // scoreSound
    }
}
```

Classic **invisible trigger zone** pattern — detects "passing through" without any visual element.

</details>

<details>
<summary><b>🧠 LogicManader.cs — Game State Manager</b></summary>

<br>

Central controller for score, UI updates, game over, and restart.

```csharp
public void addScore(int scoreToAdd)
{
    playerScore += scoreToAdd;
    scoreText.text = playerScore.ToString(); // live UI update
}

public void gameOver()
{
    gameOverScreen.SetActive(true); // shows game over panel
}

public void restartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // full reload
}
```

</details>

---

## 📁 Project Structure

```
Assets/
├── Resources/
│   ├── gameOverSound.wav     ← loaded at runtime by BirdScript
│   └── scoreSound.wav        ← loaded at runtime by PipeMiddle
├── Scenes/
│   └── MainScene
├── Scripts/
│   ├── BirdScript.cs         ← player input, physics, death
│   ├── PipeSpawning.cs       ← procedural pipe factory
│   ├── PipeMoveScript.cs     ← pipe movement + cleanup
│   ├── PipeMiddle.cs         ← score trigger zone
│   └── LogicManader.cs       ← score, UI, game over, restart
├── Sprites/
│   └── minecraft_pi...       ← pixel chicken(minecraft theme) + pipe sprites
└── Screenshots/
    └── gameplay.gif          ← shown at top of this README
```

---

## ▶️ How to Run

```bash
git clone https://github.com/Holmes-99/flappy-bird-unity
```

1. Open **Unity Hub** → **Add** → select the cloned folder
2. Open `Assets/Scenes/MainScene`
3. Press **▶ Play**

> Requires **Unity 2022.3+** — the New Input System package is already included in `Packages/`

---

## 🧠 What I Learned

<details>
<summary><b>Click to expand</b></summary>

<br>

- Setting up a **2D Unity project** from scratch — physics, colliders, prefabs
- Using Unity's **New Input System** for keyboard input
- **Procedural spawning** with randomized parameters
- **Frame-rate independent movement** using `Time.deltaTime`
- Loading audio at **runtime** with `Resources.Load`
- The **invisible trigger zone** pattern for detecting gap passes
- Managing **game state** across scripts via a central manager
- **Scene reloading** for clean game restarts

</details>

---

## 👩‍💻 About Me

<div align="center">

**Shatha Abualrob** — 3rd year Computer Engineering @ Birzeit University 🇵🇸

*Learning Unity & C# | Preparing for Game Jam hackathon*

[![GitHub](https://img.shields.io/badge/GitHub-Holmes--99-181717?style=flat&logo=github&logoColor=white)](https://github.com/Holmes-99)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-Shatha_Abualrob-0A66C2?style=flat&logo=linkedin&logoColor=white)](https://linkedin.com/in/shatha-abualrub-632a05331)

</div>

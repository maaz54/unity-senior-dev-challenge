# Task 1 — Generic Object Pooling Manager

# Overview

-This demo implements a generic object pooling system in Unity to efficiently manage a large number of objects without causing any runtime garbage collection.

# How To Run

- You’ll find the demo scene for this task here: Assets\Solutions\Task1_ObjectPooling\Scenes\DemoScene.unity
- Hit Play and click the Spawn button in the UI.
- It Spawns 1000 cube objects in a random circular pattern. Holds them in the scene for 2 seconds, then releases them back to the pool.
- Everything runs through a generic object pooling system, so there will be zero GC allocation at runtime.
- You can easily change the spawn quantity and hold time from the Inspector by adjusting the ObjectPoolController component.


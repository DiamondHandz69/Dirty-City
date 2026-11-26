# DIRTY CITY — Unreal Engine 5 Premium Game

This folder is the starting point for a standalone Unreal Engine 5 project. The browser demo lives in `../prototypes/night-drive-3d.html`.

## Vision
A third-person open-world urban action game with cinematic lighting, driving, exploration, missions, and nine original characters. This is a production plan and initial source scaffolding, **not** a finished AAA game.

## Getting started
1. Install Unreal Engine 5 using Epic Games Launcher on a Windows PC with a suitable GPU.
2. Create a new **Games → Third Person → Blueprint** project named `DirtyCity` with Starter Content enabled.
3. Configure the project for desktop/high quality and enable Lumen where supported.
4. Create a level `L_NeonDistrict` with a road grid, sidewalks, modular buildings, streetlights, and mission pickup points.
5. Create a Blueprint actor `BP_MissionPickup` with a Sphere Collision and a Niagara glow effect. On player overlap, broadcast a mission objective completion event.
6. Add a `BP_DirtyCityGameMode` that tracks pickups, objective state, and character selection.
7. Add a drivable vehicle using the Chaos Vehicles plugin and a player-controlled third-person character.
8. Import original, appropriately licensed character meshes and animations; preserve the existing DIRTY CITY designs.
9. Profile frame time, draw calls, texture memory, and streaming before expanding the map.

## Core cast
DIRTY — The Boss (Colombia)
LA JEFA — The Driver (Mexico)
DREAD — The Ghost (Jamaica)
NADYA “BLACK ICE” VOLKOVA — The Spy (Russia)
SHEIKH — The Hacker (Middle East)
CONNECT — The Dealmaker (China)
TANK — The Hitman
JOBBER — The Fixer (Nigeria)

**Roster note:** The existing homepage also includes OUTLAW, so the definitive ninth slot needs to be reconciled against the approved reference art before generating character assets. Do not overwrite original artwork.

## First vertical slice acceptance criteria
- 1 small streaming city district with night lighting
- 1 fully rigged player character and third-person camera
- 1 drivable vehicle with collision
- 3 objectives with HUD markers and mission completion
- Keyboard/gamepad controls, tested performance
- Licensed or original assets and clear credits

## Architecture
- `BP_DirtyCityGameMode`: objective state and progression
- `BP_DirtyCityCharacter`: locomotion and interactions
- `BP_DirtyCityVehicle`: Chaos vehicle input and camera
- `BP_MissionPickup`: proximity and completion events
- `WBP_HUD`: objective, map and interaction prompts
- `DA_CrewMember`: data assets for roles, bios, portrait references and eventual character mesh assets

## Constraints
Unreal Engine desktop games cannot simply be embedded into a normal static GitHub Pages website. A separate browser game or streamed game would be needed. Unreal, Unity and PlayCanvas are distinct runtimes; asset sharing is possible, but gameplay code generally must be ported.

## Milestones
1. Browser driving prototype — initial version committed.
2. Unreal vertical slice — project setup and playable packaged build.
3. Character art/rigging — approved reference-based assets.
4. Expanded driving and mission systems.
5. Performance optimization, QA and distribution.

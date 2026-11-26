# DIRTY CITY — Native Mobile / Premium Coastal Slice

This is the Unity 6 source starter for a separate native game, not an installable build or a completed Unity project. The existing website remains the playable browser prototype. The goal is an original cinematic coastal crime-fiction game with console-inspired art direction, not a replica of GTA maps, characters, logos or audio.

## First playable target
- One 250–400 m coastal street district with modular hotels, alleys, palm trees, waterfront and interiors only where useful.
- One approved crew character with a realistic humanoid mesh, PBR materials, facial detail, rig, locomotion blend tree and jump animation.
- One licensed/original detailed car with wheel physics, enter/exit and chase camera.
- One timed delivery mission, map guidance, pedestrian and traffic loops.
- Landscape touch controls: floating left movement stick, right drag camera, sprint, jump and context action. UI must fit the safe area and support simultaneous touches.
- Console-inspired sunset lighting, night lighting, reflection probes, baked indirect illumination, shadows, terrain and surface detail. No primitive meshes in the final art-quality review.

## Import the source starter
1. Create a Unity 6 Universal 3D (URP) project using Unity Hub with iOS/Android build modules as required.
2. Copy the Assets/DirtyCity folder from this repository into that project. Set Active Input Handling to Input Manager (Old) or Both for the starter script. Set orientation to landscape.
3. Import an approved rigged humanoid and animations. Attach a CharacterController and CoastalPlayer to the player root; set capsule dimensions to fit the model. The camera and Animator are assignable in the inspector. Configure Animator float Speed and bool Grounded, turn off Apply Root Motion, and create a locomotion blend tree in meters/sec. Configure cameraObstacles to include solid city geometry but exclude the player.
4. Add a scene camera, lights, proper colliders and an EventSystem. Create safe-area UI action buttons. Wire sprint hold/release to SetSprint(true/false) and jump click to RequestJump. Movement and look use separate fingers on the non-UI screen. Keyboard WASD, Shift, Space and right-drag camera support editor testing.
5. Add the actual district meshes, road materials, rigged animation, vehicle and mission systems before packaging. These assets/systems are not supplied by this starter.

## Performance tiers
Mobile baseline: 30 fps target on tested devices, use URP, baked GI, limited real-time shadows, material atlases, LODs, occlusion and texture compression. Target a playable device-specific quality tier, not console parity on older phones.
High-end mobile: test optional 60 fps, better shadow distance, denser props and higher render scale; do not advertise before profiling.
PC/console-quality development can use a separate rendering profile and higher quality assets. Shipping on PlayStation requires the relevant platform approval and tools.

## Asset rules
Preserve the original crew art as the character design authority; do not replace characters with generic models. Start with La Jefa, then obtain user-approved models for the full roster. Use owned, original or properly licensed models, animations, sound and music; maintain attribution. The website's nine-slot lineup remains untouched.

## Release gates
A real device build is mandatory. Verify touch input, camera collisions, character motion, vehicle handling, stable frame pacing, thermal behavior, and memory use on target hardware. iOS export needs a compatible Xcode/macOS signing workflow; Android needs the corresponding SDK and signing configuration. No APK/IPA or store release has been produced.

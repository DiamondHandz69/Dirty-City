# Native vertical slice integration checklist (source only)
Use Unity 6 URP, landscape orientation. Import scripts into a real project. No native scene or signed binary exists yet.
- CoastalPlayer: existing walking, camera, animation parameter controller. Assign a properly rigged original crew mesh and compatible clips.
- CoastalVehicle: Rigidbody plus chassis colliders, four WheelColliders, visual wheels and center-of-mass transform; tune suspension, mass, friction and torque on test track.
- VehicleInteraction: assign camera, capsule player, separate visual root and UI panels. Vehicle layers must select vehicles; solid layers must exclude the player, but include vehicle chassis, walls and obstacles. Exit point must be beside the car at player root height. Character follows the seat while driving; connect mobile buttons to Gas/Steer/Brake. Use MobileHoldButton heldValue -1 or 1 for direction, 1 for gas/brake. Wire the context button to Interact.
- TrafficRoute: kinematic traffic only; configure obstacle layers to exclude own collider and ground (use a separate detection arrangement/layer as needed); waypoint loops, safe spawn spacing and intersection yielding require scene testing. Do not use with CoastalVehicle on the same object.
- DeliveryMission: three pickup transforms, destination, tracked player, UI events and timer. Pickups and destination must not be parents of the mission component.
- SafeAreaPanel: stretch a direct child of a full-screen Canvas; connect controls and verify actual devices.
## Visual asset acceptance list
1. One approved rigged La Jefa model with authentic proportions, clothing, hair, PBR textures and retargeted idle/walk/run/jump clips. Then the rest of the existing approved crew. No generic replacement of their designs.
2. Detailed sports sedan with separate wheels, licensed mesh, interior and collisions. Realistic traffic variations.
3. Modular coastal streets: layered asphalt, sidewalks, building facades, palm foliage, signs, shopfronts, waterfront and props. Use LODs and texture atlases.
4. URP lighting: baked indirect light, reflection probes, directional sun, limited realtime lights, shadow quality profiles and restrained bloom/color grading.
5. Audio: original/licensed engine sounds, footsteps, ambience, radio.
## Status
Code committed, not compiled or verified in Unity. All artwork, animations, scene assembly, physics tuning, visual quality checks and iOS/Android builds remain outstanding. Existing web game stays playable. Do not describe this source starter as a PS5-quality release.

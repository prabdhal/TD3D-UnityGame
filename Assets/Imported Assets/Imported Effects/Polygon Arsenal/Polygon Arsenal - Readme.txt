----------------------------------------
POLYGON ARSENAL
----------------------------------------

1. Introduction
2. Spawning effects
3. Scaling effects
4. Upgrading to LWRP
5. FAQ / Problemsolving
6. Asset Extras
7. Contact
8. Credits

----------------------------------------
1. INTRODUCTION
----------------------------------------

Thank you for buying! Please consider rating and reviewing the asset after you've tried it out!

Effects can be found in the 'Polygon Arsenal/Prefabs' folder. Here they are sorted in 3 main categories: Combat, Environment and Interactive.

----------------------------------------
2. SPAWNING EFFECTS
----------------------------------------

In some cases you can just drag&drop the effect into the scene, otherwise you can spawn them via scripting.

Small example on spawning an explosion via script:

public Vector3 effectNormal;

spawnEffect = Instantiate(spawnEffect, transform.position, Quaternion.FromToRotation(Vector3.up, effectNormal)) as GameObject;

----------------------------------------
3. SCALING
----------------------------------------

To scale an effect in the scene, you can simply use the default Scaling tool (Hotkey 'R'). You can also select the effect and set the Scale in the Hierarchy.

Please remember that some parts of the effects such as Point Lights, Trail Renderers and Audio Sources may have to be manually adjusted afterwards.

----------------------------------------
4. Upgrading to LWRP
----------------------------------------

Open and extract the bundled 'PolygonArsenal LWRP Upgrade' unitypackage to your project. This should overwrite the old Standard Shaders, custom shaders and Materials.

If you still have pink materials/effects, try this:

1. Make sure you have LWRP and Shader Graph installed in your project via the Package Manager. (Window/Package Manager)

2. Go to 'Edit/Project Settings/Graphics' and select a Scriptable Render Pipeline Setting from 'Polygon Arsenal/Demo/LWRP Settings'

3. Sometimes the custom shaders may be stuck looking pink, you can fix this by opening each custom shader and press 'Save Asset' in the top left corner of the Shader Graph.

----------------------------------------
5. FAQ / Problemsolving
----------------------------------------

Q: I want to upgrade the asset to the LWRP (Lightweight Render Pipeline)

Go to 'Edit/Project Settings/Graphics' and select a Scriptable Render Pipeline Setting from 'Polygon Arsenal/Demo/LWRP Settings'

--------------------

Q: Particles appear stretched or too thin after scaling
 
A: This means that one of the effects are using a Stretched Billboard render type. Select the prefab and locate the Renderer tab at the bottom of the Particle System. If you scaled the effect up to be twice as big, you'll also need to multiply the current Length Scale by two.

--------------------

Q: The effects look grey or darker than they're supposed to

A: https://forum.unity.com/threads/epic-toon-fx.390693/#post-3279824

--------------------

Q: Annoying "Invalid AABB aabb" errors

A: This seems to be an error that comes and goes in between some versions, possible fix: https://forum.unity.com/threads/epic-toon-fx.390693/#post-3542039

--------------------

Q: I can't find what I'm looking for

A: There are a lot of effects in this pack, I suggest searching the Project folder or send an e-mail if you need a hand.

--------------------

Q: Can you add X effect to this asset?

A: Maybe! Add sufficient details to your request, and I will consider including it for the next update. Please note that it can take weeks or months in between updates.

----------------------------------------
6. ASSET EXTRAS
----------------------------------------

In the 'Polygon Arsenal/Scripts' folder you can find some neat scripts that may further help you customize the effects.

PolygonLightFade - This lets you fade out lights which are useful for explosions

PolygonPitchRandomizer - Randomizes the pitch of sounds, and can help make repetetive sounds less annoying.

PolygonRotation - A simple script that applies constant rotation to an object

PolygonSoundSpawn - A handy script for playing sounds which destroys itsself after the sound is over

----------------------------------------
7. CONTACT
----------------------------------------

Need help with anything? 

E-Mail : archanor.work@gmail.com
Website: archanor.com

Follow me on Twitter for regular updates and news

Twitter: @Archanor

----------------------------------------
8. CREDITS
----------------------------------------

Special thanks to:

Jan Jørgensen

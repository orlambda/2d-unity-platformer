- Priority: kinematic
    - Detect collision
    - Correct for collision
    - Add gravity
    - Detect if standing / can jump (if collision when moving down for gravity)
        - Note: do I need to translate player more than once now?
    - Change jump be single impulse followed by falling
        - Add bool jumping: true for jumpTime?
        - What stops jumping?
            - Time limit
            - Any collision, or specific collision?
            - Height reached?

- Basic platformer room
    - Check z transorm / layer convention for sprites
    - Make sideways movement smooth
    - Fix collision at high speeds
    - Make the portal entrance trigger visible only in the editor
    - Add line between entrance and exit in editor when portal is selected (gizmos?)
    - Add looping background music
    - Add a portal sound

- Sams Teach Yourself Unity in 24 Hours
    - Hour 4

- Platformer room - features to add
    - Collectibles (disappear and play a sound, no score increment etc)
    - Dash/run
    - 2-way portal with cooldown / switching / checking for player to exit the trigger / larger trigger to detect player leaving
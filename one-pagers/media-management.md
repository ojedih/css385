# One Pager - Media Management

### Common uses of media items:

- Sprites/animations: Characters, bullets, spike defuse progress, textures for tilemaps.
- Sound effects: Gunshots, footsteps, ticking/defuse beeps/cues.
- UI assets: Timers, health bars, minimaps.
- Music: tracks to build tension, victory/defeat stings.

### Strategies:

- Middleware (Mirror Networking): I'm actually using this in my project to handle the multiplayer. It's a 3rd party tool that makes it easier to create multiplayer games.
  - Pros: Straightforward to integrate, saves a lot of time since I don't have to program a network system myself.
  - Cons: Learning curve, extra tools, adds complexity.

- Native engine integration (Unity Engine): Direct sprite/UI/audio playback via scripts.
    - Pros: Simple, accessible, performs well in 2D.
    - Cons: Less adaptive, manual syncing.

### Approach

I'd rather go with a native engine integration "plug and play" approach because in these kind of agile extreme programming development methodologies the less the better. The more the game engine has to offer that is built-in, the easier is going to be managing the project as it grows in complexity. I took a risk by deciding to make it multiplayer, which forced me to use a 3rd party tool, but sometimes there is no choice.
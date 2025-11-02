# Performance Considerations
by Alejandro Ojeda

### 3 sources of performance concerns
1. Too many objects in scene (inefficient code), RAM or CPU gets clogged up, resulting in slow FPS.
2. Server sync problems cause users to see things other users don't see
3. High latency for players located scattered around the world

### Solution strategies
1. A common example is loading objects/textures that the user isn't really seeing on screen. The straight forward solution is to verify the code trying to spot inefficiencies within. I'm not too concerned for my game because it's 2D and nowadays machines are very powerful, but still worth keeping a clean code.
2. Again, issues with the code or how the server is design can lead to this problem. To tackle this, I will ensure the server uses a server-authoritative model where the server is actually in control of the whole chain of events and change of states. Users would only send inputs to the server.
3. This doesn't really apply to my game because I don't plan on hosting it on the cloud, but a common way of solving this issue is by deploying the game to different servers around the world. This way, the users only connect to the ones closest to them to get the best latency.

### How will I use these strategies
I will use these strategies to ensure my game runs efficiently and delivers a smooth experience. By keeping my code optimized and avoiding unnecessary object or texture loading, I’ll minimize CPU and RAM strain even as the project grows. To maintain consistent gameplay between players, I’ll implement a server-authoritative model, making sure the server manages all critical events and player states to prevent desync issues. For now, I only plan on hosting it on a local network, so I'm not worried about latency problems.
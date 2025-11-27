# Checkpoint 3 - Game Testing
by Alejandro Ojeda

### 1. Game Testing Questions
1. What’s the most common bug you find in early builds?
2. How do you prioritize which bugs to report first?
3. How do you test a game, do you follow a process?
4. How do you test for fun factor?

### 2. Testers Feedback
1. Multiplayer feels smooth but the lobby matchmaking could be much refined to make for a more seamless user experience.
2. The map needs more work to make the gameplay more engaging. For example, add more cover walls, shrink the map progressively like in Fortnite, etc.
3. Defusing feels a little glitchy when someone stops defusing and a player from a different team takes over.

### 3. Summarize findings and identify common patterns

I found that the defusing mechanic was shared from the players to the server and back to the players, making the progress not reset properly when a new player is defusing. Lobby flow needs improvement indeed. For the map I found an error with the way the tilemap is created and how the texture slicing is done.

### 4. AI summary comparison - using Grok 4.1

"""

My testing aligns perfectly with player feedback on all three points.  
- Defuse glitches: Confirmed root cause is client-server sync; progress isn't resetting on player switch, exactly as testers reported.  
- Matchmaking/lobby: Players called it clunky, and I saw the same—queue times and flow need real work.  
- Map issues match too: testers want more cover and dynamic shrinking; I found the underlying tilemap/texture slicing bugs that make the map feel empty and broken.  

Everything testers felt as "annoying" has a clear technical reason. No surprises, just validation.

"""
# Managing Connections with LobbyScene

This method for connections allows users to choose their own room name rather
than being put into the same game. It's a bit clunkier to navigate though,
especially on Android, and isn't ideal for debugging:

- Add the ASL LobbyScene to your project

- When you're finished editing your scene, right click and unload it (So UI doesn't overlay)

- Go to ASL_LobbyScene -> LobbyManager -> Hostmenupanel -> AvailableScenes. Delete the other scenes in the dropdown and put the name of your scene there

- Go to build settings, make sure the Lobbyscene and Sceneloader are checked. Press "add open scenes" to add your current scene to the build. Delete all the others if you'd like.

- Make LobbyScene the first scene in your build order

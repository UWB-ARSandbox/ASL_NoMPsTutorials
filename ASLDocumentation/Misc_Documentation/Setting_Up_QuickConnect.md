#  Managing Connections with QuickConnect

This will put users into a predefined room, which means they could wind up
with random players if your app is widely distributed.

- Create an Empty GameObject. Add the QuickConnect Script to it, and move it
to the top of your scene hierarchy so it'll load first.

- Configure the script so that it will put your users into a room with a unique
name (don't use a variant of 'test'), and enter the case-sensitive name of
your scene

- Go to build settings, make sure the Lobbyscene and Sceneloader are checked.
Press "add open scenes" to add your current scene to the build. Delete all
the others if you'd like.

- Move your scene to the top of the build settings hierarchy

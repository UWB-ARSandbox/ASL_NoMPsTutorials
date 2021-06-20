#  Setting Up A Project Using the Unity 2020.3 Branch

- Install Unity 2020.3.0f1

- Add Android Build Support, Universal Windows Platform Build Support,
and Windows Build Support (IL2CPP) modules

- Download the 2020.3 Branch of ASL
From: https://github.com/UWB-ARSandbox/ASL_NoMPsTutorials/tree/Unity-2020.3

- Extract all

- Take the ASL directory out and put it in whichever folder you like to put
your Unity projects

- Navigate to that ASL file in the Unity editor and open it

- Ignore the Unity Safe Mode Prompt

- Navigate to Library/PackageCache/com.google.ar.core.arfoundation.extensions@...../Editor/Scripts/Internal/Analytics

- Delete Google.Protobuf.dll and the corresponding meta file

- (optional - clears error) go to         
Library/PackageCache/com.google.ar.core.arfoundation.extensions@...../Editor/ExternalDependencyManager/Editor and delete the Google.IOSResolver dll

- The !name.empty() assertion failure will go away after you run the scene for
the first time, ignore it!

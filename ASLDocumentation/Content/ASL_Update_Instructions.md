## Updating the Augmented Space Library

This document was created on 3/30/21 by Matthew Munson. I recently updated ASL
from Unity version 2020.1.0b12 to 2020.3.0f1. This document is a record of
everything that needed to be changed in order to get the library updated.

### Setting up the Unity Install:

- When installing Unity, make sure that your installation includes Android
Build support, Universal Windows Build Platform support, and Windows Build
Support (IL2CPP) modules.

- Make sure to change the Unity version in the projects list prior to
opening!

### Updating the Windows Mixed Reality Toolkit

- The MRTK toolkit broke when updating to 2020.3.0f1, so it's possible that
it will cause issues again in the future.

- Updating MRTK is relatively simple. Fetch the newest build from:
https://github.com/Microsoft/MixedRealityToolkit-Unity/releases. Within the
downloaded folder, navigate to Assets. The MRTK folder is what should be
put into ASL.

- Simply delete the MRTK folder in ASL/Assets/ASL and replace it with the
new version.

- Additional MRTK settings can be found in Project Settings -> Mixed Reality
Toolkit. Updating to 2020.3.0f1 did not require changing these settings.

### Updating Google XR and ARCore

- Problems in ARCore may not immediately cause compiler errors when running
ASL, even in Android build mode. When updating to 2020.3.0f1, errors only
occured when an AR camera was activated, specifically a NullReference on
the ARSessionExtentions.SessionHandle.

- When updating to future versions, be sure to run an AR demo to ensure that
all functionality still exists.

- Because future errors may not be exactly the same as what was encountered
here, consider following this tutorial:
https://developers.google.com/ar/develop/unity-arf/quickstart-android
which is intended to walk developers through Google XR setup.

- To update to 2020.3.0f1 the XR .tgz file was downloaded from:
https://github.com/google-ar/arcore-unity-extensions/releases/

- In Unity, go to Window -> Package Manager. Select ARCore Extensions.
Click 'Remove' in the button right corner

- Add the new ARCore version by pressing the plus button in the upper left
corner. Add package from tarball and select the .tgz file

- Once the extension is loaded, the duplicate Google protobuf file will be back.
Delete it from Library/PackageCache/com.google.ar.core.arfoundation.extensions@...../Editor/Scripts/Internal/Analytic

- In 2020.3, an IOS specific error caused warnings to appear on console. This
was resolved by deleting Google.IOSResolver.dll from
Library/PackageCache/com.google.ar.core.arfoundation.extensions@...../Editor/ExternalDependencyManager/Editor

- Go to Project Settings -> XR Plug-in Management -> Android tab and make sure
ARCore is checked

### Updates through the Package Manager

- This one is pretty self-explanatory! Use the package manager to update old packages. Be aware that Microsoft
doesn't always have its packages up to date, which is why TextMeshPro is currently a few versions behind.

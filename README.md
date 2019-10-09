This is version 1 of the upgraded Augmented Space Library (ASL). It is being built with Unity 2019.2.7f and GameSparks (https://www.gamesparks.com/). 

Documentation for this framework can be found here: https://uwb-arsandbox.github.io/ASL_Master/ASLDocumentation/Help/index.html The first page of website is shown below.

<html>
	<head></head>
	<body>
		<h1>Augmented Space Library Documentation</h1>
		<p>Welcome to the Augmented Space Library (ASL) Documentation System. Here you will find documentation on how ASL works so you can futher improve its functionaility, as well as find documentation on demos within ASL to better understand how they work and how you can use ASL for your own multiplayer projects. </p>
			<h2>Getting Started</h2>
			<p>The steps to install and setup ASL are as follows:
				<ul>				
					<li>
						<p><strong>Download ASL</strong> - Download this project and open it in Unity 2019.2.8f1 (other verisions should work but are not guaranteed). If you have packages errors, then make sure you are using Unity 2019.2.8f1 or higher. If that still does not resolve your package issues, then  continue into Unity and then go to Help->Reset Packages to defaults. This should resolve any package errors.</p>
					</li>
					<li>
						<p><strong>Setup GameSparks</strong> - Locate "GameSparksSettings" in GameSparks/Resources. Then enter and save the following information.</p>
					</li>					
						<ul>
							<li>
								<p><strong>API Key</strong> - Ask Kelvin Sung</p>
							</li>
							<li>
								<p><strong>API Secret</strong> - Ask Kelvin Sung</p>
							</li>
							<li>
								<p><strong>Credential</strong> - device</p>
							</li>
							<li>
								<p><strong>Preview Build</strong> - true</p>
							</li>
							<li>
								<p><strong>Debug Build</strong> - (Optional) true</p>
							</li>
						</ul>
					<li>
						<p><strong>Connect to other users</strong> - First and very importantly, you must always build the scenes: <strong>LobbyScene</strong> and <strong>SceneLoader</strong>. You will not be able to connect to others and will recieve errors if you do not have these scenes in your Unity build settings. Now, with that out of the way, there are two ways in which you can connect players to each other for either testing your application or using it. The first is through the use of the provided LobbyScene. If you use this method, you must ensure you always launch from this scene and include the name of the scene you want to launch after players ready up via the "Starting Scene" field in the Unity Editor under the LobbyManager Script found in the Lobby Manager GameObject. The second method allows you to connect from whatever scene you want. To use this method, you must attach the QuickConnect script to an empty GameObject and place that empty GameObject in the Unity Hierarchy window of your desired scene at the very top so it execute first. You must also pick a unique room name and the starting scene for players to transition to after connecting and readying up with each other. If your room name is not unique, then you will join another ASL user's room. The starting scene can be the name of the scene you are currently working in. However, QuickConnect should only exist in one, and only one scene in your project if you use it at all. It is adiviced to use the QuickConnect option as it is easier to test with. If you do use this method, you must still build the LobbyManager and SceneLoader scene, though they no longer have to be built first.</p>
					</li>
					<li>
						<p><strong>Using ASL</strong> - You are now ready to begin programming a multi-user interactive experience! Refer to the documention if you are having trouble with any function, but in general, make sure to claim your object before doing anything with it and even then, the stuff you do with it should be ASL related only if you want others to see your changes.</p>
					</li>
				</ul>
			</p>				
			<h2>Adding documentation</h2>
			<p>Note, the steps to add to this documentation file are as follows, but only work with Visual Studio 2017 as of October 3, 2019. Currently there is no way to generate documentation with Visual Studio 2019 using this method.
				<ul>
					<li>
						<p><strong>Install Sandcastle</strong> -  Go here https://randynghiem.wordpress.com/2015/06/18/how-to-set-up-sandcastle-help-file-builder-with-visual-studio-2015/ and follow the steps provided. When you are actually installing SandCastle, install everything it desires.</p>
					</li>
					<li>
						<p><strong>Check the XML Documentation File Box</strong> - Right click in Visual Studios on "Assembly-CSharp" and go to properties. If nothing shows up then you need to allow Unity properties to be visible in Visual Studios. To do this, follow these directions: https://answers.unity.com/questions/1140063/cant-open-project-property-in-visual-studio-2015-w.html Once in the properties, go to build, and tick the "XML documentation file:" check box. An image of what it looks like can be found in the first link. WARNING: Rather annoying, everytime you interact with, run, or even focus on Unity, this file gets rewritten and this checkbox becomes unchecked. If you build SandCastle when this box is unchecked, no errors will appear, instead your documentation file will be blank.</p>
					</li>
					<li>
						<p><strong>Add Sandcastle to Visual Studio</strong> - Now that you have SandCastle installed and Visual Studio project properties set up, you need to add SandCaslte to your project. If the current documentation file (where you got this information from) is no longer available, then you'll need to add a new SandCastle project. To do this, right click your Solution in Visual Studios and Add New Project. Then select Documentation, Sandcastle Help File Builder Project. After adding this, go to the "Documentation Sources" folder and add "Assembly-CSharp.dll" which should also automatically add "Assembly-CSharp.xml", but if it doesn't, add it yourself. These sources by default will be found in "Temp\bin\Debug\" if you were able to successfully build your "Assembly-CSharp" project from Visual Studio. After adding this documentation sources, right click and build your SandCastle Documentation Folder. After this, just go to where it was built and go into the "Help" folder to find your documentation. If this documentation still exists and just needs updating, then instead of creating a new SandCastle project, just add this one instead (ASLDocumentation.shfbproj) and then build.</p>
					</li>
				</ul>
			</p>
			<p>For any additional help in installing, using, or modifying ASL, please contact Kelvin Sung.</p>		
			<h2>Final Notes</h2>
			<p>ASL allows you to have in sync, or ASLObjects, objects in your scene before you start your scene (the objects in the hierachy window before starting) and to instantiate these objects at run time. However, in order to ensure ASLObjects starting in your scene are synced properly across users they must have a unique name. This allows ASL to find these objects later and assign them the correct ASL ID. Techically, the formula used to help generate object uniqueness is based upon a string that combines the following: The object's name + the square root of ((the object's position, rotation, scale) + (the Dot Product of the object's rotation and scale * Vector.one)). The following string ends up being the object's name plus a number that is most likely unique, but not guaranteed. To ensure proper ASL object synchronization, give your ASL object's that start in your scene unique names. ASL objects instantiated during runtime do not need to have a unique name as their ids are created in a different fashion that is  guaranteed to be unique and synchronized.</p>			
	</body>
</html>

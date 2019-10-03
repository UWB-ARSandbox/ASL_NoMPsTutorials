using GameSparks.RT;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ASL
{
    /// <summary>
    /// Provides functions pertaining to ASL to be called by the user but not linked to any specific object
    /// </summary>
    public static class ASLHelper
    {
        /// <summary>
        /// A dictionary containing all of the ASLObjects in a scene
        /// </summary>
        static public Dictionary<string, ASLObject> m_ASLObjects = new Dictionary<string, ASLObject>();

        #region Instantiation

        #region Primitive Instantiation

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity);
        /// }
        /// </code></example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation)
        {
            SendSpawnPrimitive(_type, _position, _rotation);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id); 
        /// }
        /// </code>
        /// <code>
        /// void SomeOtherFunction()
        /// {
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, ""); //Using this overload (and others) and passing in an empty string is a valid option 
        /// }
        /// </code>
        /// <code>
        /// void WoahAnotherFunction()
        /// {
        ///     //Where 'MyParentObject' is the name of the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, "MyParentObject"); 
        ///     //The parent of your object can also be found by passing in the name (e.g., gameObject.name) of the parent you want. However, this method is a lot slower than using the ASL ID method, 
        ///     //but it is the only way to assign a non-ASL Object as a parent to an ASL Object for all users
        /// }
        /// </code></example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID)
        {
            SendSpawnPrimitive(_type, _position, _rotation, _parentID);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.
        /// When you use a Unity component, things are slightly different. For example, to add a Rigidbody component, you would enter this: "UnityEngine.Rigidbody,UnityEngine"
        /// In this case, the pattern is still the namespace + component, but then it is followed with by ",UnityEngine". If you need UnityEditor, try that after the comma</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Using just the name space and the class name should be enough for _componentAssemblyQualifiedName. For more info, go here
        ///     //https://docs.microsoft.com/en-us/dotnet/api/system.type.assemblyqualifiedname?view=netframework-4.8#System_Type_AssemblyQualifiedName
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id, "MyNamespace.MyComponent"); 
        ///     
        ///     //Note: If you need to add more than 1 component to an object, use the AddComponent ASL function after this object is created via an after creation function
        ///     //To do so, you will need to use the instantiatedGameObjectClassName and InstantiatedGameObjectFunctionName parameters as well. 
        ///     //See those instantiation overload options for more details
        /// }
        /// </code>
        ///</example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName)
        {
            SendSpawnPrimitive(_type, _position, _rotation, _parentID, _componentAssemblyQualifiedName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction"); //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        /// }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        /// }
        /// 
        /// </code></example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName,
            string _instantiatedGameObjectFunctionName)
        {
            SendSpawnPrimitive(_type, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated. 
        /// This parameter is now optional. If you use this parameter you must also provide _instantiatedGameObjectFunctionName</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated.
        /// This parameter is not optional. If you use this parameter you must also provide _instantiatedGameObjectClassName </param>
        /// <param name="_claimRecoveryClassName">This is the name of the class that contains your function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryFunctionName</param>
        /// <param name="_claimRecoveryFunctionName">This is the name of the function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryClassName</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction", GetType().Namespace + "." + GetType().Name, "MyClaimRejectedFunction"); //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        ///  }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        /// }
        /// 
        /// public static void MyClaimRejectedFunction(string _id, int _cancelledCallbacks)
        /// {
        ///    Debug.LogWarning("We are going to cancel " + _cancelledCallbacks +
        ///       " callbacks generated by a claim for object: " + _id + " rather than try to recover.");
        /// }
        /// </code></example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName = "",
            string _instantiatedGameObjectFunctionName = "",
            string _claimRecoveryClassName = "",
            string _claimRecoveryFunctionName = "")
        {
            SendSpawnPrimitive(_type, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName, _claimRecoveryClassName, _claimRecoveryFunctionName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_type">The primitive type to be instantiated</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated. 
        /// This parameter is not optional to remove ambiguity. However, you can pass in an empty string if you don't need to do anything upon creation.</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated.
        /// This parameter is not optional to remove ambiguity. However, you can pass in an empty string if you don't need to do anything upon creation.</param>
        /// <param name="_claimRecoveryClassName">This is the name of the class that contains your function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryFunctionName</param>
        /// <param name="_claimRecoveryFunctionName">This is the name of the function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryClassName</param>
        /// <param name="_sendFloatClassName">This is the name of the class that contains your function that you want to be executed whenever you use the 
        /// <see cref="ASLObject.SendFloat4(float[])"/> function. This parameter is optional. If you use this parameter you must also provide _sendFloatFunctionName</param>
        /// <param name="_sendFloatFunctionName">This is the name of the function that you want to be executed whenever you use the <see cref="ASLObject.SendFloat4(float[])"/> function.
        /// This parameter is optional. If you use this parameter you must also provide _sendFloatClassName</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction", GetType().Namespace + "." + GetType().Name, "MyClaimRejectedFunction",
        ///     GetType().Namespace + "." + GetType().Name, "MySendFloatFunction"); //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        /// }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        ///     _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().SendAndSetClaim(() =>
        ///     {
        ///         float[] myFloats = new float[] { 1.1f, 2.5f, 3.4f, 4.9f };
        ///         _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().SendFloat4(myFloats);
        ///     });
        /// }
        /// 
        /// public static void MyClaimRejectedFunction(string _id, int _cancelledCallbacks)
        /// {
        ///    Debug.LogWarning("We are going to cancel " + _cancelledCallbacks +
        ///       " callbacks generated by a claim for object: " + _id + " rather than try to recover.");
        /// } 
        /// public static void MySendFloatFunction(float[] _floats)
        /// {
        ///     for (int i = 0; i&lt;_floats.Length; i++)
        ///     {
        ///         Debug.Log("Value Sent: " + _floats[i]);
        ///     }
        /// }
        /// </code></example>
        static public void InstanitateASLObject(PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName,
            string _instantiatedGameObjectFunctionName,
            string _claimRecoveryClassName = "",
            string _claimRecoveryFunctionName = "",
            string _sendFloatClassName = "",
            string _sendFloatFunctionName = "")
        {
            SendSpawnPrimitive(_type, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName, 
                _claimRecoveryClassName, _claimRecoveryFunctionName, _sendFloatClassName, _sendFloatFunctionName);
        }

        #endregion

        #region Prefab Instantiation

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity);
        /// }
        /// </code></example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation)
        {
            SendSpawnPrefab(_prefabName, _position, _rotation);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <example><code>
        /// //Where gameObject is the parent of the object that is being created here
        /// ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id); 
        /// </code>
        /// <code>
        /// void SomeFunction()
        /// {
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, ""); //Using this overload (and others) and passing in an empty string is a valid option 
        /// }
        /// </code>
        /// <code>
        /// void SomeOtherFunction()
        /// {
        ///     //Where 'MyParentObject' is the name of the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, "MyParentObject"); 
        ///     //The parent of your object can also be found by passing in the name (e.g., gameObject.name) of the parent you want. However, this method is a lot slower than using the ASL ID method, 
        ///     //but it is the only way to assign a non-ASL Object as a parent to an ASL Object for all users
        /// }
        /// </code></example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID)
        {
            SendSpawnPrefab(_prefabName, _position, _rotation, _parentID);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.
        /// When you use a Unity component, things are slightly different. For example, to add a Rigidbody component, you would enter this: "UnityEngine.Rigidbody,UnityEngine"
        /// In this case, the pattern is still the namespace + component, but then it is followed with by ",UnityEngine". If you need UnityEditor, try that after the comma</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Using just the name space and the class name should be enough for _componentAssemblyQualifiedName. For more info, go here
        ///     //https://docs.microsoft.com/en-us/dotnet/api/system.type.assemblyqualifiedname?view=netframework-4.8#System_Type_AssemblyQualifiedName
        ///     ASL.ASLHelper.InstanitateASLObject(PrimitiveType.Cube, new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id, "MyNamespace.MyComponent"); 
        ///     
        ///     //Note: If you need to add more than 1 component to an object, use the AddComponent ASL function after this object is created via an after creation function
        ///     //To do so, you will need to use the instantiatedGameObjectClassName and InstantiatedGameObjectFunctionName parameters as well. 
        ///     //See those instantiation overload options for more details
        /// }
        /// </code>
        ///</example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName)
        {
            SendSpawnPrefab(_prefabName, _position, _rotation, _parentID, _componentAssemblyQualifiedName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated.</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated.</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction"); //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        /// }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        /// }
        /// 
        /// </code></example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName,
            string _instantiatedGameObjectFunctionName)
        {
            SendSpawnPrefab(_prefabName, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated. 
        /// This parameter is now optional. If you use this parameter you must also provide _instantiatedGameObjectFunctionName</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated.
        /// This parameter is not optional. If you use this parameter you must also provide _instantiatedGameObjectClassName </param>
        /// <param name="_claimRecoveryClassName">This is the name of the class that contains your function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryFunctionName</param>
        /// <param name="_claimRecoveryFunctionName">This is the name of the function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryClassName</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction", GetType().Namespace + "." + GetType().Name, "MyClaimRejectedFunction");
        ///     //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        ///  }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        /// }
        /// 
        /// public static void MyClaimRejectedFunction(string _id, int _cancelledCallbacks)
        /// {
        ///    Debug.LogWarning("We are going to cancel " + _cancelledCallbacks +
        ///       " callbacks generated by a claim for object: " + _id + " rather than try to recover.");
        /// } 
        /// 
        /// </code></example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName = "",
            string _instantiatedGameObjectFunctionName = "",
            string _claimRecoveryClassName = "",
            string _claimRecoveryFunctionName = "")
        {
            SendSpawnPrefab(_prefabName, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName, _claimRecoveryClassName, _claimRecoveryFunctionName);
        }

        /// <summary>
        /// Create an ASL Object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be instantiated. Make sure your prefab is located in the Resources/Prefabs folder so it can be found</param>
        /// <param name="_position">The position where the object will be instantiated</param>
        /// <param name="_rotation">The rotation orientation of the object to be instantiated</param>
        /// <param name="_parentID">The id or name of the parent object for this instantiated object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">This is the name of the class that contains your function that you want to be executed after this object is instantiated. 
        /// This parameter is now optional. If you use this parameter you must also provide _instantiatedGameObjectFunctionName</param>
        /// <param name="_instantiatedGameObjectFunctionName">This is the name of the function that you want to be executed after this object is instantiated.
        /// This parameter is not optional to remove ambiguity. However, you can pass in an empty string if you don't need to do anything upon creation.</param>
        /// <param name="_claimRecoveryClassName">This is the name of the class that contains the function you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryClassName</param>
        /// /// <param name="_claimRecoveryFunctionName">This is the name of the function that you want to be executed if a claim for this object is ever rejected.
        /// This parameter is optional. If you use this parameter you must also provide _claimRecoveryClassName</param>
        /// <param name="_sendFloatClassName">This is the name of the class that contains your function that you want to be executed whenever you use the 
        /// <see cref="ASLObject.SendFloat4(float[])"/> function. This parameter is optional. If you use this parameter you must also provide _sendFloatFunctionName</param>
        /// <param name="_sendFloatFunctionName">This is the name of the function that you want to be executed whenever you use the <see cref="ASLObject.SendFloat4(float[])"/> function.
        /// This parameter is optional. If you use this parameter you must also provide _sendFloatClassName</param>
        /// /// <example><code>
        /// void SomeFunction()
        /// {
        ///     //Where gameObject is the parent of the object that is being created here
        ///     ASL.ASLHelper.InstanitateASLObject("MyPrefab", new Vector3(0, 0, 0), Quaternion.identity, gameobject.GetComponent&lt;ASL.ASLObject&gt;().m_Id,
        ///     "MyNamespace.MyClass",
        ///     GetType().Namespace + "." + GetType().Name, "MyUponInstantiationFunction", GetType().Namespace + "." + GetType().Name, "MyClaimRejectedFunction",
        ///     GetType().Namespace + "." + GetType().Name, "MySendFloatFunction"); //'GetType().Namespace + "." + GetType().Name' automatically returns the name space and class 
        ///     //name of the file your code is in. You can manually enter these values if you want, but this saves you the trouble of changing your 
        ///     //value if you ever change your name space or class names
        /// }
        /// public static void MyUponInstantiationFunction(GameObject _myGameObject)
        /// {
        ///     Debug.Log("Caller-Object ID: " + _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().m_Id);
        /// _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().SendAndSetClaim(() =>
        /// {
        ///     float[] myFloats = new float[] { 1.1f, 2.5f, 3.4f, 4.9f };
        ///     _myGameObject.GetComponent&lt;ASL.ASLObject&gt;().SendFloat4(myFloats);
        /// });
        /// }
        /// 
        /// public static void MyClaimRejectedFunction(string _id, int _cancelledCallbacks)
        /// {
        ///    Debug.LogWarning("We are going to cancel " + _cancelledCallbacks +
        ///       " callbacks generated by a claim for object: " + _id + " rather than try to recover.");
        /// } 
        /// public static void MySendFloatFunction(float[] _floats)
        /// {
        ///     for (int i = 0; i&lt;_floats.Length; i++)
        ///     {
        ///         Debug.Log("Value Sent: " + _floats[i]);
        ///     }
        /// } 
        /// 
        /// </code></example>
        static public void InstanitateASLObject(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID, string _componentAssemblyQualifiedName,
            string _instantiatedGameObjectClassName,
            string _instantiatedGameObjectFunctionName,
            string _claimRecoveryClassName = "",
            string _claimRecoveryFunctionName = "",
            string _sendFloatClassName = "",
            string _sendFloatFunctionName = "")
        {
            SendSpawnPrefab(_prefabName, _position, _rotation, _parentID, _componentAssemblyQualifiedName, _instantiatedGameObjectClassName, _instantiatedGameObjectFunctionName, 
                _claimRecoveryClassName, _claimRecoveryFunctionName, _sendFloatClassName, _sendFloatFunctionName);
        }


        #endregion

        #endregion


        /// <summary>
        /// Sends a packet out to all players to spawn an object based upon a prefab
        /// </summary>
        /// <param name="_type">The type of primitive to be spawned</param>
        /// <param name="_position">The position of where the object will be spawned</param>
        /// <param name="_rotation">The rotation orientation of the object upon spawn</param>
        /// <param name="_parentID">The id of the parent object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">The name of the class that contains the user provided function detailing what to do with this object after creation</param>
        /// <param name="_instantiatedGameObjectFunctionName">The name of the user provided function that contains the details of what to do with this object after creation</param>
        /// <param name="_claimRecoveryClassName">The name of the class that contains the user provided function detailing what to do if a claim for this object is rejected</param>
        /// <param name="_claimRecoveryFunctionName">The name of the user provided function that contains the details of what to do with this object if a claim for it is rejected</param>
        /// <param name="_sendFloatClassName">The name of the class that contains the user provided function detailing what to do when a user calls <see cref="ASLObject.SendFloat4(float[])"/></param>
        /// <param name="_sendFloatFunctionName">The name of the user provided function that contains the details of what to do with this object if a user calls <see cref="ASLObject.SendFloat4(float[])"/></param>
        private static void SendSpawnPrimitive
            (PrimitiveType _type, Vector3 _position, Quaternion _rotation, string _parentID = "", string _componentAssemblyQualifiedName = "", string _instantiatedGameObjectClassName = "", string _instantiatedGameObjectFunctionName = "", 
            string _claimRecoveryClassName = "", string _claimRecoveryFunctionName = "", string _sendFloatClassName = "", string _sendFloatFunctionName = "")
        {
            string guid = Guid.NewGuid().ToString();
            using (RTData data = RTData.Get())
            {
                data.SetString((int)GameController.DataCode.Id, guid);
                data.SetInt((int)GameController.DataCode.PrimitiveType, (int)_type);
                data.SetVector3((int)GameController.DataCode.LocalPosition, _position);
                data.SetVector4((int)GameController.DataCode.LocalRotation, new Vector4(_rotation.x, _rotation.y, _rotation.z, _rotation.w));
                data.SetString((int)GameController.DataCode.ParentId, _parentID);
                data.SetString((int)GameController.DataCode.ComponentName, _componentAssemblyQualifiedName);
                data.SetString((int)GameController.DataCode.InstantiatedGameObjectClassName, _instantiatedGameObjectClassName);
                data.SetString((int)GameController.DataCode.InstantiatedGameObjectFunctionName, _instantiatedGameObjectFunctionName);
                data.SetString((int)GameController.DataCode.ClaimRecoveryClassName, _claimRecoveryClassName);
                data.SetString((int)GameController.DataCode.ClaimRecoveryFunctionName, _claimRecoveryFunctionName);
                data.SetString((int)GameController.DataCode.SendFloatClassName, _sendFloatClassName);
                data.SetString((int)GameController.DataCode.SendFloatFunctionName, _sendFloatFunctionName);
                GameSparksManager.Instance().GetRTSession().SendData((int)GameSparksManager.OpCode.SpawnPrimitive, GameSparksRT.DeliveryIntent.RELIABLE, data);
            }
        }

        /// <summary>
        /// Sends a packet out to all players to spawn a prefab object
        /// </summary>
        /// <param name="_prefabName">The name of the prefab to be used</param>
        /// <param name="_position">The position of where the object will be spawned</param>
        /// <param name="_rotation">The rotation orientation of the object upon spawn</param>
        /// <param name="_parentID">The id of the parent object</param>
        /// <param name="_componentAssemblyQualifiedName">The full name of the component to be added to this object upon creation.</param>
        /// <param name="_instantiatedGameObjectClassName">The name of the class that contains the user provided function detailing what to do with this object after creation</param>
        /// <param name="_instantiatedGameObjectFunctionName">The name of the user provided function that contains the details of what to do with this object after creation</param>
        /// <param name="_claimRecoveryClassName">The name of the class that contains the user provided function detailing what to do if a claim for this object is rejected</param>
        /// <param name="_claimRecoveryFunctionName">The name of the user provided function that contains the details of what to do with this object if a claim for it is rejected</param>
        /// <param name="_sendFloatClassName">The name of the class that contains the user provided function detailing what to do when a user calls <see cref="ASLObject.SendFloat4(float[])"/></param>
        /// <param name="_sendFloatFunctionName">The name of the user provided function that contains the details of what to do with this object if a user calls <see cref="ASLObject.SendFloat4(float[])"/></param>
        private static void SendSpawnPrefab(string _prefabName, Vector3 _position, Quaternion _rotation, string _parentID = "", string _componentAssemblyQualifiedName = "", string _instantiatedGameObjectClassName = "", string _instantiatedGameObjectFunctionName = "",
            string _claimRecoveryClassName = "", string _claimRecoveryFunctionName = "", string _sendFloatClassName = "", string _sendFloatFunctionName = "")
        {
            string guid = Guid.NewGuid().ToString();
            using (RTData data = RTData.Get())
            {
                data.SetString((int)GameController.DataCode.Id, guid);
                data.SetString((int)GameController.DataCode.PrefabName, _prefabName);
                data.SetVector3((int)GameController.DataCode.LocalPosition, _position);
                data.SetVector4((int)GameController.DataCode.LocalRotation, new Vector4(_rotation.x, _rotation.y, _rotation.z, _rotation.w));
                data.SetString((int)GameController.DataCode.ParentId, _parentID);
                data.SetString((int)GameController.DataCode.ComponentName, _componentAssemblyQualifiedName);
                data.SetString((int)GameController.DataCode.InstantiatedGameObjectClassName, _instantiatedGameObjectClassName);
                data.SetString((int)GameController.DataCode.InstantiatedGameObjectFunctionName, _instantiatedGameObjectFunctionName);
                data.SetString((int)GameController.DataCode.ClaimRecoveryClassName, _claimRecoveryClassName);
                data.SetString((int)GameController.DataCode.ClaimRecoveryFunctionName, _claimRecoveryFunctionName);
                data.SetString((int)GameController.DataCode.SendFloatClassName, _sendFloatClassName);
                data.SetString((int)GameController.DataCode.SendFloatFunctionName, _sendFloatFunctionName);
                GameSparksManager.Instance().GetRTSession().SendData((int)GameSparksManager.OpCode.SpawnPrefab, GameSparksRT.DeliveryIntent.RELIABLE, data);
            }
        }


        /// <summary>
        /// Change scene for all players. This function is called by a user. 
        /// </summary>
        /// <param name="_sceneName">The name of the scene to change to</param>
        /// <example><code>
        /// void SomeFunction()
        /// {
        ///     ASL.ASLHelper.SendAndSetNewScene("YourSceneName");
        /// }
        /// </code></example>
        public static void SendAndSetNewScene(string _sceneName)
        {
            using (RTData data = RTData.Get())
            {
                data.SetString((int)GameController.DataCode.SceneName, _sceneName);
                GameSparksManager.Instance().GetRTSession().SendData((int)GameSparksManager.OpCode.LoadScene, GameSparksRT.DeliveryIntent.RELIABLE, data);
            }
        }

    }
}

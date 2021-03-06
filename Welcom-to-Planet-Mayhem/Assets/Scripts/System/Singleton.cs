﻿//=======================================================================================================================================//
// Product:    	GameSystems																												 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

#region Imports

using UnityEngine;

#endregion


public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
	//=====================================================================================================================//
	//=================================================== Private Fields ==================================================//
	//=====================================================================================================================//

	#region Private Fields

	protected static T instance;
	static object _lock = new Object ();
	static bool applicationIsQuitting = false;

	#endregion

	//=====================================================================================================================//
	//=================================================== Public Fields ===================================================//
	//=====================================================================================================================//

	#region Public Fields

	public static T Instance {
		get {
			if (applicationIsQuitting) {
				Debug.LogWarning ("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit. Won't create again - returning null.");
				return null;
			}

			lock (_lock) {
				if (instance == null) {					
					//Check if there's an object in the active scene
					instance = FindObjectOfType<T> ();
					if (instance == null) {							
						// Check if prefab exists in Resources Folder.
						// -- Prefab must have the same name as the Singleton SubClass
						var prefab = Resources.Load<GameObject> (typeof(T).ToString ());

						//Create singleton from prefab or as new gameObject
						if (prefab != null)
							instance = Instantiate<GameObject> (prefab).GetComponent<T> ();
						else
							instance = new GameObject (typeof(T).ToString ()).AddComponent<T> ();

					}
					if (Application.isPlaying)
						DontDestroyOnLoad (instance.gameObject);
				}
			}

			return instance;				
		}
	}

	#endregion

	//=====================================================================================================================//
	//=============================================== Unity Event Methods =================================================//
	//=====================================================================================================================//

	#region Unity Event Functions

	void Awake ()
	{
		instance = Instance;
	}

	// To make sure there are no ghost objects in editor after the object is destroyed
	void OnDestroy ()
	{
		applicationIsQuitting = true;
	}

	#endregion
}

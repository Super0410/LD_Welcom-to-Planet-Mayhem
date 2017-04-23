using UnityEngine;
using System;
using System.Collections.Generic;
using GameSystems.Patterns;

namespace GameSystems.ObjectManagement
{
	public class ObjectPooler : Singleton<ObjectPooler>
	{
		//===============================================================================================================================//
		//==================================================== Inspector Variables ======================================================//
		//===============================================================================================================================//

		#region Public Properties

		public bool debug;

		#endregion

		//===============================================================================================================================//
		//======================================================= Private Fields ========================================================//
		//===============================================================================================================================//

		#region Private Fields

		static Dictionary <string, List<GameObject>> pools = new Dictionary<string, List<GameObject>> ();
		static List<GameObject> poolParents = new List<GameObject> ();

		const int DEFAULT_POOL_SIZE = 50;
		static Vector3 STACK_POSITION = new Vector3 (-9999f, -9999f, -9999f);

		#endregion

		//===============================================================================================================================//
		//=========================================================== Public Methods ====================================================//
		//===============================================================================================================================//

		#region Public Methods

		/// <summary>
		/// Creates a pool of given prefab. 
		/// </summary>
		/// <returns><c>true</c>, if pool was generated, <c>false</c> otherwise.</returns>
		/// <param name = "objectID"></param>
		/// <param name="prefab">Prefab.</param>
		/// <param name="poolSize">Pool size.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool CreatePool (string objectID, GameObject prefab, int poolSize = DEFAULT_POOL_SIZE)
		{
			if (prefab == null || poolSize <= 0 || string.IsNullOrEmpty (objectID))
				return false;

			//Pool of type T already exists
			if (HasPoolOfType (objectID)) {
				if (Instance.debug)
					Debug.Log ("============= ObjectPool_CreatePool<" + objectID + ">: Pool of type already exists. =============");
				return false;
			}

			if (Instance.debug)
				Debug.Log ("============= ObjectPool_CreatePool<" + objectID + ">: Generating pool =============");

			List<GameObject> _newPool = new List<GameObject> (poolSize);
			GameObject _parent = new GameObject (prefab.GetType ().Name + " Pool");
			poolParents.Add (_parent);
			_parent.transform.position = STACK_POSITION;

			for (int i = 0; i < poolSize; i++) {
				var _instance = Instantiate (prefab) as GameObject;
				_instance.name = prefab.name;
				_instance.transform.position = STACK_POSITION;
				_instance.transform.SetParent (_parent.transform);
				_instance.SetActive (false);
				_newPool.Add (_instance);
			}

			pools.Add (objectID, _newPool);
			return true;
		}

		/// <summary>
		/// Gets the Instance of type. if it exists.
		/// </summary>
		/// <returns>The Instance.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static GameObject FetchInstance (string objectID)
		{
			if (!HasPoolOfType (objectID)) {
				if (Instance.debug)
					Debug.Log ("============= ObjectPool_FetchInstance<" + objectID + ">: Pool of given type does not exist =============");
				return null;
			}

			if (Instance.debug)
				Debug.Log ("============= ObjectPool_FetchInstance<" + objectID + ">: Fetching first available Instance. =============");

			var _instance = pools [objectID].Find (obj => !obj.gameObject.activeSelf);

			//If there are no inactive Instances in the pool, increase the pool size and return one of the new Instances.
			if (_instance == null) {
				if (Instance.debug)
					Debug.Log ("============= ObjectPool_FetchInstance<" + objectID + ">: There are no available Instances. Increasing pool size =============");

				_instance = pools [objectID] [0];
				var _parent = poolParents.Find (_p => _p.name == _instance.GetType ().Name + " Pool");
				for (int i = 0; i < DEFAULT_POOL_SIZE; i++) {
					var _newInstance = Instantiate (_instance);
					_newInstance.name = _instance.name;
					_newInstance.transform.position = STACK_POSITION;
					_newInstance.transform.SetParent (_parent.transform);
					_newInstance.SetActive (false);
					pools [objectID].Add (_newInstance);
				}
				_instance = pools [objectID] [DEFAULT_POOL_SIZE];
			}

			_instance.transform.SetParent (null);
			return _instance;
		}

		/// <summary>
		/// Returns the instance to the pool.
		/// </summary>
		/// <returns><c>true</c>, if instance was returned, <c>false</c> otherwise.</returns>
		/// <param name="_instance">Instance.</param>
		/// <typeparam name="T">MonoBehaviour.</typeparam>
		public static bool ReturnInstance (string objectID, GameObject _instance)
		{
			if (_instance == null || (!HasPoolOfType (objectID) && !CreatePool (objectID, _instance))) {
				return false;
			}

			var _parent = poolParents.Find (_p => _p.name == _instance.GetType ().Name + " Pool");

			_instance.transform.position = STACK_POSITION;
			_instance.transform.SetParent (_parent.transform);
			_instance.SetActive (false);
			pools [objectID].Add (_instance);

			return true;
		}

		/// <summary>
		/// Determines whether a pool of given type exists
		/// </summary>
		/// <returns><c>true</c> if pool of type exists; otherwise, <c>false</c>.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool HasPoolOfType (string objectID)
		{			
			if (pools == null) {
				pools = new Dictionary<string, List<GameObject>> ();
				return false;
			}

			return pools.Count != 0 && pools.ContainsKey (objectID);

		}

		/// <summary>
		/// Count the number of active Instances for given type.
		/// </summary>
		/// <returns><c>-1</c> if given type does not have a generated pool; The number of active Instances of type, otherwise</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static int CountActiveInstancesOfType (string objectID)
		{
			if (!HasPoolOfType (objectID))
				return -1;

			return pools [objectID].FindAll (obj => obj.gameObject.activeSelf).Count;
		}

		#endregion

	}
}
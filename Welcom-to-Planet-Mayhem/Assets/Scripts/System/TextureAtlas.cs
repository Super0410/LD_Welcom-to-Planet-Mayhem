//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//
using System.Collections.Generic;

#region Imports

using UnityEngine;

#endregion


public static class TextureAtlas
{
	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	static Dictionary<string, Texture> atlas = new Dictionary<string, Texture> ();

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public static void LoadTextures ()
	{
		if (atlas == null) {
			atlas = new Dictionary<string, Texture> ();
		}

		var textures = Resources.LoadAll<Texture> ("/Textures/");
		for (int i = 0; i < textures.Length; i++) {
			if (!atlas.ContainsKey (textures [i].name)) {
				atlas.Add (textures [i].name, textures [i]);
			}
		}
	}

	public static bool HasTexture (string id)
	{
		return atlas.ContainsKey (id) && atlas [id] != null;
	}

	public static bool AddTexture (string id, Texture texture)
	{
		if (atlas == null)
			LoadTextures ();
			
		if (HasTexture (id))
			return false;
			
		atlas.Add (id, texture);

		return true;
	}

	public static Texture GetTexture (string id)
	{
		if (atlas == null) {
			LoadTextures ();
		}

		var texture = new Texture ();
		if (atlas.TryGetValue (id, out texture)) {
			return texture;
		}
		return null;
	}

	#endregion

	//===============================================================================================================================//
	//========================================================== Private  Methods ===================================================//
	//===============================================================================================================================//

	#region Private Methods



	#endregion

}

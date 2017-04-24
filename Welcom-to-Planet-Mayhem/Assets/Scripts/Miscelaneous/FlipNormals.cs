//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

#region Imports

using UnityEngine;
using System.Collections;

#endregion

[RequireComponent (typeof(MeshFilter))]
public class FlipNormals : MonoBehaviour
{
	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	[ContextMenu ("Flip")]
	public void Flip ()
	{
		var mesh = GetComponent<MeshFilter> ().mesh;
		var normals = mesh.normals;
		for (int i = 0; i < normals.Length; i++) {
			normals [i] *= -1;
		}

		mesh.normals = normals;

		for (int i = 0; i < mesh.subMeshCount; i++) {
			var tris = mesh.triangles;
			for (int j = 0; j < tris.Length; j += 3) {
				var tmp = tris [j];
				tris [j] = tris [j + 1];
				tris [j + 1] = tmp;
			}
			mesh.SetTriangles (tris, i);
		}
	}

	public static Mesh Flip (Mesh mesh)
	{
		var normals = mesh.normals;
		for (int i = 0; i < normals.Length; i++) {
			normals [i] *= -1;
		}

		mesh.normals = normals;

		for (int i = 0; i < mesh.subMeshCount; i++) {
			var tris = mesh.triangles;
			for (int j = 0; j < tris.Length; j += 3) {
				var tmp = tris [j];
				tris [j] = tris [j + 1];
				tris [j + 1] = tmp;
			}
			mesh.SetTriangles (tris, i);
		}
		return mesh;
	}

	#endregion

}
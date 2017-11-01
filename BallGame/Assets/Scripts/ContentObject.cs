using UnityEngine;
using System.Collections;

public abstract class ContentObject /*: GameObject*/ {

	protected float[] coords{ get; set; }
	protected float[] scale{ get; set; }
	//protected GameObject thisObject;

	public ContentObject(/*float x, float y, float z, float scaleX, float scaleY, float scaleZ*/){
		/*coords = new float[]{ x, y, z };
		scale = new float[]{ scaleX, scaleY, scaleZ };*/
	
	}
		

	public static void setTransform(GameObject go, string name, float x, float y, float z, float scaleX, float scaleY, float scaleZ){
		go.transform.parent = GameObject.Find ("Content").transform;
		go.transform.position = new Vector3 (x, y, z);
		go.transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
		go.name = name;
	}

	public static void addMaterial(string goName, string matName){
		Material[] mats = new Material[]{ Resources.Load (matName, typeof(Material)) as Material };
		GameObject go = GameObject.Find (goName);
		go.GetComponent<Renderer>().materials = mats;
	}


}

public class ContentCube : ContentObject {

	/*public ContentCube(float x, float y, float z, float scaleX, float scaleY, float scaleZ) : base(x, y, z, scaleX, scaleY, scaleZ){

	}*/

	public static void convert(GameObject go, string name, float x, float y, float z, float scaleX, float scaleY, float scaleZ){
		setTransform (go, name, x, y, z, scaleX, scaleY, scaleZ);
		GameObject primitive = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Mesh mesh = primitive.GetComponent<MeshFilter> ().mesh;
		go.AddComponent<BoxCollider> ();
		go.AddComponent<MeshFilter> ();
		go.GetComponent<MeshFilter> ().mesh = mesh;
		go.AddComponent<MeshRenderer> ();
		addPhysicMaterial (go.name, "surface");

		GameObject.Destroy (primitive);
	}
	public static void addPhysicMaterial(string goName, string physName){
		PhysicMaterial phys = Resources.Load (physName, typeof(PhysicMaterial)) as PhysicMaterial;
		GameObject.Find(goName).GetComponent<BoxCollider> ().material = phys;
	}


}

public class ContentSphere : ContentObject {
	public static void convert(GameObject go, string name, float x, float y, float z, float scaleX, float scaleY, float scaleZ){
		setTransform(go, name, x, y, z, scaleX, scaleY, scaleZ);
		GameObject primitive = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		Mesh mesh = primitive.GetComponent<MeshFilter>().mesh;

		go.AddComponent<SphereCollider> ();
		go.AddComponent<MeshFilter> ();
		go.GetComponent<MeshFilter>().mesh = mesh;
		go.AddComponent<MeshRenderer> ();
		go.AddComponent<Rigidbody> ();
		GameObject.Destroy(primitive);
	}

	public static void addPhysicMaterial(string goName, string physName){
		PhysicMaterial phys = Resources.Load (physName, typeof(PhysicMaterial)) as PhysicMaterial;
		GameObject.Find(goName).GetComponent<SphereCollider> ().material = phys;
	}
}

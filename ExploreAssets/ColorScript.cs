using UnityEngine;
using System.Collections;

public class ColorScript : MonoBehaviour {

	public int id;
	// Use this for initialization
	void Start () {

		if (DataScript.Party[id].exists)
		{
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = DataScript.Party[id].hair_color;
			transform.GetChild(1).GetComponent<SpriteRenderer>().color = DataScript.Party[id].skin_color;
			transform.GetChild(2).GetComponent<SpriteRenderer>().color = DataScript.Party[id].shirt_color;
			transform.GetChild(3).GetComponent<SpriteRenderer>().color = DataScript.Party[id].pants_color;
			transform.GetChild(4).GetComponent<SpriteRenderer>().color = DataScript.Party[id].shoes_color;
		}
		else
		{
			Vector4 temp = new Vector4(transform.GetComponent<Renderer>().material.color.r,//keep original color
																   transform.GetComponent<Renderer>().material.color.g,
																   transform.GetComponent<Renderer>().material.color.b,
																   0);
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(1).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(2).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(3).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(4).GetComponent<SpriteRenderer>().color = temp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!DataScript.Party[id].exists)
		{
			Vector4 temp = new Vector4(transform.GetComponent<Renderer>().material.color.r,//keep original color
																   transform.GetComponent<Renderer>().material.color.g,
																   transform.GetComponent<Renderer>().material.color.b,
																   0);
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(1).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(2).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(3).GetComponent<SpriteRenderer>().color = temp;
			transform.GetChild(4).GetComponent<SpriteRenderer>().color = temp;
		}
	}
}

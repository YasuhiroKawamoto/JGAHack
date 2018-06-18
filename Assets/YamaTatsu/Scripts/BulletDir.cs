using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDir : MonoBehaviour {

   
    public enum Dir
    {
        Front,
        Back,
        Left,
        Right
    }

    [SerializeField]
    private Dir _dir;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        switch (_dir)
        {
            case Dir.Front:
                this.transform.Rotate(0, 0, 90);
                break;
            case Dir.Back:
                this.transform.Rotate(0, 0, 270);
                break;
            case Dir.Left:
                this.transform.Rotate(0, 0, 180);
                break;
            case Dir.Right:
                this.transform.Rotate(0, 0, 0);
                break;
        }

    }

    public void SetDir(Dir dir)
    {
        _dir = dir;
    }

}

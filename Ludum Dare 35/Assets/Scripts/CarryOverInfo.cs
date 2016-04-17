using UnityEngine;
using System.Collections;

public class CarryOverInfo : MonoBehaviour {

    public static int levelNumber;
    public static bool DemonControl;

    void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
	}
	
	
}

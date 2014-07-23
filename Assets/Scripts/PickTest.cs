using UnityEngine;
using System.Collections;

public class PickTest : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			PickManager.PickManagerConfigure(Camera.main);
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			//PickManager.startPreciseRotate(Input.mousePosition);
			PickManager.StartPick(Input.mousePosition);
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			//PickManager.stopPreciseRotate();	
			PickManager.StopPick();
		}

		if (Input.GetKey (KeyCode.M)) {
			//PickManager.preciseMoveRotate(Input.mousePosition);	
			PickManager.MovePick(Input.mousePosition);
		}

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PickManager.StartPreciseRotate(Input.mousePosition);
            //PickManager.startPick(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PickManager.StopPreciseRotate();	
            //PickManager.stopPick();
        }

        if (Input.GetKey(KeyCode.I))
        {
            PickManager.PreciseMoveRotate(Input.mousePosition);	
            //PickManager.movePick(Input.mousePosition);
        }

		if (Input.GetKeyDown (KeyCode.Q)) {
			PickManager.StartPrecisePick(Input.mousePosition);		
		}
		
		if (Input.GetKeyDown (KeyCode.W)) {
			PickManager.StopPrecisePick();		
		}
		
		if (Input.GetKey (KeyCode.R)) {
			PickManager.PreciseMovePick(Input.mousePosition);		
		}
	}
}

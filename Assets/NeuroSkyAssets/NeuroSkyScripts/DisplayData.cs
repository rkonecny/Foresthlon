using UnityEngine;
using System.Collections;

public class DisplayData : MonoBehaviour
{
	public Texture2D[] signalIcons;
	
	public int indexSignalIcons = 1;
	
    TGCConnectionController controller;

    void Start()
    {
		
		controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
		
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;		
    }
	
	void OnUpdatePoorSignal(int value){
		if(value < 25){
      		indexSignalIcons = 0;
		}else if(value >= 25 && value < 51){
      		indexSignalIcons = 4;
		}else if(value >= 51 && value < 78){
      		indexSignalIcons = 3;
		}else if(value >= 78 && value < 107){
      		indexSignalIcons = 2;
		}else if(value >= 107){
      		indexSignalIcons = 1;
		}
	}

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Space(Screen.width - 40);
        GUILayout.Label(signalIcons[indexSignalIcons]);

        GUILayout.EndHorizontal();

    }
}

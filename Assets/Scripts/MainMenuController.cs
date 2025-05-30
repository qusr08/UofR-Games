using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour {
    private VisualElement ui;

    private void Awake( ) {
        ui = GetComponent<UIDocument>( ).rootVisualElement;

        // Set up menu button functionality
        ui.Q<Button>("CraveSmashButton").clicked += ( ) => { SceneManager.LoadScene(1); };
        //ui.Q<Button>("PuffDodgeButtom").clicked += ( ) => { SceneManager.LoadScene(2); };
        //ui.Q<Button>("BlowItOffButton").clicked += ( ) => { SceneManager.LoadScene(3); };
        //ui.Q<Button>("NotSoTastyButton").clicked += ( ) => { SceneManager.LoadScene(4); };
        //ui.Q<Button>("MatchAndCatchButton").clicked += ( ) => { SceneManager.LoadScene(5); };

        ui.Q<Button>("QuitButton").clicked += ( ) => { 
            Application.Quit( );
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        };
    }
}

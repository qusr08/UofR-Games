using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public abstract class GameController : UIController {
    [Header("GameController")]
    [SerializeField, Range(0f, 10f)] private float tutorialTime;

    private float tutorialTimer;
    private int currentPoints;

    protected Label scoreLabel;
    protected Label finalScoreLabel;
    protected Label totalScoreLabel;

    protected VisualElement gameScreen;
    protected VisualElement pauseScreen;
    protected VisualElement winScreen;

    protected VisualElement gameSubscreen;
    protected VisualElement tutorialSubscreen;

    protected override void Awake( ) {
        base.Awake( );

        // Get all screens within the game
        // Each game should have the same types of screens, but more functionality will need to be added individually
        gameSubscreen = ui.Q<VisualElement>("GameSubscreen");
        tutorialSubscreen = ui.Q<VisualElement>("TutorialSubscreen");
        gameScreen = ui.Q<VisualElement>("GameScreen");
        pauseScreen = ui.Q<VisualElement>("PauseScreen");
        winScreen = ui.Q<VisualElement>("WinScreen");

        // Set the states of the screens and subscreens based on what game controller state is active
        screens[(int) UIState.TUTORIAL] = gameScreen;
        screens[(int) UIState.GAME] = gameScreen;
        screens[(int) UIState.PAUSE] = pauseScreen;
        screens[(int) UIState.WIN] = winScreen;

        subscreens[(int) UIState.TUTORIAL] = tutorialSubscreen;
        subscreens[(int) UIState.GAME] = gameSubscreen;

        // Set all common button functions
        // Each game should also have these buttons
        ui.Q<Button>("ResumeButton").clicked += ( ) => { UIControllerState = LastUIControllerState; };
        ui.Q<Button>("QuitButton").clicked += ( ) => { FadeToScene(0); };
        ui.Q<Button>("HomeButton").clicked += ( ) => { FadeToScene(0); };
        ui.Q<Button>("PlayAgainButton").clicked += ( ) => { FadeToScene(SceneManager.GetActiveScene( ).buildIndex); };
        ui.Q<Button>("PauseButton").clicked += ( ) => { UIControllerState = UIState.PAUSE; };

        // Get references to other important UI elements
        scoreLabel = ui.Q<Label>("ScoreLabel");
        finalScoreLabel = ui.Q<Label>("FinalScoreLabel");
        totalScoreLabel = ui.Q<Label>("TotalScoreLabel");

        // Set default values for some of the variables
        tutorialTimer = 0f;

        // Update the score labels for the beginning of the game
        AddPoints(0);
    }

    protected virtual void Update( ) {
        // If the current game controller state is not the tutorial, then do not update the tutorial timer
        if (UIControllerState != UIState.TUTORIAL) {
            return;
        }

        tutorialTimer += Time.deltaTime;

        // Once the tutorial timer has reached the alloted tutorial time, then switch the game state to show the game
        if (tutorialTimer > tutorialTime) {
            UIControllerState = UIState.GAME;
        }
    }

    /// <summary>
    /// Update all of the subscreens in this game controller based on the current game controller state
    /// </summary>
    protected override void UpdateSubscreens( ) {
        SetSubscreenVisibility(gameSubscreen, UIControllerState == UIState.GAME);
        SetSubscreenVisibility(tutorialSubscreen, UIControllerState == UIState.TUTORIAL);
    }

    protected void AddPoints (int points) {
        // Add the points to the total values
        currentPoints += points;
        playerData.TotalPoints += points;

        // Update the score label text based on the new score value
        scoreLabel.text = $"Score: <b>{currentPoints} points</b>";
        finalScoreLabel.text = $"{currentPoints} points";
        totalScoreLabel.text = $"{playerData.TotalPoints} points";
    }
}

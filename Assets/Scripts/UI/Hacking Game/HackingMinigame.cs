using System.Collections;
using System;
using Game.Plugins.TimeProcesses;
using Game.State;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Hacking
{
    /// <summary>
    /// Handles minigame on screen. Its a simple "Find sequence of 4 buttons" game
    /// </summary>
    public class HackingMinigame : MonoBehaviour
    {
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TextMeshProUGUI _stateText;

        [SerializeField] private float stateUpdateDelay;

        [SerializeField] private Image[] buttonImages;
        [SerializeField] private Color colorNormal;
        [SerializeField] private Color colorCorrect;
        [SerializeField] private Color colorIncorrect;

        [SerializeField] private int maxTries;

        [SerializeField] private UnityEvent _gameStarted;
        [SerializeField] private UnityEvent _gameFinished;

        private HackingComponent _current;

        private int currentTry;

        private int[] currentSequence = new int[4] { 0, 1, 2, 3 };
        private int currentIndex;

        private bool inputAllowed;


        private void OnEnable()
        {
            HackingComponent.HackStarted += OnGameStarted;
        }

        private void OnDisable()
        {
            HackingComponent.HackStarted -= OnGameStarted;
        }


        public void OnButtonPress(int index)
        {
            if (!inputAllowed)
                return;

            if (index == currentSequence[currentIndex])
                OnPressedCorrectly();
            else
                OnPressedIncorrectly();
        }



        private void OnGameStarted(HackingComponent component)
        {
            _current = component;

            GenerateSequence();

            GameStateLocator.Service.Control.TogglePause(true);
            _gameStarted?.Invoke();

            _gameCanvas.enabled = true;
            inputAllowed = false;

            currentTry = 0;
            ResetIndex();
        }

        private void OnGameFinished()
        {
            _gameCanvas.enabled = false;
            GameStateLocator.Service.Control.TogglePause(false);
            _gameFinished?.Invoke();

            _current.SetSuccess();
            _current = null;
        }

        private void OnGameFailed()
        {
            _gameCanvas.enabled = false;
            GameStateLocator.Service.Control.TogglePause(false);
            _gameFinished?.Invoke();

            _current.SetFailed();
            _current = null;
        }


        /// <summary>
        /// Shuffles array of sequence around
        /// </summary>
        private void GenerateSequence()
        {
            var rng = new System.Random();
            var n = currentSequence.Length;

            while (n > 1)
            {
                // n--;
                int k = UnityEngine.Random.Range(0, --n);
                int temp = currentSequence[n];

                currentSequence[n] = currentSequence[k];
                currentSequence[k] = temp;
            }

            Debug.Log($"Sequence: {currentSequence[0] + 1}, {currentSequence[1] + 1}, {currentSequence[2] + 1}, {currentSequence[3] + 1}");
        }


        private void ResetIndex()
        {
            SetAllColors(colorNormal);
            currentIndex = 0;

            _stateText.SetText($"Tries left: {maxTries - currentTry}");

            inputAllowed = true;
        }


        private void OnPressedCorrectly()
        {
            SetColor(currentSequence[currentIndex], colorCorrect);
            currentIndex++;

            if (currentIndex >= currentSequence.Length)
            {
                _stateText.SetText("Success!");
                DoAfterDelay(OnGameFinished, stateUpdateDelay);
            }
        }

        private void OnPressedIncorrectly()
        {
            SetAllColors(colorIncorrect);
            currentTry++;

            inputAllowed = false;

            if (currentTry >= maxTries)
            {
                _stateText.SetText("Failed!");
                DoAfterDelay(OnGameFailed, stateUpdateDelay);
            }
            else
            {
                _stateText.SetText($"Tries left: {maxTries - currentTry}");
                DoAfterDelay(ResetIndex, stateUpdateDelay);
            }
        }


        private void SetColor(int index, Color color) => buttonImages[index].color = color;

        private void SetAllColors(Color color)
        {
            foreach (var image in buttonImages)
                image.color = color;
        }


        private void DoAfterDelay(Action action, float cooldown)
        {
            StartCoroutine(WaitProcess(action, cooldown));
        }

        private IEnumerator WaitProcess(Action action, float cooldown)
        {
            yield return new WaitForSecondsRealtime(cooldown);

            action.Invoke();
        }
    }
}
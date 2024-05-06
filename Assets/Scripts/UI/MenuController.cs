using System.Collections;
using ShootEmUp.GameCycle;
using ShootEmUp.GameCycle.Models;
using TMPro;
using UnityEngine;

namespace ShootEmUp.UI
{
    public class MenuController : MonoBehaviour, IGameStartListener, IGameEndListener, IGamePauseListener, IGameResumeListener
    {
        [SerializeField] private GameCycleStateMachine _gameCycleStateMachine;
        [SerializeField] private GameObject _startGamePanel;
        [SerializeField] private GameObject _pauseGamePanel;
        [SerializeField] private GameObject _resumeGamePanel;
        [SerializeField] private GameObject _counterGamePanel;
        [SerializeField] private TMP_Text _counterText;

        private void Start()
        {
            IGameEventListener.Register(this);
            this._startGamePanel.SetActive(true);
        }

        public void OnStartGameClick()
        {
            this._startGamePanel.SetActive(false);
            this._counterGamePanel.SetActive(true);
            
            StartCoroutine(StartCounter());
        }

        public void OnPauseGameClick()
        {
            this._gameCycleStateMachine.PauseGame();
        }

        public void OnEndGameClick()
        {
            this._gameCycleStateMachine.EndGame();
        }

        public void OnResumeGameClick()
        {
            this._gameCycleStateMachine.ResumeGame();
        }
        
        public void OnGameStart()
        {
            this._startGamePanel.SetActive(false);
            this._resumeGamePanel.SetActive(false);
            this._counterGamePanel.SetActive(false);
            this._pauseGamePanel.SetActive(true);
        }
        
        public void OnGameEnd()
        {
            this._pauseGamePanel.SetActive(false);
            this._resumeGamePanel.SetActive(false);
            this._startGamePanel.SetActive(true);
        }
        
        public void OnGamePause()
        {
            this._pauseGamePanel.SetActive(false);
            this._resumeGamePanel.SetActive(true);
        }
        
        public void OnGameResume()
        {
            this._pauseGamePanel.SetActive(true);
            this._resumeGamePanel.SetActive(false);
        }

        private IEnumerator StartCounter()
        {
            this._counterText.text = "3";
            yield return new WaitForSeconds(1f);
            this._counterText.text = "2";
            yield return new WaitForSeconds(1f);
            this._counterText.text = "1";
            yield return new WaitForSeconds(1f);
            this._counterText.text = "Go!";
            yield return new WaitForSeconds(0.5f);
            this._gameCycleStateMachine.StartGame();
        }
    }
}

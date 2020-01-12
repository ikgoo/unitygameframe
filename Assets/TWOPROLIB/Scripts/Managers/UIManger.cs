/**
 * Title : 전반적인 UI 컨트롤
 * Desc :
 * 게임 화면 전반적으로 활성화 유무를 컨트롤 함
 **/
using System;
using System.Collections;
using System.Collections.Generic;
using TWOPROLIB.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace TWOPROLIB.Scripts.Managers
{
    /// <summary>
    /// UI 관련 매니져
    /// </summary>
    public class UIManger : MonoBehaviour
    {
        [Serializable]
        public class GameUIView
        {
            /// <summary>
            /// 게임 상태
            /// </summary>
            [Tooltip("게임 상태")]
            public GameStateType gameState;
            /// <summary>
            /// 해당 게임에 보여야 할 오브젝트 리스트
            /// </summary>
            [Tooltip("해당 게임에 보여야 할 오브젝트 리스트")]
            public List<GameObject> gameStateObj;
            /// <summary>
            /// 게임 속도 설정
            /// </summary>
            [Tooltip("게임 속도 설정")]
            public float timeScale = 1;
        }

        [SerializeField]
        public List<GameUIView> lsGameStatView;


        /// <summary>
        /// 화면에 게임 상태 보일지 여부
        /// </summary>
        [Tooltip("화면에 게임 상태 보일지 여부")]
        public bool isShowDebugInfo;
        [Tooltip("GameState 보이기")]
        public Text GameStateView;
        [Tooltip("PlayState 보이기")]
        public Text PlayStateView;

        [Header("==== Option View ====")]
        /// <summary>
        /// 배경음 세팅용 슬라이더
        /// </summary>
        [Tooltip("배경음 세팅용 슬라이더")]
        public Slider slider_Music;
        /// <summary>
        /// 효과음 세팅용 슬라이더
        /// </summary>
        [Tooltip("효과음 세팅용 슬라이더")]
        public Slider slider_Effect;
        /// <summary>
        /// 진동 세팅용 토글
        /// </summary>
        [Tooltip("진동 세팅용 토글")]
        public ToggleController toggle_Vibrate;

        public static UIManger Instance = null;
        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            if (GameStateView != null)
                GameStateView.gameObject.SetActive(isShowDebugInfo);
            if (PlayStateView != null)
                PlayStateView.gameObject.SetActive(isShowDebugInfo);

            
        }

        void Update()
        {

        }

        /// <summary>
        /// 게임 상태 랜더링을 위한 상태
        /// </summary>
        bool isshowDebug = false;

        private void OnEnable()
        {
            isshowDebug = true;
            if (isShowDebugInfo)
            {
                StartCoroutine("ShowGameState");
            }
        }

        private void OnDisable()
        {
            isshowDebug = false;
        }

        IEnumerator ShowGameState()
        {
            while(isshowDebug)
            {
                // Show Game State 
                if (isShowDebugInfo && GameStateView != null && GameManager.Instance != null && GameManager.Instance.gameState != null)
                {
                    GameStateView.text = "GS:" + GameManager.Instance.gameState.gameState.ToString();
                }
                // Show Play State
                if (isShowDebugInfo && PlayStateView != null && GameManager.Instance != null && GameManager.Instance.playState != null)
                {
                    PlayStateView.text = "PS:" + GameManager.Instance.playState.playState.ToString();
                }
                yield return new WaitForEndOfFrame();
            }
        }


        #region Public Methods
        /// <summary>
        /// 게임 상태에 따른 UI 처리 - 같은 게임 상태의 UI를 화면에 출력
        /// </summary>
        public void ViewGameStateUI()
        {
            // 해당 UI를 View하기 위한 로직
            for (int i = 0; i < this.lsGameStatView.Count; i++)
            {
                for (int j = 0; j < this.lsGameStatView[i].gameStateObj.Count; j++)
                {
                    this.lsGameStatView[i].gameStateObj[j].SetActive(GameManager.Instance.gameState.gameState == this.lsGameStatView[i].gameState ? true : false);

                }
                Time.timeScale = this.lsGameStatView[i].timeScale;
            }

            // 해당 UI를 View한 후 세부 설정 관련 화면
            if (GameManager.Instance.gameState.gameState == GameStateType.Option && slider_Music != null)
            {
                // 옵션 화면 기존 설정 부분
                AudioManager.Instance.audioData.InitAudio();
                slider_Music.value = AudioManager.Instance.audioData.volumes[0].volume;
                slider_Effect.value = AudioManager.Instance.audioData.volumes[1].volume;
                if(toggle_Vibrate != null && GameManager.Instance.gameOption != null)
                    toggle_Vibrate.isOn = GameManager.Instance.gameOption.isVibrate;
            }

        }

        /// <summary>
        /// UI Update 처리
        /// </summary>
        public void UpdateUI()
        {
            // UI 업데이트 내용 추가
        }

        #endregion

        #region UI Buttons
        /// <summary>
        /// Game 상태 전환
        /// </summary>
        /// <param name="gameState"></param>
        public void OnGameStateButton(int gameState)
        {
            if(GameManager.Instance.gameState.gameState == GameStateType.Option)
            {
                AudioManager.Instance.audioData.SaveAudio();
            }
            GameManager.Instance.ChangeGameState((GameStateType)gameState);
        }

        /// <summary>
        /// Play 상태 전환
        /// </summary>
        /// <param name="playState"></param>
        public void OnPlayStateButton(int playState)
        {
            GameManager.Instance.ChangePlayState((PlayStateType)playState);
        }

        /// <summary>
        /// Show High Score
        /// </summary>
        public void OnShowHighScore()
        {
            Debug.Log("HighScore");
        }

        /// <summary>
        /// Show Rank
        /// </summary>
        public void OnShowRank()
        {
            Debug.Log("Rank");
        }

        /// <summary>
        /// Game 종료
        /// </summary>
        public void OnGameEnd()
        {
            Debug.Log("GameEnd");
        }

        #endregion

    }
}

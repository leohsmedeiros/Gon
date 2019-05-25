using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Gon {
    public class GameBoard : MonoBehaviour {
        public GameoverPanelController gameoverPanel;

        public Transform transformHealth;
        public Text tvCoins;
        public Text tvScore;

        public IntReactiveProperty health { private set; get; }
        public IntReactiveProperty coins { private set; get; }
        public IntReactiveProperty score { private set; get; }

        private void Awake() {
            health = new IntReactiveProperty(0);
            coins = new IntReactiveProperty(0);
            score = new IntReactiveProperty(0);
        }
        // Start is called before the first frame update
        void Start() {
            health.Value = 5;
            coins.Value = 0;
            score.Value = 0;

            health.Subscribe(observer => {
                if (health.Value == 0) {
                    Debug.Log("Dead");
                    gameoverPanel.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                } else {
                    for (int i = 0; i < transformHealth.childCount; i++)
                        transformHealth.GetChild(i).gameObject.SetActive(i < health.Value);
                }
            });

            coins.SubscribeToText(tvCoins, x => x.ToString());
            score.SubscribeToText(tvScore, x => x.ToString());
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                coins.Value++;
            }
            if (Input.GetMouseButtonDown(1)) {
                score.Value++;
            }
            if (Input.GetKeyDown(KeyCode.Space) && health.Value > 0) {
                health.Value--;
            }
            if (Input.GetKeyDown(KeyCode.Return) && health.Value < 5) {
                health.Value++;
            }
        }

    }
}

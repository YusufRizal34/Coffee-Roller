using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    public GameObject tutorial;
    public GameObject[] tutorialUI;
    private int _tutorialIndex;

    private Touch theTouch;
    private float timeTouchEnded;

    void Start() {
        if(!GameManager.Instance.ShowIsTutorialDone()){
            GameManager.Instance.isTutorial = true;
            tutorial.SetActive(true);
        }
    }

    void Update() {
        if(GameManager.Instance.isTutorial == true){
            for(int i=0; i<tutorialUI.Length; i++) {
                if (i == tutorialUI.Length) {
                    Time.timeScale = 1;
                    GameManager.Instance.isTutorial = false;
                    tutorial.SetActive(false);
                }
                else {
                    if (i == _tutorialIndex) {
                        GameManager.Instance.isTutorial = true;
                        tutorialUI[i].SetActive(true);
                    }
                    else {
                        tutorialUI[i].SetActive(false);
                        if(_tutorialIndex == tutorialUI.Length) {
                            GameManager.Instance.isTutorial = false;
                            tutorial.SetActive(false);
                        }
                    }
                } 
            }

            if(_tutorialIndex!=tutorialUI.Length) {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    _tutorialIndex++;
                }
                if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _tutorialIndex++;
                }
            }
            else {
                GameManager.Instance.SetTutorialDone();
                return;
            }
        }
    }
}

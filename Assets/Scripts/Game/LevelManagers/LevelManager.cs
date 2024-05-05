using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject joystick;
    public GameObject LevelEndMenu;


   public void LevelCompleted(){
        //Switch to level end UI
        LevelEndScreen(true);

   }

    //Toggles between the level UI
    //And Level End UI
   private void LevelEndScreen(bool x){

        if(true){
            LevelEndMenu.SetActive(true);
            joystick.SetActive(false);
        } else {
            LevelEndMenu.SetActive(false);
            joystick.SetActive(true);
        }
   }

   public void AdvanceLevels(){

        //Advance to next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    public void ResetLevel(){

        //Reload current scene index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelSelect(){

        //Load level select scene
        SceneManager.LoadScene(0);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    //Number of required objectives to complete level
    private int ObjectiveCount = 0;

    public LevelManager levelManager;

    //Incremenet objectives
    public void AddObjective(){
        ObjectiveCount++;
        Refresh();
    }

    //Decrement objectives
    public void RemoveObjective(){
        ObjectiveCount--;
        Refresh();
    }

    private void Refresh(){

        Debug.Log("Objectives left: " + ObjectiveCount.ToString());

        if(ObjectiveCount < 0) Debug.Log("Objective count error in OM");
        if(ObjectiveCount == 0){

            //Advance to next level
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            //Pull up level end screen
            levelManager.LevelCompleted();
        }
    }
}

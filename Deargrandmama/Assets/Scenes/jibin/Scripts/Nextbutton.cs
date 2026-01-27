using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextbutton : MonoBehaviour
{
    public void GoToFoodSelect()
    {
        SceneManager.LoadScene("FoodSelect");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodSelect : MonoBehaviour
{
    // 🐔 Chicken 버튼
    public void OnClickChicken()
    {
        Debug.Log("Chicken 버튼 클릭됨");

        // 실제 씬 이름으로 바꿔줘!
       // SceneManager.LoadScene("RecipeBook");
    }

    // 🍝 Spaghetti 버튼
    public void OnClickSpaghetti()
    {
        Debug.Log("Spaghetti 버튼 클릭됨");
    }

    // 🍗 Inasal 버튼
    public void OnClickInasal()
    {
        Debug.Log("Inasal 버튼 클릭됨");
    }
}

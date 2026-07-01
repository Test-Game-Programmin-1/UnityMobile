using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] string NextLVL;
    
    public void Win()
    {
        SceneManager.LoadScene(NextLVL);
        Debug.Log("Hai vinto");
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

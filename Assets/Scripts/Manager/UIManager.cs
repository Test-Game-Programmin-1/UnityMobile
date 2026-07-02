using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] string NextLVL;
    [SerializeField] Button UndoButton;
    [SerializeField] Button ResetButton;
    
    public void Update()
    {
        ResetButton.interactable = CommandManager.instance.Moved;
        UndoButton.interactable = CommandManager.instance.CanUndo;
    }
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

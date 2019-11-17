using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameObject canvas;
    private GameObject description;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject;
        description = GameObject.Find("Description");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadText(string text)
    {
        description.GetComponent<UnityEngine.UI.Text>().text = text;
    }

    public void ToggleCanvas(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.Confined : CursorLockMode.Locked;
        canvas.GetComponent<CanvasGroup>().alpha = state ? 1 : 0;
        canvas.GetComponent<CanvasGroup>().blocksRaycasts = !state;
        canvas.GetComponent<CanvasGroup>().interactable = state;
    }
}

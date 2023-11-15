using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    private EventSystem eventSystem;

    void Start()
    {
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var selectedButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            if (selectedButton != null)
            {
                selectedButton.onClick.Invoke();
                // BattleSystem.PlayerAttack();
            }
        }
    }
}
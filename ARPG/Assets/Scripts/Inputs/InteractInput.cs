
using UnityEngine;
using UnityEngine.UI;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen;
    [SerializeField] Slider sliderValue;
    [SerializeField] Texture2D redCursor;  // Add this line to reference the red cursor texture
    private Vector2 mousePosition;
    [HideInInspector] public IInteractObject hoveringOverObject;
    private IInteractObject previousHoveredObject;

    public static IInteractObject interactTarget;

    [SerializeField]
    private InputReader inputReader;

    private void Awake()
    {
        inputReader.OnInputMousePosition += MousePositionInput;

    }

    private void OnDisable()
    {
        inputReader.OnInputMousePosition -= MousePositionInput;
    }

    void Update()
    {
        CheckInteractablObject();
    }

    public void MousePositionInput(Vector2 mousePos)
    {
        mousePosition = mousePos;
    }
  
    private void CheckInteractablObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IInteractObject interactableObject = hit.transform.GetComponent<IInteractObject>();

            // Si el objeto actual es diferente al anterior, desactiva el contorno del anterior
            if (interactableObject != previousHoveredObject)
            {
                if (previousHoveredObject != null)
                {
                    Outline previousOutline = previousHoveredObject.outLine();
                    if (previousOutline != null)
                    {
                        previousOutline.OutlineWidth = 0;
                    }
                }

                previousHoveredObject = interactableObject;

                if (interactableObject != null)
                {
                    // Mostrar el nombre y la barra de vida del objeto
                    ShowInteractUI(interactableObject);
                    interactTarget = interactableObject;

                    // Activar el contorno del objeto actual
                    Outline currentOutline = interactableObject.outLine();
                    if (currentOutline != null)
                    {
                        currentOutline.OutlineWidth = 1.5f;
                    }

                    // Cambiar el cursor a rojo
                    Cursor.SetCursor(redCursor, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    // Si no hay objeto interactuable, ocultar la barra de vida
                    interactTarget = null;
                    HideInteractUI();

                    // Restaurar el cursor predeterminado
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
        }
        else
        {
            // Si no hay objeto interactuable, ocultar la barra de vida y desactivar el contorno del objeto anterior
            if (previousHoveredObject != null)
            {
                HideInteractUI();
                previousHoveredObject.outLine().OutlineWidth = 0;
                previousHoveredObject = null;
            }

            // Restaurar el cursor predeterminado
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void HideInteractUI()
    {
        sliderValue.gameObject.SetActive(false);
        textOnScreen.SetText("");
    }

    private void ShowInteractUI(IInteractObject interactableObject)
    {
        textOnScreen.SetText(interactableObject.ObjectName());
        sliderValue.gameObject.SetActive(true);
        UpdateUI(interactableObject);
    }

    public void UpdateUI(IInteractObject interactableObject)
    {
        sliderValue.maxValue = interactableObject.Health()[1];
        sliderValue.value = interactableObject.Health()[0];
    }
}

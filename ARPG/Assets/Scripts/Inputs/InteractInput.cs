using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractInput : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI textOnScreen;
    [SerializeField] Slider sliderValue;
    Vector2 mousePosition;
    [HideInInspector] public IInteractObject hoveringOverObject;
    private IInteractObject previousHoveredObject;
    void Update()
    {
        CheckInteractablObject();

    }
    public void MousePositionInput(InputAction.CallbackContext callbackContext)
    {
        mousePosition = callbackContext.ReadValue<Vector2>();
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

                    // Activar el contorno del objeto actual
                    Outline currentOutline = interactableObject.outLine();
                    if (currentOutline != null)
                    {
                        currentOutline.OutlineWidth = 1.5f;
                    }
                }
                else
                {
                    // Si no hay objeto interactuable, ocultar la barra de vida
                    HideInteractUI();
                }
            }
        }
        else
        {
            // Si no hay objeto interactuable, ocultar la barra de vida y desactivar el contorno del objeto anterior
            if (previousHoveredObject != null)
            {
                HideInteractUI();
                previousHoveredObject.outLine().OutlineWidth=0;
                previousHoveredObject = null;
                
            }
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

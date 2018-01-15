using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
    private bool isSelecting = false;
    private Vector3 mousePosition1;
    private List<Transform> selectedObjects = new List<Transform>();

    /// <summary>
    /// Selection system.
    /// </summary>
    private void unitSelectionSystem()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                // Disable and remove selection.
                if (selectableObject.isSelected())
                {
                    Renderer rend = selectableObject.GetComponent<Renderer>();
                    rend.material.color = Color.white;
                    selectableObject.setSelection(false);
                }
            }
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        { 
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (this.isInBound(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject.transform);
                }
            }

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (this.isInBound(selectableObject.gameObject))
                {

                    // Change the color of the selected object.
                    if (!selectableObject.isSelected())
                    {
                        selectableObject.setSelection(true);
                        Renderer rend = selectableObject.GetComponent<Renderer>();
                        rend.material.color = Color.green;
                    }
                }
                else
                {
                    // Destroy all the selected object.
                    if (selectableObject.isSelected())
                    {
                        Renderer rend = selectableObject.GetComponent<Renderer>();
                        rend.material.color = Color.white;
                        selectableObject.setSelection(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Check if gameobject is within selection bound
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns>boolean</returns>
    private bool isInBound(GameObject gameObject)
    {
        if( !isSelecting )
            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
    }

    /// <summary>
    /// Get the list of selected objects
    /// </summary>
    /// <returns>A list of tranform object</returns>
    public List<Transform> getSelectedObjects()
    {
        List<Transform> result = selectedObjects;
        return result;
    }

    /// <summary>
    /// Clear all selected objects.
    /// </summary>
    public void clearSelections()
    {
        this.selectedObjects.Clear();
    }

    /// <summary>
    /// Draw the box.
    /// </summary>
    void OnGUI()
    {
        if( isSelecting )
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition );
            Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
            Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
        }
    }

    void Update()
    {
        this.unitSelectionSystem();
    }
}
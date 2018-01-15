using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class SelectableUnitComponent : MonoBehaviour
{
    [SerializeField] private bool selection = false;

    /// <summary>
    /// Set the selection.
    /// </summary>
    /// <param name="value">boolean</param>
    public void setSelection(bool value)
    {
        this.selection = value;
    }

    /// <summary>
    /// Check if it is selected
    /// </summary>
    /// <returns>boolean</returns>
    public bool isSelected()
    {
        bool result = selection;
        return result;
    }
}
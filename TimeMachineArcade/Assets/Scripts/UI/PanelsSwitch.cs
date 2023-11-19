using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsSwitch
{
    private Stack<GameObject> _openedPanels;
    private GameObject _activePanel;

    public PanelsSwitch(GameObject startPanel)
    {
        _openedPanels = new Stack<GameObject>();
        _openedPanels.Push(startPanel);

        SetPanelActive(startPanel);
    }

    public void OpenPanel(GameObject panel)
    {
        _openedPanels.Push(_activePanel);
        _activePanel.SetActive(false);

        SetPanelActive(panel);
    }

    public void Back()
    {
        _activePanel.SetActive(false);
        GameObject previousPanel = _openedPanels.Pop();
        SetPanelActive(previousPanel);
    }

    private void SetPanelActive(GameObject panel)
    {
        _activePanel = panel;
        _activePanel.SetActive(true);
    }
}
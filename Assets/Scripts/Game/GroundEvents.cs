using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace TowerDefence
{
    public class GroundEvents : MonoBehaviour
    {
        GameManager gameManager;

        BuildNode[] buildNodes;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            buildNodes = FindObjectsOfType<BuildNode>();
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
           
            if (Utils.Utilities.IsPointerOverUIObject())
                return;

            gameManager.CloseAllWindowsButton();

            GameItemManager.activeItem = false;

            foreach(BuildNode node in buildNodes)
            {
                if (node.towerScript != null)
                    node.towerScript.HideRangeUI();
                node.rend.material.color = node.startColor;
                node.isSelected = false;
                gameManager.selectedBuildNode = null;
            }
        }
    }
}

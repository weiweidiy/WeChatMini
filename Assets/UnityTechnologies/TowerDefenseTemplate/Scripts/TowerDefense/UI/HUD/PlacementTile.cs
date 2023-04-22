using TMPro;
using UnityEngine;

namespace TowerDefense.UI.HUD
{
    /// <summary>
    /// States the placement tile can be in
    /// </summary>
    public enum PlacementTileState
    {
        Filled,
        Empty
    }

    /// <summary>
    /// Simple class to illustrate tile placement locations
    /// </summary>
    public class PlacementTile : MonoBehaviour
    {
        /// <summary>
        /// Material to use when this tile is empty
        /// </summary>
        public Material emptyMaterial;
        /// <summary>
        /// Material to use when this tile is filled
        /// </summary>
        public Material filledMaterial;

        /// <summary>
        /// The renderer whose material we're changing
        /// </summary>
        public Renderer tileRenderer;


        public TextMeshProUGUI txtNumber;

        public int TowerNumber { get; set; }

        public PlacementTileState tileState;

        /// <summary>
        /// Update the state of this placement tile
        /// </summary>
        public void SetState(PlacementTileState newState)
        {
            tileState = newState;
            switch (newState)
            {
                case PlacementTileState.Filled:
                    if (tileRenderer != null && filledMaterial != null)
                    {
                        tileRenderer.sharedMaterial = filledMaterial;
                        if (txtNumber != null)
                            txtNumber.gameObject.SetActive(false);
                    }
                    break;
                case PlacementTileState.Empty:
                    if (tileRenderer != null && emptyMaterial != null)
                    {
                        tileRenderer.sharedMaterial = emptyMaterial;
                        if (txtNumber != null)
                        {
                            txtNumber.gameObject.SetActive(true);
                            txtNumber.text = TowerNumber.ToString();
                        }


                    }
                    break;
            }
        }
    }
}
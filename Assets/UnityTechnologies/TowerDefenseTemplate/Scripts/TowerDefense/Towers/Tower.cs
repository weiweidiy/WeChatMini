using ActionGameFramework.Health;
using Core.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using TowerDefense.Affectors;
using TowerDefense.Level;
using TowerDefense.Towers.Placement;
using TowerDefense.UI.HUD;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace TowerDefense.Towers
{
    /// <summary>
    /// Common functionality for all types of towers
    /// </summary>
    public class Tower : Targetable
    {
        /// <summary>
        /// The tower levels associated with this tower
        /// </summary>
        public TowerLevel[] levels;

        /// <summary>
        /// A generalised name common to a levels
        /// </summary>
        public string towerName;

        /// <summary>
        /// The size of the tower's footprint
        /// </summary>
        public IntVector2 dimensions;

        /// <summary>
        /// The physics mask the tower searches on
        /// </summary>
        public LayerMask enemyLayerMask;

        /// <summary>
        /// The current level of the tower
        /// </summary>
        public int currentLevel { get; protected set; }

        public string userId;
        public TextMeshProUGUI txtName;
        public Image headIcon;

        /// <summary>
        /// Reference to the data of the current level
        /// </summary>
        public TowerLevel currentTowerLevel { get; protected set; }

        /// <summary>
        /// Gets whether the tower can level up anymore
        /// </summary>
        public bool isAtMaxLevel
        {
            get { return currentLevel == levels.Length - 1; }
        }

        /// <summary>
        /// Gets the first level tower ghost prefab
        /// </summary>
        public TowerPlacementGhost towerGhostPrefab
        {
            get { return levels[currentLevel].towerGhostPrefab; }
        }

        /// <summary>
        /// Gets the grid position for this tower on the <see cref="placementArea"/>
        /// </summary>
        public IntVector2 gridPosition { get; private set; }

        /// <summary>
        /// The placement area we've been built on
        /// </summary>
        public IPlacementArea placementArea { get; private set; }

        /// <summary>
        /// The purchase cost of the tower
        /// </summary>
        public int purchaseCost
        {
            get { return levels[0].cost; }
        }

        /// <summary>
        /// The event that fires off when a player deletes a tower
        /// </summary>
        public Action towerDeleted;

        /// <summary>
        /// The event that fires off when a tower has been destroyed
        /// </summary>
        public Action towerDestroyed;

        public void Speedup(float k)
        {
            var attack = GetComponentInChildren<AttackAffector>();

            if (attack.fireRateExtention < k)
                attack.fireRateExtention = k;

        }

        /// <summary>
        /// Provide the tower with data to initialize with
        /// </summary>
        /// <param name="targetArea">The placement area configuration</param>
        /// <param name="destination">The destination position</param>
        public virtual void Initialize(IPlacementArea targetArea, IntVector2 destination, string id = null, string name = null, string headIcon = null)
        {
            placementArea = targetArea;
            gridPosition = destination;

            if (targetArea != null)
            {
                transform.position = placementArea.GridToWorld(destination, dimensions);
                transform.rotation = placementArea.transform.rotation;
                targetArea.Occupy(destination, dimensions);
            }

            SetLevel(0);
            if (LevelManager.instanceExists)
            {
                LevelManager.instance.levelStateChanged += OnLevelStateChanged;
            }

            this.userId = id;
            this.txtName.text = name;
            DownSprite(headIcon);

        }

        async Task DownSprite(string url)
        {
            UnityWebRequest wr = new UnityWebRequest(url);
            DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
            wr.downloadHandler = texDl;
            await wr.SendWebRequest();
            int width = 100;
            int high = 100;
            if (!wr.isNetworkError)
            {
                Texture2D tex = new Texture2D(width, high);
                tex = texDl.texture;
                //Save2LocalPath(tex);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                headIcon.sprite = sprite;
            }
            else
            {
                Debug.LogError("ÍøÂç´íÎó " + url);
            }
        }

        private void Save2LocalPath(Texture2D texture)
        {
            byte[] bytes = texture.EncodeToPNG();
            string savePath = "D:/";
            try
            {
                File.WriteAllBytes(string.Format("{0}/{1}.jpeg", savePath, "icon"), bytes);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }


        /// <summary>
        /// Provides information on the cost to upgrade
        /// </summary>
        /// <returns>Returns -1 if the towers is already at max level, other returns the cost to upgrade</returns>
        public int GetCostForNextLevel()
        {
            if (isAtMaxLevel)
            {
                return -1;
            }
            return levels[currentLevel + 1].cost;
        }

        /// <summary>
        /// Kills this tower
        /// </summary>
        public void KillTower()
        {
            // Invoke base kill method
            Kill();
        }

        /// <summary>
        /// Provides the value recived for selling this tower
        /// </summary>
        /// <returns>A sell value of the tower</returns>
        public int GetSellLevel()
        {
            return GetSellLevel(currentLevel);
        }

        /// <summary>
        /// Provides the value recived for selling this tower of a particular level
        /// </summary>
        /// <param name="level">Level of tower</param>
        /// <returns>A sell value of the tower</returns>
        public int GetSellLevel(int level)
        {
            // sell for full price if waves haven't started yet
            if (LevelManager.instance.levelState == LevelState.Building)
            {
                int cost = 0;
                for (int i = 0; i <= level; i++)
                {
                    cost += levels[i].cost;
                }

                return cost;
            }
            return levels[currentLevel].sell;
        }

        /// <summary>
        /// Used to (try to) upgrade the tower data
        /// </summary>
        public virtual bool UpgradeTower()
        {
            if (isAtMaxLevel)
            {
                return false;
            }
            SetLevel(currentLevel + 1);
            return true;
        }

        /// <summary>
        /// A method for downgrading tower
        /// </summary>
        /// <returns>
        /// <value>false</value> if tower is at lowest level
        /// </returns>
        public virtual bool DowngradeTower()
        {
            if (currentLevel == 0)
            {
                return false;
            }
            SetLevel(currentLevel - 1);
            return true;
        }

        /// <summary>
        /// Used to set the tower to any valid level
        /// </summary>
        /// <param name="level">
        /// The level to upgrade the tower to
        /// </param>
        /// <returns>
        /// True if successful
        /// </returns>
        public virtual bool UpgradeTowerToLevel(int level)
        {
            if (level < 0 || isAtMaxLevel || level >= levels.Length)
            {
                return false;
            }
            SetLevel(level);
            return true;
        }

        public void Sell()
        {
            Remove();
        }

        /// <summary>
        /// Removes tower from placement area and destroys it
        /// </summary>
        public override void Remove()
        {
            base.Remove();

            placementArea.Clear(gridPosition, dimensions);
            Destroy(gameObject);
        }

        /// <summary>
        /// unsubsribe when necessary
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (LevelManager.instanceExists)
            {
                LevelManager.instance.levelStateChanged += OnLevelStateChanged;
            }
        }

        /// <summary>
        /// Cache and update oftenly used data
        /// </summary>
        protected void SetLevel(int level)
        {
            if (level < 0 || level >= levels.Length)
            {
                return;
            }
            currentLevel = level;
            if (currentTowerLevel != null)
            {
                Destroy(currentTowerLevel.gameObject);
            }

            // instantiate the visual representation
            currentTowerLevel = Instantiate(levels[currentLevel], transform);

            // initialize TowerLevel
            currentTowerLevel.Initialize(this, enemyLayerMask, configuration.alignmentProvider);

            // health data
            ScaleHealth();

            // disable affectors
            LevelState levelState = LevelManager.instance.levelState;
            bool initialise = levelState == LevelState.AllEnemiesSpawned || levelState == LevelState.SpawningEnemies;
            currentTowerLevel.SetAffectorState(initialise);
        }

        /// <summary>
        /// Scales the health based on the previous health
        /// Requires override when the rules for scaling health on upgrade changes
        /// </summary>
        protected virtual void ScaleHealth()
        {
            configuration.SetMaxHealth(currentTowerLevel.maxHealth);

            if (currentLevel == 0)
            {
                configuration.SetHealth(currentTowerLevel.maxHealth);
            }
            else
            {
                int currentHealth = Mathf.FloorToInt(configuration.normalisedHealth * currentTowerLevel.maxHealth);
                configuration.SetHealth(currentHealth);
            }
        }

        /// <summary>
        /// Intiailises affectors based on the level state
        /// </summary>
        protected virtual void OnLevelStateChanged(LevelState previous, LevelState current)
        {
            bool initialise = current == LevelState.AllEnemiesSpawned || current == LevelState.SpawningEnemies;
            currentTowerLevel.SetAffectorState(initialise);
        }
    }
}
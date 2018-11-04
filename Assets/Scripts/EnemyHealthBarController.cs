using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour {

    private List<GameObject> enemies;
    Dictionary<GameObject, GameObject> enemyHealthBarMap;

    private void Start()
    {
        enemies = new List<GameObject>();
        enemyHealthBarMap = new Dictionary<GameObject, GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    // Update is called once per frame
    void Update () {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        // Instantiate any missing HealthBars
        foreach (GameObject enemyObject in enemies)
        {
            if (!enemyHealthBarMap.ContainsKey(enemyObject) && enemyObject != null)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                GameObject enemyHealthBarObject = enemy.CreateHealthBar();
                enemyHealthBarObject.transform.SetParent(this.transform);
                enemyHealthBarMap.Add(enemyObject, enemyHealthBarObject);
            }
        }

        // Update Health Bars visually
        foreach (KeyValuePair<GameObject, GameObject> entry in enemyHealthBarMap)
        {
            GameObject enemyObject = entry.Key;
            GameObject healthBarObject = entry.Value;
            if (enemyObject != null)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                
                // Update bar position and value
                Slider healthSlider = healthBarObject.GetComponent<Slider>();
                healthBarObject.transform.position = enemy.GetWorldToScreenPoint();
                healthBarObject.transform.position += new Vector3(0, 30, 0);
                healthSlider.value = enemy.getHealth();
            } else
            {
                Destroy(healthBarObject);
                enemyHealthBarMap.Remove(enemyObject);
                break;
            }
        }
    }
}

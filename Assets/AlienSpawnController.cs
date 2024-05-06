using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class AlienSpawnController : MonoBehaviour
{
    public int initialAliensPerWave = 5;
    public int currentAlienPerWave;

    public float spawnDelay = 0.5f;
    
    public int curretWave = 0;
    public float waveCooldown = 10.0f;

    public bool inCooldown;
    public float cooldownCounter = 0;

    public List<Enemy> currentAliensAlive;

    public GameObject alienPrefab;

    public TextMeshProUGUI WaveOverUI;
    public TextMeshProUGUI cooldownCounterUI;

    public TextMeshProUGUI curretWaveUI;

    private void Start() 
    {
        currentAlienPerWave = initialAliensPerWave;

        GlobalReferences.Instance.waveNumber = curretWave;

        StartNextWave();
    }

    private void StartNextWave()
    {
        currentAliensAlive.Clear();

        curretWave++;

        GlobalReferences.Instance.waveNumber = curretWave;

        curretWaveUI.text = "Wave: " + curretWave.ToString();

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentAlienPerWave; i++)
        {

            Vector3 spawnOffset = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;

            //instantaite alien
            var Alien = Instantiate(alienPrefab, spawnPosition, Quaternion.identity);

            Enemy enemyScript = Alien.GetComponent<Enemy>();

            currentAliensAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update() 
    {
        List<Enemy> alienToRemove = new List<Enemy>();
        foreach (Enemy alien in currentAliensAlive)
        {
            if (alien.isDead)
            {
                alienToRemove.Add(alien);
            }
        } 

        foreach (Enemy alien in alienToRemove)
        {
            currentAliensAlive.Remove(alien);
        } 

        alienToRemove.Clear();

        if (currentAliensAlive.Count == 0 && inCooldown == false)
        {

            StartCoroutine(WaveCooldown());
        }

        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }


        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        WaveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        WaveOverUI.gameObject.SetActive(false);

        currentAlienPerWave *= 2;

        StartNextWave();
    }

}

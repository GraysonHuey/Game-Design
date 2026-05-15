using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ScoreZone scoreZonePrefab;
    public GameObject[] characterPrefabs;

    public float spawnRate = 1f;
    [Range(0f, 1f)] public float characterSpawnChance = 0.3f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        Instantiate(scoreZonePrefab, transform.position, Quaternion.identity);

        if (characterPrefabs.Length == 0) return;
        if (Random.value > characterSpawnChance) return;

        GameObject prefab = GetWeightedCharacter();
        if (prefab == null) return;

        float y = Random.Range(-2.75f, 4.6f);
        Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);

        Instantiate(prefab, pos, Quaternion.identity);
    }

    private GameObject GetWeightedCharacter()
    {
        float total = 0f;

        foreach (var prefab in characterPrefabs)
        {
            if (prefab == null) continue;

            var data = prefab.GetComponent<CharacterBase>();
            total += data != null ? data.spawnWeight : 1f;
        }

        float roll = Random.Range(0f, total);

        foreach (var prefab in characterPrefabs)
        {
            if (prefab == null) continue;

            var data = prefab.GetComponent<CharacterBase>();
            float w = data != null ? data.spawnWeight : 1f;

            roll -= w;

            if (roll <= 0f)
                return prefab;
        }

        return characterPrefabs[0];
    }
}
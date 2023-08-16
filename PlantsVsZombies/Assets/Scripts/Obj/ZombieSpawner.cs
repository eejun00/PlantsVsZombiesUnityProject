using System.Collections;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 좀비 프리팹
    public GameObject flagZombiePrefab; // 깃발 좀비 프리팹

    //아래의 두개의 리스트는 순서와 갯수가 맞아야함
    public GameObject[] otherZombieList;
    public int[] otherZombieCount;
    
    public float spawnInterval = 5f; // 좀비 생성 간격
    public float[] allowedYPositions; // 좀비 생성 허용 y좌표 배열
    public int zombieCount = 5;
    public int waveCount = 3;

    private bool coroutineFlag = false;


    private void Start()
    {
        if(GameManager.instance.stageOneNum < 3)
        {
            waveCount = 0;
        }
        // spawnInterval마다 SpawnZombie 함수를 호출
        GameManager.instance.zombieDeathCount = zombieCount + waveCount;
    }

    private void Update()
    {
        if (coroutineFlag == false && GameManager.instance.stagePlaying)
        {
            coroutineFlag = true;
            StartCoroutine(SpawnZombieRoutine());
        }
        if (waveCount <= 0 && GameManager.instance.isStageClear == false)
        {
            GameManager.instance.isWave = false;
            GameManager.instance.isStageClear = true;
        }
    }

    IEnumerator SpawnZombieRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (zombieCount > 0)
        {
            // 랜덤한 인덱스 선택
            int randomIndex = Random.Range(0, allowedYPositions.Length);

            // 선택된 인덱스에 해당하는 y좌표 가져오기
            float spawnYPosition = allowedYPositions[randomIndex];

            // 좀비 생성 위치 설정
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

            // 좀비 프리팹을 생성 위치에 생성
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            zombieCount -= 1;
            // 다음 생성 대기
            yield return new WaitForSeconds(spawnInterval);
        }

        
        if (waveCount > 0)
        {
            yield return new WaitForSeconds(2.0f);

            GameManager.instance.isWave = true;

            yield return new WaitForSeconds(2.0f);

            // 깃발 좀비 프리팹을 생성 위치에 생성
            Instantiate(flagZombiePrefab, new Vector3(transform.position.x, -1.3f, transform.position.z), Quaternion.identity);
            waveCount -= 1;
            // 다음 생성 대기
            yield return new WaitForSeconds(2.0f);

            while (waveCount > 0)
            {
                // 랜덤한 인덱스 선택
                int randomIndex = Random.Range(0, allowedYPositions.Length);

                // 선택된 인덱스에 해당하는 y좌표 가져오기
                float spawnYPosition = allowedYPositions[randomIndex];

                // 좀비 생성 위치 설정
                Vector3 spawnPosition = new Vector3(transform.position.x, spawnYPosition, transform.position.z);

                // 좀비 프리팹을 생성 위치에 생성
                Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                waveCount -= 1;
                // 다음 생성 대기
                yield return new WaitForSeconds(2.0f);
            }
        }
    }
}
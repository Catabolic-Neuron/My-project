using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPreFab;
    public float spawnRate = 2.0f;
    public int spwanAmount = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryVariance = 15.0f;
    

    private void Start(){
        InvokeRepeating(nameof(spawn),this.spawnRate,this.spawnRate);
    }

    private void spawn(){
        for(int i=0;i<this.spwanAmount;i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            float variance = Random.Range(-trajectoryVariance,trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance,Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPreFab,spawnPoint,rotation);
            asteroid.size = Random.Range(asteroid.minsize,asteroid.maxsize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}

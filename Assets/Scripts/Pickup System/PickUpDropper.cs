using UnityEngine;

public class PickUpDropper : MonoBehaviour
{
    ///<summmar>
    ///
    /// This script will be attached to obstacle prefab objects
    /// It listens to OnDestroyEvent of the obstacle
    /// On destroy event flag, before the game object gets destroyed, it will run a 
    /// random pick up dropper
    /// 
    /// </summmar>
    /// 


    public Obstacle obstacle;


    private void Start()
    {
        if(obstacle != null)
        obstacle.OnDestroyEvent += OnObstacleDestroyEventHandler;
    }

    private void OnDisable()
    {
        obstacle.OnDestroyEvent -= OnObstacleDestroyEventHandler;
    }

    private void OnObstacleDestroyEventHandler()
    {
        Debug.Log("Trigger drop rate of the obstacle");

        //For now dropping at every object destroy
        int index = (int)Random.Range(0, 100);

        //Currently 50% chance of a pick up item drop

        if(index <= 50) return;

        PickUpSystem.instance.CreateDrops(transform.position);
      
    }
}

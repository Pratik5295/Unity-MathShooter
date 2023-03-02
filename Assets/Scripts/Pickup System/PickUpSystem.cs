using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    ///<summary>
    ///
    /// This system acts like a manager in game.
    /// It has references to all the pick ups available
    /// It is responsible for spawning the pickups
    /// 
    /// TODO: Object Pooling
    /// </summary>
    /// 

    public static PickUpSystem instance;

    public Pickup[] pickUpPrefabs;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateDrops(Vector3 dropPosition)
    {
        //Randomly select one of the prefabs to instantiate

        int index = (int)Random.Range(0, pickUpPrefabs.Length);  

        if(index == pickUpPrefabs.Length)
        {
            Debug.Log("Max length reached, spawning lesser");
            index = 0;
        }

        Instantiate(pickUpPrefabs[index],dropPosition,Quaternion.identity);
    }
}

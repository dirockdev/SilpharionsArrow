using UnityEngine;

public class RewardItem : MonoBehaviour
{
    
   
    public int experience;
    private byte speed = 5;

    private bool inRange = false;
    private int range = 10;
    [SerializeField] private LayerMask playermask;
    private Vector3 velocity = Vector3.zero;
    public void StartFollowing()
    {
        inRange = Physics.CheckSphere(transform.position, range, playermask);

    }
    private void Update()
    {
        if (inRange)
        {
            GetComponent<Animator>().SetBool("Follow", true);
            transform.position = Vector3.SmoothDamp(transform.position, InstancePlayer.instance.transform.position, ref velocity, Time.deltaTime * speed);
        }
    }
}

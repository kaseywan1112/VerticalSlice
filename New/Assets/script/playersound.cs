using UnityEngine;
using UnityEngine.AI;

public class PlayerSoundController : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent != null && AudioManager.Instance != null)
        {
            // 如果 NavMesh 的速度大于 0.1，说明玩家正在走！
            bool isWalking = agent.velocity.magnitude > 0.1f;
            if (isWalking)
            {
                // 只要在走，就呼叫 AudioManager
                // AudioManager 内部会判断当前声音是否播完了，播完了才放下一个
                AudioManager.Instance.PlayFootstep();
            }
        }
    }
}
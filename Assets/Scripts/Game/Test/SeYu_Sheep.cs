public class SeYu_Sheep : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new SeYu_PatrolState();     //³õÊ¼»¯Ñ²Âß×´Ì¬
        chaseState = new SeYu_ChaseState();       //³õÊ¼»¯×·»÷×´Ì¬
    }
}

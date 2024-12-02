namespace ConditionSystem
{
    public class KillEnemyCondition: ConditionBase
    {
        public KillEnemyCondition(){}
        public KillEnemyCondition(int id) : base(id)
    	{
    		
    	}

        public override void Init()
        {
            base.Init();
            maxValue = ParamList[0];
        }

        protected override void OnConditionReached()
        {
            base.OnConditionReached();
            Console.WriteLine("Kill Enemy Condition Reached!");
        }
    }
}


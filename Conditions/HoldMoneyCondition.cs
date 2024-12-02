namespace ConditionSystem
{
    public class HoldMoneyCondition: ConditionBase
    {
        public HoldMoneyCondition(){}
        public HoldMoneyCondition(int id) : base(id)
    	{
    		
    	}

        public override void UpdateCondition()
        {
            base.UpdateCondition();
            CurrentValue = Player.Money;
        }

        public override void Init()
        {
            base.Init();
            maxValue = ParamList[0];
        }

        protected override void OnConditionReached()
        {
            base.OnConditionReached();
            Console.WriteLine("HoldMoney Condition Reached!");
        }
    }
}


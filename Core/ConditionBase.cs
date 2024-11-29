namespace ConditionSystem
{
    public enum ConditionStatus
    {
        Ing,
        Succeed,
        Failed,
    }
    public abstract class ConditionBase :  IComparable<ConditionBase>
    {
        /// <summary>
        /// 唯一ID 用来获取条件,从1自增,不减
        /// </summary>
        public int Id;
        /// <summary>
        /// 多个条件同时触发时的先后顺序
        /// </summary>
        public int Order;
        /// <summary>
        /// 条件当前状态
        /// </summary>
        public ConditionStatus Status;
        /// <summary>
        /// 条件需要的参数列表
        /// </summary>
        public List<int> ParamList;
        /// <summary>
        /// 当前值
        /// </summary>
        protected int currentValue;
        /// <summary>
        /// 达成条件所需要的最大值
        /// </summary>
        protected int maxValue;
        
        /// <summary>
        /// 是否达成条件
        /// </summary>
        public bool IsReachCondition => currentValue >= maxValue;
    
        public ConditionBase() { }
        public ConditionBase(int id)
        {
            this.Id = id;
        }
        public virtual int CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (Status != ConditionStatus.Ing)
                    return;
                
                currentValue = value > maxValue ? maxValue : value;
                
                if (IsReachCondition)
                {
                    Status = ConditionStatus.Succeed;
                }
            }
        }
    
        public virtual void UpdateCondition()
        {

        }

        public virtual void UpdateCondition(int param)
        {
        
        }
        public virtual void Init()
        {

        }
        public virtual string[] GetDes()
        {
            return new string[1] { CurrentValue.ToString() };
        }

        public int CompareTo(ConditionBase other)
        {
            return Order.CompareTo(other.Order);
        }
    }
}


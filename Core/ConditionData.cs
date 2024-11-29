namespace ConditionSystem
{
    public class ConditionData
    {
        #region Singleton

        private static ConditionData _i;
        public static ConditionData i
        {
            get
            {
                if (_i == null)
                {
                    _i = new ConditionData();
                }

                return _i;
            }
        }

        #endregion
        
        private int id = 1;

        public Dictionary<ConditionType, List<ConditionBase>> Conditions = new Dictionary<ConditionType, List<ConditionBase>>();

        public int GetId()
        {
            return id++;
        }

        public ConditionBase GetCondition(int id)
        {
            if (id == 0)
                return null;
            foreach (var item in Conditions)
            {
                var condition = item.Value.Find(condition => condition.Id == id);
                if (condition != null)
                    return condition;
            }
            return null;
        }

        public ConditionBase AddNewCondition(ConditionType type)
        {
            var newCondition = ConditionFactory.GetCondition(type);
            if (Conditions.TryGetValue(type, out var conditionList))
            {
                conditionList.Add(newCondition);
                conditionList.Sort();
            }
            else
            {
                conditionList = new List<ConditionBase>() { newCondition };
                Conditions.Add(type, conditionList);
            }
        
            return newCondition;
        }

        public void RemoveCondition(int id)
        {
            if (id == 0)
                return;
            foreach (var item in Conditions)
            {
                foreach (var conditionBase in item.Value)
                {
                    if (conditionBase.Id == id)
                    {
                        item.Value.Remove(conditionBase);
                        return;
                    }
                }
            }
        }
        
        public void AddValue(ConditionType type, int value = 1)
        {
            if (!Conditions.TryGetValue(type, out var conditionList))
                return;
            conditionList.ForEach((condition) => { condition.CurrentValue += value; });
        }
        
        public void UpdateCondition(ConditionType type)
        {
            if (!Conditions.TryGetValue(type, out var conditionList))
                return;
            conditionList.ForEach((condition) => { condition.UpdateCondition(); });
        }

        public void UpdateCondition(ConditionType type, int param)
        {
            if (!Conditions.TryGetValue(type, out var conditionList))
                return;
            conditionList.ForEach(condition => condition.UpdateCondition(param));
        }
    }
}
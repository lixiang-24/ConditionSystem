//此为自动生成的文件,不要手动修改该文件
//如果你需要的话请修改FactoryTemplate.txt或FileGenerator.cs
//This is auto GenerateFile,Please DO NOT EDIT this!   
//Please EDIT FactoryTemplate.txt or FileGenerator.cs, if you need!

namespace ConditionSystem
{
    public static class ConditionFactory
    {
    	public static ConditionBase GetCondition(ConditionType type)
        {
    		int id = ConditionData.i.GetId();
            switch (type)
            {
				case ConditionType.KillEnemy:
					return new KillEnemyCondition(id);
				case ConditionType.HoldMoney:
					return new HoldMoneyCondition(id);
				default:
					return null;
            }
        }	
    }
}

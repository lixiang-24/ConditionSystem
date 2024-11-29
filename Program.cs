using ConditionSystem;

Console.WriteLine("Input \"kill\" to kill enemy.");
Console.WriteLine("Input \"money 200\" to add 200 money");
Console.WriteLine("Input \"failQuest 2\" to change quest2 status to failed");
//FileGenerator.GenerateFiles();

Quest quest1 = new Quest();
quest1.QuestName = "KillEnemy";
quest1.QuestDes = "Kill 5 Enemy : {0}/5";
var condition1 = ConditionData.i.AddNewCondition(ConditionType.KillEnemy);
quest1.condition = condition1;
condition1.ParamList = new List<int>(){5};
condition1.Init();

Quest quest2 = new Quest();
quest2.QuestName = "HoldMoney";
quest2.QuestDes = "Hold 2000 Money : {0}/2000";
var condition2 = ConditionData.i.AddNewCondition(ConditionType.HoldMoney);
quest2.condition = condition2;
condition2.ParamList = new List<int>(){2000};
condition2.Init();

while (true)
{
    quest1.Print();
    quest2.Print();
    string input = Console.ReadLine();
    if (input == "quit")
    {
        break;
    }
    if (input == "kill")
    {
        ConditionData.i.AddValue(ConditionType.KillEnemy);
    }

    if (input.Contains("money"))
    {
        var moneyInfo = input.Split(' ');
        Player.Money += int.Parse(moneyInfo[1]);
        ConditionData.i.UpdateCondition(ConditionType.HoldMoney);
    }
    if (input.Contains("failQuest"))
    {
        var moneyInfo = input.Split(' ');
        int questId = int.Parse(moneyInfo[1]);
        if (questId == 1)
        {
            quest1.QuestFailed();
        }
        else if(questId == 2)
        {
            quest2.QuestFailed();
        }
    }
    quest1.Print();
    quest2.Print();
}

class Player
{
    public static int Money;
}
public class Quest
{
    public string QuestName;
    public string QuestDes;
    public ConditionBase condition;

    public void Print()
    {
        Console.WriteLine($"Quest Name: {QuestName} \t QuestDes: {GetDes()}");
    }

    private string GetDes()
    {
        return string.Format(QuestDes, condition.GetDes());
    }

    public void QuestFailed()
    {
        condition.Status = ConditionStatus.Failed;
    }
}

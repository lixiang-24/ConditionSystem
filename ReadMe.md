# ConditionSystem

基于C#编写的一款方便拓展的条件系统，通过计数来达成条件，可以用于搭建游戏中的任务/成就系统。

---
### 条件系统可以完成什么计数？

例如：
- 击杀5名敌人
- 持有2000金币
- 使用Id为001的物品5次
---
# 如何创建和使用条件

**以`Program.cs`为例**

## 1、创建条件脚本

你需要做的只是在在`ConditionType`中手动新增一个枚举
  ```csharp
public enum ConditionType
{
        KillEnemy,
}
  ```

然后调用`FileGenerator.GenerateFiles`方法

`FileGenerator`会自动生成一个继承自`ConditionBase`的`XXXCondition`脚本

```csharp
public class KillEnemyCondition: ConditionBase
{
    public KillEnemyCondition(){}
    public KillEnemyCondition(int id) : base(id){}
}
```

<details>

<summary>关于生成路径</summary>

`FileGenerator`默认会将脚本生成至Conditions文件夹下，如果你有其他需求，可以更改`FileGenerator`中的路径
```csharp
 public static class FileGenerator
 {
        private static string conditionTemplateFilePath = "Core/ConditionTemplate.txt";
        private static string factoryTemplateFilePath = "Core/FactoryTemplate.txt";
        private static string factoryFilePath = "Core/ConditionFactory.cs";
        private static string outputConditionFilePath = "Conditions";
 }
```
</details>

## 2、创建条件实例

你可能需要在创建任务时生成条件实例。调用`ConditionData.i.AddNewCondition`来生成一个条件实例
```csharp
var condition1 = ConditionData.i.AddNewCondition(ConditionType.KillEnemy);
quest1.condition = condition1;
condition1.ParamList = new List<int>(){5};
condition1.Init();
```
拿到Condition实例后可以设置该条件类型所需要的参数列表,这里的5为需要的最大杀敌数量，并调用Init方法设置计数最大值

在`XXXCondition`脚本中重写`Init`方法给计数最大值赋值
```csharp
public override void Init()
{
    base.Init();
    maxValue = ParamList[0];
}
```

## 3、条件计数

条件计数有两种不同方法
- 使用`ConditionData.i.AddValue`直接增加计数值 *(默认+1，可以传参)*
  - 当计数达到最大值时会**更新条件状态**并触发`OnConditionReached`方法
- 使用`ConditionData.i.UpdateCondition`更新条件状态
  - 相应的会触发`XXXCondition.UpdateCondition`方法，可以在`XXXCondition.UpdateCondition`方法中更新`CurrentValue`来刷新计数值
  ```csharp
  public override void UpdateCondition()
  {
      base.UpdateCondition();
      CurrentValue = Player.Money;
  }
  ```
  - 需要注意`UpdateCondition`具有两个重载方法，无参的和一个int参数的


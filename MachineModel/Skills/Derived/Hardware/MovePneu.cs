namespace Model.Skills
{
    ///// <summary>
    ///// Skill that represents a pneumatic movement
    ///// </summary>
    //class MovePneu : SingleSkill
    //{
    //    public MovePneu(string name, ResourceBase executer, Signal startPosition, Signal targetPosition,
    //        string startCons = null, string activeCons = null, string finishedCons = null, bool sequentially=true)
    //        : base(name, executer, startCons, activeCons, finishedCons, sequentially)
    //    {
    //        StartConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        StartConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        StartConditions.AddCondition($"{startPosition.Name}|SensorIsSet == true");
    //        StartConditions.AddCondition($"{targetPosition.Name}|SensorIsSet == false");

    //        ActiveConditions.AddCondition($"{executer.Name}|IsInPosition == false");
    //        ActiveConditions.AddCondition($"{executer.Name}|IsMoving == true");
    //        ActiveConditions.AddCondition($"{startPosition.Name}|SensorIsSet == false");
    //        ActiveConditions.AddCondition($"{targetPosition.Name}|SensorIsSet == false");

    //        FinishConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        FinishConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        FinishConditions.AddCondition($"{startPosition.Name}|SensorIsSet == false");
    //        FinishConditions.AddCondition($"{targetPosition.Name}|SensorIsSet == true");


    //        executer.ResourceStates.SetState("IsMoving", false);

    //        Name = $"{ExecutingResource.Name} Moves";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            Logger.Log($"{Name} ...");
    //            Thread.Sleep(300);
    //            Logger.Log($"{Name} ...done");
    //        });

    //        executer.Skills.Add(this.Name, this);
    //    }
    //}
}

namespace Model.Skills
{
    ///// <summary>
    ///// A Skill that represents the movement of a <see cref="Robot"/>
    ///// </summary>
    //class MoveTo : SingleSkill
    //{
    //    public MoveTo(string name, Robot executer, ResourceBase startLocation = null, ResourceBase targetLocation = null,
    //        string startCons = null, string activeCons = null, string finishedCons = null, bool sequentially=true)
    //        : base(name, executer, startCons, activeCons, finishedCons, sequentially)
    //    {
    //        StartConditions.AddCondition($"{executer.Name}|IsMoving == false");

    //        StartConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        if (startLocation != null) { StartConditions.AddCondition($"{startLocation.Name}|AreaIsOccupied == true"); }
    //        if (targetLocation != null) { StartConditions.AddCondition($"{targetLocation.Name}|AreaIsOccupied == false"); }

    //        ActiveConditions.AddCondition($"{executer.Name}|IsInPosition == false");
    //        ActiveConditions.AddCondition($"{executer.Name}|IsMoving == true");

    //        FinishConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        FinishConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        if (startLocation != null) { FinishConditions.AddCondition($"{startLocation.Name}|AreaIsOccupied == false"); }
    //        if (targetLocation != null) { FinishConditions.AddCondition($"{targetLocation.Name}|AreaIsOccupied == true"); }

    //        executer.ResourceStates.SetState("IsMoving", false);

    //        Name = $"{ExecutingResource.Name} MovesTo";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            Logger.Log($"{Name} ...");
    //            Thread.Sleep(400);
    //            Logger.Log($"{Name} ...done");
    //        });
    //    }
    //}
}

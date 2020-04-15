namespace Model.Skills
{
    ///// <summary>
    ///// A Skill that represents the transportation of an object
    ///// </summary>
    //class Feed : SingleSkill
    //{
    //    public Feed(string name, ResourceBase executer, ResourceBase transportedObject, string startCons = null, string activeCons = null, string finishCons = null, bool sequentially=true) : base(name, executer, startCons, activeCons, finishCons, sequentially)
    //    {
    //        ActiveConditions.AddCondition($"{executer.Name}|IsMoving == true");
    //        ActiveConditions.AddCondition($"{transportedObject.Name}|IsMoving == true");
    //        ActiveConditions.AddCondition($"{transportedObject.Name}|IsInPosition == false");


    //        FinishConditions.AddCondition($"{transportedObject.Name}|IsInPosition == true");
    //        FinishConditions.AddCondition($"{transportedObject.Name}|IsMoving == false");

    //        DesiredObjects = new HashSet<IResource>() { transportedObject };

    //        executer.ResourceStates.SetState("IsMoving", false);

    //        Name = "Transport";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            Logger.Log($"{ExecutingResource.Name} does transport {DesiredObjects.FirstOrDefault().Name}...");
    //            Thread.Sleep(500);
    //            Logger.Log($"{ExecutingResource.Name} transported {DesiredObjects.FirstOrDefault().Name} to its destination");
    //        });
    //    }
    //}
}

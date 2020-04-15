namespace Model.Skills
{
    ///// <summary>
    ///// A Skill that represents the dropping of an object
    ///// </summary>
    //class Drop : SingleSkill
    //{
    //    /// <summary>
    //    /// Creates the Drop Skill
    //    /// </summary>
    //    /// <param name="executer">The Resource that drops an object</param>
    //    /// <param name="objectToDrop">The Resource that is dropped</param>
    //    /// <param name="targetLocation">The Resource that determines if the target location is occupied</param>
    //    public Drop(string name, ResourceBase executer, ResourceBase objectToDrop, ResourceBase targetLocation = null,
    //        string startCons = null,
    //        string activeCons = null,
    //        string finishedCons = null,
    //        bool sequentially=true)
    //        : base(name, executer, startCons, activeCons, finishedCons,sequentially)
    //    {
    //        StartConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        StartConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        StartConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == true");
    //        StartConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == true");

    //        // targetLocation St06 Tray needs none of these startconditions
    //        if (targetLocation != null && targetLocation.Name != "St06 Tray")
    //        {
    //            StartConditions.AddCondition($"{targetLocation.Name}|AreaIsOccupied == true");
    //            StartConditions.AddCondition($"{targetLocation.Name}|InterfaceIsClosed == false");
    //            StartConditions.AddCondition($"{targetLocation.Name}|InterfaceIsOccupied == false");
    //        }


    //        ActiveConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        ActiveConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == true");
    //        ActiveConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == true");

    //        FinishConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        FinishConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == false");
    //        FinishConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == false");

    //        // targetLocation St06 Tray needs none of these finishconditions
    //        if (targetLocation != null && targetLocation.Name != "St06 Tray")
    //        {
    //            FinishConditions.AddCondition($"{targetLocation.Name}|InterfaceIsOccupied == true");
    //            FinishConditions.AddCondition($"{targetLocation.Name}|InterfaceIsClosed == true");
    //        }


    //        DesiredObjects = new HashSet<IResource>() { objectToDrop };

    //        executer.ResourceStates.SetState("InterfaceIsClosed", false);
    //        executer.ResourceStates.SetState("InterfaceIsOccupied", false);

    //        Name = $"Drop {objectToDrop.Name} from {executer.Name}";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            Logger.Log($"{ExecutingResource.Name} tries to drop {DesiredObjects.FirstOrDefault().Name}...");
    //            Thread.Sleep(200);
    //            Logger.Log($"{ExecutingResource.Name} dropped {DesiredObjects.FirstOrDefault().Name}");
    //        });
    //    }
    //}
}

namespace Model.Skills
{
    ///// <summary>
    ///// A Skill that represents the picking up of an object
    ///// </summary>
    //class PickUp : SingleSkill
    //{
    //    /// <summary>
    //    /// constructor for the PickUp <see cref="SkillBase"/>
    //    /// </summary>
    //    /// <param name="executer">The <see cref="ResourceBase"/> that picks up a part</param>
    //    /// <param name="objectToPickUp">The <see cref="ResourceBase"/> is to be picked up</param>
    //    /// <param name="sourceLocation">The <see cref="ResourceBase"/> that determines if a part is present</param>
    //    public PickUp(string name, ResourceBase executer, ResourceBase objectToPickUp, ResourceBase sourceLocation = null,
    //        string startCons = null,
    //        string activeCons = null,
    //        string finishedCons = null,
    //        bool sequentially = true)
    //        : base(name, executer, startCons, activeCons, finishedCons, sequentially)
    //    {
    //        StartConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        StartConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        StartConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == false");
    //        StartConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == false");
    //        if (sourceLocation != null)
    //        {
    //            StartConditions.AddCondition($"{sourceLocation.Name}|AreaIsOccupied == true");
    //            StartConditions.AddCondition($"{sourceLocation.Name}|InterfaceIsClosed == true");
    //            StartConditions.AddCondition($"{sourceLocation.Name}|InterfaceIsOccupied == true");
    //            ActiveConditions.AddCondition($"{sourceLocation.Name}|AreaIsOccupied == true");
    //            FinishConditions.AddCondition($"{sourceLocation.Name}|InterfaceIsClosed == false");
    //            FinishConditions.AddCondition($"{sourceLocation.Name}|InterfaceIsOccupied == false");
    //        }

    //        ActiveConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        ActiveConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == false");
    //        ActiveConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == true");


    //        FinishConditions.AddCondition($"{executer.Name}|IsMoving == false");
    //        FinishConditions.AddCondition($"{executer.Name}|IsInPosition == true");
    //        FinishConditions.AddCondition($"{executer.Name}|InterfaceIsClosed == true");
    //        FinishConditions.AddCondition($"{executer.Name}|InterfaceIsOccupied == true");

    //        DesiredObjects = new HashSet<IResource>() { objectToPickUp };

    //        executer.ResourceStates.SetState("InterfaceIsClosed", true);
    //        executer.ResourceStates.SetState("InterfaceIsOccupied", true);

    //        Name = $"PickUp {objectToPickUp.Name} with {executer.Name }";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            Logger.Log($"{ExecutingResource.Name} tries to pick up {DesiredObjects.FirstOrDefault().Name}...");
    //            Thread.Sleep(300);
    //            Logger.Log($"{ExecutingResource.Name} picked up {DesiredObjects.FirstOrDefault().Name}... done");
    //        });
    //    }
    //}
}

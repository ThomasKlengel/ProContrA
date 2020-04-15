namespace Model.Skills
{
    ///// <summary>
    ///// Skill for detecting a hole in a workpiece or the color of a workpiece
    ///// </summary>
    //class Detect : SingleSkill
    //{
    //    public Detect(string name, ResourceBase executer, ResourceBase areaToDetect, bool sequentially) : base(name, executer, sequentially)
    //    {
    //        StartConditions.AddCondition($"{areaToDetect.Name}|InterfaceIsOccupied == true");
    //        StartConditions.AddCondition($"{areaToDetect.Name}|ItemIsProcessed == false");

    //        ActiveConditions.AddCondition($"{areaToDetect.Name}|InterfaceIsOccupied == true");

    //        FinishConditions.AddCondition($"{areaToDetect.Name}|AreaIsOccupied == false");
    //        FinishConditions.AddCondition($"{areaToDetect.Name}|ItemIsProcessed == true");

    //        Name = $"{ExecutingResource.Name} detects object in {areaToDetect.Name}";

    //        ExecuteMethod = new Action(() =>
    //        {
    //            //default
    //            Logger.Log($"{ExecutingResource.Name} detecs...");
    //            Thread.Sleep(200);
    //            Logger.Log($"{ExecutingResource.Name} detection... done");
    //        });
    //    }
    //}
}

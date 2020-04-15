namespace Model.Resources
{
    /// <summary>
    /// A class representing a robot
    /// </summary>
    class Robot : ResourceBase
    {
        public Robot(string name) : base(name, ResourceType.Tool)
        {
            ResourceStates.SetState("IsInPosition", true);
            ResourceStates.SetState("IsMoving", false);
            ResourceStates.SetState("InterfaceIsOccupied", false);
            ResourceStates.SetState("InterfaceIsClosed", false);
        }
    }
}

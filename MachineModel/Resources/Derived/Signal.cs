namespace Model.Resources
{
    /// <summary>
    /// A class representing a signal of a sensor
    /// </summary>
    class Signal : ResourceBase
    {
        public Signal(string name) : base(name, ResourceType.Signal)
        {
            Name = name;
            ResouceType = ResourceType.Signal;
            ResourceStates = new ResourceState();
            ResourceStates.SetState("SensorIsSet", false);
        }
    }
}

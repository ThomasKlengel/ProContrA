namespace Model.Resources
{
    /// <summary>
    /// A class representing a sensor
    /// </summary>
    class Sensor : ResourceBase
    {
        public Sensor(string name) : base(name, ResourceType.Tool)
        {
            ResourceStates.SetState("SensorIsSet", false);
        }
    }
}

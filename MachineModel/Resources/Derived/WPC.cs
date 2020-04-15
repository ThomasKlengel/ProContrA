namespace Model.Resources
{
    /// <summary>
    /// A class representing a workpiece carrier
    /// </summary>
    class WPC : ResourceBase
    {
        public WPC(string name) : base(name, ResourceType.Storage)
        {
            ResourceStates.SetState("ItemIsProcessed", false);
        }
    }
}

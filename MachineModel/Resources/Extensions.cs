using System.Collections.Generic;

namespace Model.Resources
{
    /// <summary>
    ///  A class containg extension methods
    /// </summary>
    public static class Extensions
    {
        #region Extension methods for AttachedResources
        public static void AddAttachedResource(this IResource parent, IResource resourceToAdd)
        {
            // check double Resources 
            bool doubled = false;
            foreach (var res in parent.AttachedResources) if (res.Name == resourceToAdd.Name) { doubled = true; break; }
            if (!doubled)
            {
                parent.AttachedResources.Add(resourceToAdd);

                if (resourceToAdd.Parent != null)
                {
                    resourceToAdd.Parent.AttachedResources.Remove(resourceToAdd);
                }
                resourceToAdd.Parent = parent;
            }
        }

        public static void AddAttachedResources(this IResource parent, List<IResource> resourcesToAdd)
        {
            foreach (ResourceBase resource in resourcesToAdd)
            {
                parent.AddAttachedResource(resource);
            }
        }
        #endregion
    }
}

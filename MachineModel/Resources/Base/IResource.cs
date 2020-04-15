using Model.Skills;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Resources
{
    /// <summary>
    /// An interface containg the resource model properties and methods
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// A unique identifier of a resource
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The <see cref="ResourceType"/> of the resource
        /// </summary>
        ResourceType ResouceType { get; set; }

        /// <summary>
        /// The current <see cref="State"/>s of the resource
        /// </summary>
        ResourceState ResourceStates { get; set; }

        /// <summary>
        /// The <see cref="SkillBase"/>s that the resource can execute
        /// </summary>
        Dictionary<string,ISkill> Skills { get; set; }

        /// <summary>
        /// Any <see cref="ResourceBase"/> that is currently mounted/connected to this resource
        /// </summary>
        HashSet<IResource> AttachedResources { get; set; }

        /// <summary>
        /// The <see cref="ResourceBase"/> that is one level closer to resource tree root
        /// The only resource that has no parent is the machine(later factory?) itself
        /// </summary>
        IResource Parent { get; set; }

        /// <summary>
        /// Method that executes a <see cref="SkillBase"/> of the resource
        /// </summary>
        /// <param name="s">The skill to execute</param>
        /// <param name="execParams">The parameters used for the Execution</param>
        /// <returns></returns>
        Task ExecuteSkill(SkillBase s, object execParams = null);

    }
}

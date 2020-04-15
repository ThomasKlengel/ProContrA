using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Skills;

namespace Model.Resources
{
    /// <summary>
    /// A class representing a Resource
    /// </summary>
    public class ResourceBase : IResource
    {
        #region Contructors
        public ResourceBase(string name, ResourceType type)
        {
            Name = name;
            Descripion = string.Empty;
            ResouceType = type;
            ResourceStates = new ResourceState();
            AttachedResources = new HashSet<IResource>();
            Skills = new Dictionary<string, ISkill>();

            Globals.Resources.Add(this.Name, this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// A unique identifier of a resource
        /// </summary>
        public string Name { get; set; }
        public string Descripion { get; set; }
        /// <summary>
        /// The <see cref="ResourceType"/> of the resource
        /// </summary>
        public ResourceType ResouceType { get; set; }
        /// <summary>
        /// The current <see cref="State"/>s of the resource
        /// </summary>
        public ResourceState ResourceStates { get; set; }
        /// <summary>
        /// The <see cref="SkillBase"/>s that the resource can execute
        /// </summary>
        public Dictionary<string, ISkill> Skills { get; set; }

        /// <summary>
        /// Any <see cref="ResourceBase"/> that are mounted/connected to this resource
        /// </summary>
        public HashSet<IResource> AttachedResources { get; set; }

        /// <summary>
        /// The <see cref="ResourceBase"/> that is one level closer to resource tree root
        /// The only resource that has no parent is the machine(later factory?) itself
        /// </summary>
        public IResource Parent { get; set; }
        #endregion

        #region public Methods
        /// <summary>
        /// Method that executes a <see cref="SkillBase"/> of the resource
        /// </summary>
        /// <param name="s">The <see cref="SkillBase"/> to execute</param>
        /// <param name="execParams">The parameters used for the Execution</param>
        /// <returns></returns>
        public Task ExecuteSkill(SkillBase s, object execParams = null)
        {
            string key = $"{s.ExecutingResource.Name}.{s.Name}";

            if (Skills.ContainsKey(key))
            {
                Logger.Log($"Could not execute Skill {s.Name}, since it was not in list");
                return null;
            }
            else
            {
                return ExecuteSkill(key, execParams);
            }
        }

        /// <summary>
        /// Method that executes a <see cref="SkillBase"/> of the resource
        /// </summary>
        /// <param name="s">The <see cref="SkillBase"/> by name to execute</param>
        /// <param name="execParams">The parameters used for the Execution</param>
        /// <returns></returns>
        public Task ExecuteSkill(string s, object execParams = null)
        {
            if (!Skills.ContainsKey(s))
            {
                Logger.Log($"Could not execute Skill {s}, since it was not in list");
                return null;
            }
            else
            {
                return Skills[s].Execute(execParams);
            }
        }

        #endregion
    }
}

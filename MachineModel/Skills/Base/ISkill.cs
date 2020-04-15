using Model.Conditions;
using Model.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Skills
{
    public interface ISkill
    {
        #region For display purposes only
        public string Name { get; set; }
        public string FinishedStation { get; set; }

        #endregion

        #region Properties
        /// <summary>
        ///  The <see cref="ResourceBase"/> that executes the <see cref="SkillBase"/>
        /// </summary>
        public IResource ExecutingResource { get; set; }
        /// <summary>
        /// Any <see cref="ResourceBase"/>s that are handled by the <see cref="SkillBase"/>
        /// </summary>
        /// 
        public HashSet<IResource> DesiredObjects { get; set; }

        /// <summary>
        /// Conditions that have to be matched to start the execution of the skill
        /// </summary>
        public ICondition StartConditions { get; set; }

        /// <summary>
        /// Conditions that define the active state
        /// </summary>
        public ICondition ActiveConditions { get; set; }

        /// <summary>
        /// Conditions that define the finished state
        /// </summary>
        public ICondition FinishConditions { get; set; }

        public List<Tuple<string, string>> TaskParameters { get; set; }

        /// <summary>
        /// Whether a Skill is executet sequentially or parallel
        /// </summary>
        public bool Sequential { get; }

        /// <summary>
        /// A list of skills that can be executed by this skill
        /// </summary>
        public SortedList<int, ISkill> SubSkills { get; }

        #endregion

        #region Execution of skill for simulation

        /// <summary>
        /// Stuff that is exectued when the Skill is active
        /// </summary>
        /// <param name="o"></param>
        Task Execute(object o = null);

        /// <summary>
        /// checks if the start conditions of a <see cref="SkillBase"/> are fulfilled
        /// </summary>
        /// <returns></returns>
        bool CanExecute();

        /// <summary>
        /// checks if the finished conditions are not already fulfilled
        /// </summary>
        /// <returns></returns>
        bool NeedsToExecute();

        #region helper methods
        /// <summary>
        /// Method that sets the state of the <see cref="ExecutingResource"/> when the skill is active
        /// </summary>
        void SetActive();

        /// <summary>
        /// Method that sets the state of the <see cref="ExecutingResource"/> when the skill has finished executing
        /// </summary>
        void SetFinished();

        /// <summary>
        /// Adds a new Skill to the subskills
        /// </summary>
        /// <param name="s"></param>
        public void AddSubskill(ISkill s);
        #endregion

        Action ExecuteMethod { get; set; }
        #endregion
    }
}

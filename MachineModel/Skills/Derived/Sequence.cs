using Model.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.Skills
{
    /// <summary>
    /// A class representing a < see cref="SkillBase"/> thats subitems are executet sequentially
    /// </summary>
    class Sequence : SkillBase
    {
        public Sequence(string name, IResource executer, List<ISkill> skills = null) : base(name, executer, true)
        {
            for (int i = 0; i < skills?.Count; i++)
            {
                AddSubskill(skills[i]);
            }
        }

        private Task executingTask;

        /// <summary>
        /// Stuff that is exectued when the Skill is active
        /// </summary>
        /// <param name="o"></param>
        public override Task Execute(object o = null)
        {
            // if the executing task is not null and didnt already execute to completion
            if (executingTask == null || executingTask.IsCompleted)
            {
                // create the Task
                Task t = new Task(() =>
                {
                    
                    if (CanExecute())
                    {
                        // Logger.Log($"{Name} of {ExecutingResource.Name} starts executing");

                        SetActive();
                        ExecuteMethod();

                        foreach (var skill in SubSkills)
                        {
                            var innerTask = skill.Value.Execute();
                            innerTask.Wait();
                        }

                        SetFinished();

                        Logger.Debug($"Finished Executing {Name} of {ExecutingResource.Name}");
                        if (!string.IsNullOrEmpty(FinishedStation))
                        {
                            Logger.Log(FinishedStation);
                        }
                    }
                    else
                    {
                        Logger.Debug($"Can not execute {Name} of {ExecutingResource.Name}");
                    }
                });

                // set it to the task created previously
                executingTask = t;
            }

            if (NeedsToExecute())
            {
                // if the task is not running or waiting for it
                if (executingTask.Status != TaskStatus.Running &&
                    executingTask.Status != TaskStatus.WaitingToRun &&
                    executingTask.Status != TaskStatus.RanToCompletion)
                {   // run the task
                    executingTask.Start();
                }
            }
            else
            {
                Logger.Debug($"Do not need to execute {Name} of {ExecutingResource.Name}");
            }

            return executingTask;
        }
    }
}

using Model.Resources;
using System.Threading.Tasks;

namespace Model.Skills
{
    class SingleSkill : SkillBase
    {
        public SingleSkill(string name, IResource executer, string startCons, string activeCons, string finishedCons, bool sequentially = true) 
            : base (name, executer, startCons, activeCons, finishedCons, sequentially)
        {
           
        }

        /// <summary>
        /// constructor setting the ExecutingResource and initalising the lists
        /// </summary>
        /// <param name="executer"></param>
        public SingleSkill(string name, IResource executer, bool sequentially = true) : this(name, executer, (string)null, null, null, sequentially) { }

        private Task executingTask { get; set; }

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
                    executingTask.Wait();
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

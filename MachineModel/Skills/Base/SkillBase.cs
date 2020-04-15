using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Resources;
using Model.Conditions;

namespace Model.Skills
{
    /// <summary>
    /// Basic class representing a <see cref="SkillBase"/> of a <see cref="ResourceBase"/>
    /// </summary>
    public class SkillBase : ISkill
    {
        #region Constructors

        public SkillBase(string skillname, IResource executer, string startCons, string activeCons, string finishedCons, bool sequentially=true)
        {
            Sequential = sequentially;

            // create ConditionTrees for starting, running and finishing a skill
            if (startCons != null) StartConditions = Conditions.Parser.CreateCondidion(startCons);

            if (activeCons != null) ActiveConditions = Conditions.Parser.CreateCondidion(activeCons);

            if (finishedCons != null) FinishConditions = Conditions.Parser.CreateCondidion(finishedCons);

            // initialise properties
            // initialise properties
            DesiredObjects = new HashSet<IResource>();
            TaskParameters = new List<Tuple<string, string>>();
            //set executing resource
            if (executer != null)
            {
                ExecutingResource = executer;
            }

            Name = executer.Name+"."+skillname;

            if (!ExecutingResource.Skills.ContainsKey(this.Name))
            {
                ExecutingResource.Skills.Add(this.Name, this);
            }
            if (!Model.Skills.Globals.Skills.ContainsKey(this.Name))
            {
                Model.Skills.Globals.Skills.Add(this.Name, this);
            }
        }

        /// <summary>
        /// constructor setting the ExecutingResource and initalising the lists
        /// </summary>
        /// <param name="executer"></param>
        public SkillBase(string name,IResource executer, bool sequentially = true) : this(name,executer, (string)null, null, null, sequentially) { }

        #endregion

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
        public bool Sequential { get;  }

        /// <summary>
        /// A list of skills that can be executed by this skill
        /// </summary>
        public SortedList<int, ISkill> SubSkills { get; } = new SortedList<int, ISkill>();
        #endregion

        public void AddSubskill (ISkill s)
        {
            SubSkills.Add(SubSkills.Count, s);
        }

        #region Execution of skill for simulation
        private Task executingTask;

        /// <summary>
        /// Stuff that is exectued when the Skill is active
        /// </summary>
        /// <param name="o"></param>
        public virtual Task Execute(object o = null)
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
                }
            }
            else
            {
                Logger.Debug($"Do not need to execute {Name} of {ExecutingResource.Name}");
            }

            return executingTask;
        }

        /// <summary>
        /// checks if the start conditions of a <see cref="SkillBase"/> are fulfilled
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
        {

            //check if its a rotary table
            if (ExecutingResource.GetType() == typeof(RIT))
            {
                List<bool> canExecute = new List<bool>();
                int i = 0;
                // get every nest of rotary table
                foreach (var item in ExecutingResource.AttachedResources)
                {
                    if (item.GetType() == typeof(WPC))
                    {
                        // first nest
                        if (i == 0)
                        {
                            if (Convert.ToBoolean(item.ResourceStates.GetState("InterfaceIsOccupied")) == true &&
                                Convert.ToBoolean(item.ResourceStates.GetState("AreaIsOccupied")) == false)
                            {
                                canExecute.Add(true);
                            }
                            else
                            {
                                canExecute.Add(false);
                            }
                        }

                        // other nests
                        else
                        {
                            if (!(Convert.ToBoolean(item.ResourceStates.GetState("InterfaceIsOccupied")) == true &&
                                Convert.ToBoolean(item.ResourceStates.GetState("ItemIsProcessed")) == false) &&
                                Convert.ToBoolean(item.ResourceStates.GetState("AreaIsOccupied")) == false)
                            {
                                canExecute.Add(true);
                            }
                            else
                            {
                                canExecute.Add(false);
                            }
                        }
                        i++;
                    }
                }

                // check if every nest is ready for execution
                bool execute = true;
                foreach (var item in canExecute)
                {
                    if (item == false)
                    {
                        execute = false;
                        break;
                    }
                }

                if (execute &&
                    Convert.ToBoolean(ExecutingResource.ResourceStates.GetState("IsMoving")) == false &&
                    Convert.ToBoolean(ExecutingResource.ResourceStates.GetState("IsInPosition")) == true)
                {
                    var inpos = ExecutingResource.ResourceStates.GetState("IsInPosition");
                    var mov = ExecutingResource.ResourceStates.GetState("IsMoving");
                    Logger.Debug($"'{ExecutingResource.Name}' - {inpos}");
                    Logger.Debug($"'{ExecutingResource.Name}' - {mov}");

                    foreach (var item in ExecutingResource.AttachedResources)
                    {
                        if (item.GetType() == typeof(WPC))
                        {
                            var intfaceOcc = item.ResourceStates.GetState("InterfaceIsOccupied");
                            var Area = item.ResourceStates.GetState("AreaIsOccupied");
                            Logger.Debug($"'{item.Name}' - {intfaceOcc}");
                            Logger.Debug($"'{item.Name}' - {Area}");
                        }
                    }
                    return true;
                }
            }

            // evaluate every startcondition
            var val = ResourceState.Evaluate(StartConditions);
            return val;
        }

        /// <summary>
        /// checks if the finished conditions are not already fulfilled
        /// </summary>
        /// <returns></returns>
        public bool NeedsToExecute()
        {
            // it needs to excecute if there are no finish conditions
            if (FinishConditions == null) return true;

            // evaluate every finishcondition (return true if the evaluation is false)
            return !ResourceState.Evaluate(FinishConditions);
        }

        #region helper methods
        /// <summary>
        /// Method that sets the state of the <see cref="ExecutingResource"/> when the skill is active
        /// </summary>
        public void SetActive()
        {
            //TODO: go through each sucondition, not just 1 level below
            var condition = ActiveConditions;

            if (condition is SimpleCondition sc )
            {
                Resources.Globals.Resources[sc.Resource].ResourceStates.SetState(sc.State, sc.StateValue);
            }
            else if (condition is ComplexCondition cc)
            {
                foreach (var subCondition in cc.Children)
                {
                    Resources.Globals.Resources[subCondition.Resource].ResourceStates.SetState(subCondition.State, subCondition.StateValue);
                }
            }                     
            
        }

        /// <summary>
        /// Method that sets the state of the <see cref="ExecutingResource"/> when the skill has finished executing
        /// </summary>
        public void SetFinished()
        {
            //TODO: go through each sucondition, not just 1 level below

            var condition = FinishConditions;

            if (condition is SimpleCondition sc)
            {
                Resources.Globals.Resources[sc.Resource].ResourceStates.SetState(sc.State, sc.StateValue);
            }
            else if (condition is ComplexCondition cc)
            {
                foreach (var subCondition in cc.Children)
                {
                    Resources.Globals.Resources[subCondition.Resource].ResourceStates.SetState(subCondition.State, subCondition.StateValue);
                }
            }
        }
        #endregion

        public Action ExecuteMethod { get; set; }
        #endregion
    }
}

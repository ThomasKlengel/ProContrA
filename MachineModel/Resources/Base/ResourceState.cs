using Model.Conditions;
using System;
using System.Collections.Generic;

namespace Model.Resources
{
    /// <summary>
    /// All possible StateTypes
    /// </summary>
    public enum AllStateTypes
    {
        SensorIsSet,
        IsInPosition,
        HasTimeout,
        InterfaceIsClosed,
        InterfaceIsOccupied,
        IsMoving,
        AreaIsOccupied,
        AreaIsProcessed,
        ItemIsProcessed,
        PositionX,
        PositionY,
        PositionZ
    }

    /// <summary>
    /// A class that represents all <see cref="State"/>s a <see cref="ResourceBase"/> can have
    /// </summary>
    public class ResourceState
    {
        #region Properties

        /// <summary>
        /// Dictionary with all States of the Resource (State, StateValue)
        /// </summary>
        public Dictionary<string, object> Values = new Dictionary<string, object>();

        #endregion

        #region Constructors
        public ResourceState()
        {
            // set all StateTypes 
            foreach (AllStateTypes i in Enum.GetValues(typeof(AllStateTypes)))
            {
                if (i.ToString() == "IsInPosition") this.Values.Add(i.ToString(), true);
                else if (i.ToString() == "PositionX" || i.ToString() == "PositionY" || i.ToString() == "PositionZ") this.Values.Add(i.ToString(), 0);
                else this.Values.Add(i.ToString(), false);
            }
        }

        #endregion

        #region public Methods

        /// <summary>
        /// Method to get the Value of a required state
        /// </summary>
        /// <param name="stateType">required StateType as string</param>
        /// <returns>Value of the StateType</returns>
        public object GetState(string stateType)
        {
            return this.Values[stateType];
        }

        /// <summary>
        /// Method to manually set the state of a resource
        /// </summary>
        /// <param name="stateType">StateType you want to change</param>
        /// <param name="stateValue">changed Value</param>
        public void SetState(string stateType, object stateValue)
        {
            this.Values[stateType] = stateValue;
        }


        /// <summary>
        /// check if <see cref="condition"/> is true
        /// </summary>
        /// <param name="condition"></param>
        /// <returns> true/false </returns>
        public static bool Evaluate(ICondition condition)
        {
            // initially the evaluation is false


            IResource res = Globals.Resources[condition.Resource];

            // check operator and datatype
            switch (condition.CompareOperator)
            {
                case "==":
                    Type type = condition.Value.GetType();
                    if (type.Name == "Boolean")
                    {
                        if (Convert.ToBoolean(res.ResourceStates.Values[condition.State]) == Convert.ToBoolean(condition.Value))
                            return true;
                    }
                    else if (type.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) == Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
                case "!=":
                    Type type1 = res.ResourceStates.Values[condition.State].GetType();
                    if (type1.Name == "Boolean")
                    {
                        if (Convert.ToBoolean(res.ResourceStates.Values[condition.State]) != Convert.ToBoolean(condition.Value))
                            return true;
                    }
                    else if (type1.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) != Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
                case ">=":
                    var type2 = res.ResourceStates.Values[condition.State].GetType();
                    if (type2.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) >= Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
                case "<=":
                    var type3 = res.ResourceStates.Values[condition.State].GetType();
                    if (type3.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) <= Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
                case ">":
                    var type4 = res.ResourceStates.Values[condition.State].GetType();
                    if (type4.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) > Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
                case "<":
                    var type5 = res.ResourceStates.Values[condition.State].GetType();
                    if (type5.Name == "Double")
                    {
                        if (Convert.ToDouble(res.ResourceStates.Values[condition.State]) < Convert.ToDouble(condition.Value)) return true;
                    }

                    break;
            }

            return false;
        }


        #endregion
    }
}

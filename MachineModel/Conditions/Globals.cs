using System.Collections.Generic;
using System.Linq;

namespace Model.Conditions
{
    /// <summary>
    /// Class containing global static information 
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// A dictionary containing every <see cref="ICondition"/> used by the program
        /// </summary>
        public static Dictionary<ulong, ICondition> Conditions { get; set; } = new Dictionary<ulong, ICondition>();

        /// <summary>
        /// "Left" part of the mapping, containing the resources and the state to check 
        /// </summary>
        public static List<string> MappingLeft
        {
            get
            {   // get all simple conditions in a list
                var maps = Conditions.Where(c => c.Value is SimpleCondition).Select(c => c.Value).ToList();
                HashSet<string> uniques = new HashSet<string>();
                // add a string with <Resource>|<State> to the set (duplicates are ignored)
                foreach (var c in maps)
                {
                    uniques.Add($"{c.Resource}|{c.State}");
                }
                // return as a list
                return uniques.OrderBy(c => c).ToList();
            }
        }

        /// <summary>
        /// Mapping dictionary containing the states and their mapped representative (from file)
        /// </summary>
        public static Dictionary<string, object> Mapping { get; set; } = new Dictionary<string, object>();

    }
}

using System;
using System.IO;
using System.Text;

namespace Model.Conditions
{
    public static class Mapping
    {
        private const string DEFAULT_PATH = "Mapping.txt";

        /// <summary>
        /// Writes the <see cref="Globals.MappingLeft"/> to a file
        /// </summary>
        /// <param name="path">The file path to write to</param>
        public static void WriteRawMappingToFile(string path = DEFAULT_PATH)
        {
            try
            {   //write every item as <Item> = <BlankSpace>
                StringBuilder sb = new StringBuilder(string.Empty);
                foreach (var item in Globals.MappingLeft)
                {
                    sb.AppendLine($"{item} = ");
                }

                File.WriteAllText(DEFAULT_PATH, sb.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogToList($"Error while writing mapping to {path} : {ex.Message}");
            }
        }

        /// <summary>
        /// Reads the <see cref="Globals.Mapping"/> from a file
        /// </summary>
        /// <param name="path">The file path to read from</param>
        public static bool ReadMappingFromFile(string path = DEFAULT_PATH)
        {
            try
            {
                //read teh lines in the file
                var lines = File.ReadAllLines(DEFAULT_PATH);
                foreach (var line in lines)
                {
                    // get state and value
                    var splits = line.Split('=');
                    var state = splits[0].Trim();
                    var val = splits[1].Trim().Replace(".",",");

                    // set state and value in dictionary
                    if (double.TryParse(val, out double dVal))
                    {
                        Globals.Mapping.Add(state, dVal);
                    }
                    else if (bool.TryParse(val, out bool bVal))
                    {
                        Globals.Mapping.Add(state, bVal);
                    }
                    else
                    {
                        Logger.LogToList($"Could not transform value {val} of state {state} into a know format (double or boolean).");
                        Globals.Mapping.Add(splits[0].Trim(), false);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogToList($"Error while reading mapping from {path} : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Writes the complete <see cref="Globals.Mapping"/> table to a file
        /// </summary>
        /// <param name="path">The file path to write to</param>
        public static void WriteCompleteMappingToFile(string path = DEFAULT_PATH)
        {
            try
            {
                // write current mapping element as "<State> = <StateValue>" to file
                StringBuilder sb = new StringBuilder(string.Empty);
                foreach (var item in Globals.Mapping)
                {
                    sb.AppendLine($"{item.Key} = {item.Value}");
                }

                File.WriteAllText(DEFAULT_PATH, sb.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogToList($"Error while writing mapping to {path} : {ex.Message}");
            }
        }
    }
}

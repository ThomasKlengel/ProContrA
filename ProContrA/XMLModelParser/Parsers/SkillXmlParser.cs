using Model.Resources;
using Model.Skills;
using System.Xml.Linq;
using System.Linq;

namespace ProContrA.ModelParser
{
    class SkillXmlParser
    {
        public static ISkill ParseToSkill (XElement skillNode, ResourceBase r )
        {
            // create new skill
            SkillBase skill = new SkillBase(skillNode.Attribute("Name").Value, r);

            // get the condition nodes
            var StartConditionNodes = skillNode.Element("StartConditions")?.Elements("Condition").ToList();
            var ActiveConditionNodes = skillNode.Element("ActiveConditions")?.Elements("Condition").ToList();
            var FinishedConditionNodes = skillNode.Element("FinishedConditions")?.Elements("Condition").ToList();

            // add Start Condition
            var completeConditionString = "";
            StartConditionNodes?.ForEach(cond =>
            {
                completeConditionString += (ConditionXmlParser.ParseXmlToConditionString(cond, r)) + " AND ";
            });
            if (completeConditionString != string.Empty)
            {
                completeConditionString = completeConditionString.Remove(completeConditionString.Length - 5);
                skill.StartConditions = Model.Conditions.Parser.CreateCondidion(completeConditionString);
            }
            
            // add Active Condition 
            completeConditionString = "";
            ActiveConditionNodes?.ForEach(cond =>
            {
                completeConditionString += (ConditionXmlParser.ParseXmlToConditionString(cond, r)) + " AND ";
            });
            if (completeConditionString != string.Empty)
            {
                completeConditionString = completeConditionString.Remove(completeConditionString.Length - 5);
                skill.ActiveConditions = Model.Conditions.Parser.CreateCondidion(completeConditionString);
            }

            // add Finish Condition
            completeConditionString = "";
            FinishedConditionNodes?.ForEach(cond =>
            {
                completeConditionString += (ConditionXmlParser.ParseXmlToConditionString(cond, r)) + " AND ";
            });
            if (completeConditionString != string.Empty)
            {
                completeConditionString = completeConditionString.Remove(completeConditionString.Length - 5);
                skill.FinishConditions = Model.Conditions.Parser.CreateCondidion(completeConditionString);
            }

            return skill;

           //< SkillDefinition Name = "Open" TimeRequired = "100" >
           //  < StartConditions >
           //    < Condition Resource = "Self" State = "Error" BoolValue = "false" />
           //       </ StartConditions >
           //       < FinishedConditions >

        }
    }
}

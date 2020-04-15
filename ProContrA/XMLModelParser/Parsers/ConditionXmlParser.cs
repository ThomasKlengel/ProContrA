using Model.Conditions;
using Model.Resources;
using System.Xml.Linq;

namespace ProContrA.ModelParser
{
    class ConditionXmlParser
    {
        public static ICondition ParseXmlToCondition (XElement conditionNode, ResourceBase r)
        {
            var c = Parser.CreateCondidion(ParseXmlToConditionString(conditionNode,r));
            return c;
        }

        public static string ParseXmlToConditionString(XElement conditionNode, ResourceBase r)
        {
            var statement = conditionNode.Attribute("Statement")?.Value;
            string conditionString = string.Empty;

            if (statement == null)
            {
                var res = conditionNode.Attribute("Resource")?.Value;
                var stat = conditionNode.Attribute("State")?.Value;
                var val = conditionNode.Attribute("BoolValue")?.Value;

                res = res == "Self" ? r.Name : res;

                conditionString = res + "|" + stat + "==" + val;
            }
            else
            {
                conditionString = statement.Replace("Self",r.Name);
            }

            return conditionString;
           
        }
    }
}

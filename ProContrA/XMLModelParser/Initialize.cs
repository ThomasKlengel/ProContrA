using System.Xml.Linq;

namespace ProContrA.ModelParser
{
    public class Initializer
    {
        public static bool Initialize()
        {
            string filename = @"ConfigFiles\ProContraProcess.xml";
            var doc = XDocument.Load(filename);

            Model.Resources.Globals.Resources.Clear();
            Model.Skills.Globals.Skills.Clear();
            Model.Conditions.Globals.Conditions.Clear();

            var root = doc.Root;
            var resourcesNode = root.Element("Resources");
            var resourceNodes = resourcesNode.Elements("Resource");
            foreach (var node in resourceNodes)
            {
                ResourceXMLParser.ParseToResource(node);
            }

            var r = Model.Resources.Globals.Resources;
            var s = Model.Skills.Globals.Skills;
            var c = Model.Conditions.Globals.Conditions;

            return r?.Count > 0 && s?.Count > 0 && c?.Count > 0;


        }
    }
}

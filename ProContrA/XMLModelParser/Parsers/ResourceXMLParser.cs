using System.Xml.Linq;
using Model.Resources;

namespace ProContrA.ModelParser
{
    class ResourceXMLParser
    {
        public static ResourceBase ParseToResource (XElement resourceNode )
        {
            var resource = new ResourceBase(resourceNode.Attribute("Name").Value, Model.Resources.ResourceType.Unknown);
            var skillsNode = resourceNode?.Element("SkillDefinitions");
            if (skillsNode!=null)
            {
                var skillNodes = skillsNode?.Elements("SkillDefinition");

                int i = 0;
                foreach (var node in skillNodes)
                {
                    var skill = SkillXmlParser.ParseToSkill(node, resource);
                    resource.Skills.Add(resource.Name + "." + skill.Name, skill);
                    i++;
                }
            }

            return resource;

         /*   <Resource Name="Gripper" Type="Tool">
      <SkillDefinitions>
        <SkillDefinition Name="Open" TimeRequired="100">
          <StartConditions>
            <Condition Resource="Self" State="Error" BoolValue="false" />
          </StartConditions>
          <FinishedConditions>
            <Condition Resource="Self" State="Closed" BoolValue="false" />
            <Condition Resource="Self" State="Open" BoolValue="true" />
            <!--Same as 2 above conditions-->
            <Condition Statement="Self|Closed==false
                              AND Self|Open==true"/>
          </FinishedConditions>
        </SkillDefinition>
        <SkillDefinition Name="Close" TimeRequired="100">
          <StartConditions>
            <Condition Resource="Self" State="Error" BoolValue="false" />
          </StartConditions>
          <FinishedConditions>
            <Condition Resource="Self" State="Closed" BoolValue="true" />
            <Condition Resource="Self" State="Open" BoolValue="false" />
          </FinishedConditions>
        </SkillDefinition>
        <SkillDefinition Name="ErrorQuit" TimeRequired="500">
          <StartConditions>
            <Condition Resource="Self" State="Error" BoolValue="true"/>
          </StartConditions>
          <FinishedConditions>
            <Condition Resource="Self" State="Error" BoolValue="false"/>
          </FinishedConditions>
        </SkillDefinition>
      </SkillDefinitions>
      <States>
        <State ID="-1" Name="Error"/>
        <State ID="0" Name="Undefined"/>
        <State ID="1" Name="Opened"/>
        <State ID="2" Name="Closed"/>
      </States>
    </Resource>*/
        }

    }
}

using System.Collections;
using ItsAgentFramework.Components;

namespace ItsAgentFramework
{
    // Obsolete

    //public class SkillList : IEnumerable<Skill>
    //{
    //    private Dictionary<Type,Skill> _skillDict = new();

    //    public SkillList(IEnumerable<Skill> skillEnum)
    //    {
    //        foreach (var skill in skillEnum.Distinct())
    //        {
    //            _skillDict[skill.GetType()] = skill;
    //        }
    //    }

    //    public Skill this[Type skillType]
    //    {
    //        get { return _skillDict[skillType]; }
    //    }

    //    public Skill this[Skill skill]
    //    {
    //        get { return _skillDict[skill.GetType()]; }
    //    }

    //    public bool TryGetSkill(Type skillType, out Skill? skill)
    //    {
    //        return _skillDict.TryGetValue(skillType, out skill);
    //    }

    //    public void Add(Skill skill)
    //    {
    //        var skillType = skill.GetType();
    //        if( _skillDict.ContainsKey(skillType)) 
    //        {
    //            Console.WriteLine("Overwriting skill.");
    //        }
    //        _skillDict[skillType] = skill;
    //        return;
    //    }

    //    public void Remove(Skill skill)
    //    {
    //        var skillType = skill.GetType();
    //        if( !_skillDict.ContainsKey(skillType))
    //        {
    //            throw new ArgumentException("Tried to remove skill type that is not there.");
    //        }
    //        _skillDict.Remove(skillType);
    //        return;
    //    }

    //    public IEnumerator<Skill> GetEnumerator()
    //    {
    //        return _skillDict.Select(x => x.Value).ToList().GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}
}

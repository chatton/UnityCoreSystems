namespace Core.JRPG.Combat.Turns
{
    public interface INextCombatantProvider
    {
        Combatant NextCombatant();
    }
}
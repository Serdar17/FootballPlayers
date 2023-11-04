namespace FootballPlayers.Common.Validators;

public interface IModelValidator<in T> where T : class
{
    void Check(T model);
}
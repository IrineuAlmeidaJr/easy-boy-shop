using Cassandra;

namespace Infrastructure.Interfaces;

public interface IScyllaContext
{
    ISession GetSession();
}

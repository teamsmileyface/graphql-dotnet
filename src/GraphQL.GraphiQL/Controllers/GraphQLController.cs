using GraphQL.Types;
using System.Threading.Tasks;
using System.Web.Http;
using Domain;

namespace GraphQL.GraphiQL.Controllers
{
    public class GraphQLController : ApiController
    {
        private readonly ISimpleContainer _container;
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        public GraphQLController()
        {
            _executer = new DocumentExecuter();

            _container = new SimpleContainer();
            _container.Singleton(new ALBData());
            _container.Register<ALBQuery>();
            _container.Register<MatterType>();
            _container.Register<ContactType>();
            _container.Singleton(() => new ALBSchema(type => (GraphType) _container.Get(type)));

            _schema = _container.Get<ALBSchema>();
        }

        public async Task<ExecutionResult> Post(GraphQLQuery query)
        {
            return await Execute(_schema, null, query.Query);
        }

        public async Task<ExecutionResult> Execute(
          ISchema schema,
          object rootObject,
          string query,
          string operationName = null,
          Inputs inputs = null)
        {
            return await _executer.ExecuteAsync(schema, rootObject, query, operationName);
        }
    }
}

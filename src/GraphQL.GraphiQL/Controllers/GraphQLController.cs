using GraphQL.Types;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Domain;
using Domain.GraphQLDTO;

namespace GraphQL.GraphiQL.Controllers
{
    [EnableCors(origins:"http://localhost,http://localhost:82", headers:"*",methods:"*", SupportsCredentials = true)]
    public class GraphQLController : ApiController
    {
        private readonly ISimpleContainer _container;
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        public GraphQLController()
        {
            var connectionString = "Data Source=(local);Database=IrisLawBusiness_smiths;User Id=sa;Password=20Mountain08";

            _executer = new DocumentExecuter();

            _container = new SimpleContainer();
            _container.Singleton(new ALBData(connectionString));
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

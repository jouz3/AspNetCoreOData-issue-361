using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace net6_odata.Controllers;

[ODataRouteComponent("api/v1/data")]
public class SampleEntitiesController : ODataController
{
    private readonly SampleContext _sampleContext;

    public SampleEntitiesController(SampleContext sampleContext)
    {
        _sampleContext = sampleContext;
    }

    [EnableQuery]
    [HttpGet]
    public IQueryable<SampleEntity> Get() => _sampleContext.SampleEntities;
}

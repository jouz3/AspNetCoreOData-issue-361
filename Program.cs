using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using net6_odata;

/*
 * 1. 'dotnet databse update' (to apply db migration)
 * 2. run and call:
 * GET https://localhost:5001/api/v1/data/SampleEntities?$filter=SerializedProperty/Any(s: s eq '1')
 */

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<SampleContext>(@"Server=(localdb)\mssqllocaldb;Database=SampleContext");

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .AddOData(opt =>
    {
        opt.AddRouteComponents("api/v1/data", GetEdmModel_v1())
            .Select()
            .Filter()
            .OrderBy()
            .Expand()
            .SkipToken()
            .Count()
            .SetMaxTop(1000);
    });

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


IEdmModel GetEdmModel_v1()
{
    var builder_v1 = new ODataConventionModelBuilder();

    builder_v1.EntitySet<SampleEntity>("SampleEntities");

    return builder_v1.GetEdmModel();
}
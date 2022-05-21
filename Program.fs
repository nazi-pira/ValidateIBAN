#light "off"
#nowarn "20"
namespace CheckIBAN

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program = begin

    [<EntryPoint>]
    let main args = 
        let builder = WebApplication.CreateBuilder(args) in
        builder.Services.AddControllers();
        let app = builder.Build() in
        app.MapControllers();
        app.Run();
        0
end
        

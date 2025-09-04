var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure forwarded headers to handle requests from the load balancer (NGINX).
// This is critical for resolving the HTTPS redirection issue and allowing
// the application to know the original protocol (HTTP vs HTTPS) and host.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor |
        Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Add this middleware to use the forwarded headers.
// This must be placed before other middlewares like UseHttpsRedirection.
app.UseForwardedHeaders();

// The UseHttpsRedirection middleware is now safe to keep, as UseForwardedHeaders
// will handle the redirection correctly by inspecting the proxy headers.
//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();

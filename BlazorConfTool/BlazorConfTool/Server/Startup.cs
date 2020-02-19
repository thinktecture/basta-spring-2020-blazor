using AutoMapper;
using BlazorConfTool.Server.Hubs;
using BlazorConfTool.Server.Model;
using BlazorConfTool.Shared;
using GrpcGreeter;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace BlazorConfTool.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ConferenceValidator>();

            services.AddSignalR();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ConferencesDbContext>(
                options => options.UseInMemoryDatabase(databaseName: "ConfTool"));

            services.AddMvc();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                    options.Authority = "https://demo.identityserver.io";
                    options.ApiName = "api";
                });

            services.AddAuthorization();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Program>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGrpcWeb();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<ConferencesHub>("/conferencesHub");
                endpoints.MapFallbackToClientSideBlazor<Client.Program>("index.html");
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
            });
        }
    }
}

using AutoMapper;
using Evian.Entities.Entities.Adapters;
using Evian.Repository.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Web.Http;

namespace EvianAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("App"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning)));

            services.AddOptions();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PessoaProfile());
                mc.AddProfile(new BancoProfile());
                mc.AddProfile(new CategoriaProfile());
                mc.AddProfile(new ConciliacaoBancariaItemContaFinanceiraProfile());
                mc.AddProfile(new ConciliacaoBancariaItemProfile());
                mc.AddProfile(new ConciliacaoBancariaProfile());
                mc.AddProfile(new CondicaoParcelamentoProfile());
                mc.AddProfile(new ContaBancariaProfile());
                mc.AddProfile(new ContaFinanceiraBaixaMultiplaProfile());
                mc.AddProfile(new ContaFinanceiraBaixaProfile());
                mc.AddProfile(new ContaFinanceiraProfile());
                mc.AddProfile(new EstadoProfile());
                mc.AddProfile(new CidadeProfile());
                mc.AddProfile(new FormaPagamentoProfile());
                mc.AddProfile(new PaisProfile());
                mc.AddProfile(new TransferenciaFinanceiraProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvianAPI", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvianAPI v1"));
            }

            app.UseCors(option => { option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab3.Shared.ServiceInstaller {
    public interface IServiceInstaller {
        void AddServices(IServiceCollection services, IConfiguration configuration);
    }
}
